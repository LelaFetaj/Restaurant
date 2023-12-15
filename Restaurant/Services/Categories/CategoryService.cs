using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models.DTOs.Categories;
using Restaurant.API.Models.Entities.Categories;
using Restaurant.API.Repositories.Categories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Restaurant.API.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public CategoryService(ICategoryRepository categoryRepository, 
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public async Task<(bool, string)> AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            bool exists = await categoryRepository
                .SelectAllCategories()
                .AnyAsync(x => x.Title == createCategoryDto.Title);

            if (exists)
            {
                return (false, "A category with the same title exists. Please choose another title!");
            }

            if (createCategoryDto.File.Length > 0)
            {
                // Ensure the 'Images' directory exists in the 'wwwroot' folder
                var imagesDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                var savedFilename = Guid.NewGuid().ToString();
                var imagePath = Path.Combine(imagesDirectory, "Uploads", savedFilename);
                Directory.CreateDirectory(imagePath);

                var fileName = Path.GetFileName(createCategoryDto.File.FileName);

                // Save the uploaded image to the server
                var filePath = Path.Combine(imagePath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await createCategoryDto.File.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                // Return the public URL of the uploaded image
                //var imageUrl = urlHelper.Content("~/Images/Uploads/" + savedFilename + "/" + fileName);

                var baseUrl = configuration["BaseUrl"];
                var imageUrl = $"{baseUrl}/Images/Uploads/{savedFilename}/{fileName}";

                byte[] fileContents;

                using (var memoryStream = new MemoryStream())
                {
                    await createCategoryDto.File.CopyToAsync(memoryStream);
                    fileContents = memoryStream.ToArray();
                }

                Category category = new Category
                {
                    Title = createCategoryDto.Title,
                    Featured = createCategoryDto.Featured,
                    Active = createCategoryDto.Active,
                    ImageName = fileName,
                    FileContent = fileContents
                };

                await categoryRepository.InsertCategoryAsync(category);
            
            }

            return (true, "Success");
        }


        public async Task<CategoryImageResult> GetCategoryImage(Guid categoryId, int maxWidth = 800, int maxHeight = 600)
        {
            var category = await RetrieveCategoryById(categoryId);

            if (category == null)
            {
                return new CategoryImageResult { IsSuccess = false, Message = "Category not found" };
            }

            if (category.FileContent != null)
            {
                //return new CategoryImageResult { IsSuccess = true, FileContent = category.FileContent };

                byte[] resizedImage = ResizeImage(category.FileContent, maxWidth, maxHeight);

                return new CategoryImageResult { IsSuccess = true, FileContent = resizedImage };

            }

            else if (!string.IsNullOrEmpty(category.ImageName))
            {
                var baseUrl = configuration["BaseUrl"];
                var imageUrl = $"{baseUrl}/Images/Uploads/{category.ImageName}";
                return new CategoryImageResult { IsSuccess = true, ImageUrl = imageUrl };
            }

            return new CategoryImageResult { IsSuccess = false, Message = "Image not found" };
        }

        private byte[] ResizeImage(byte[] originalImage, int maxWidth, int maxHeight)
        {
            using (var image = Image.Load(originalImage))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(maxWidth, maxHeight),
                    Mode = ResizeMode.Max
                }));

                using (var memoryStream = new MemoryStream())
                {
                    image.SaveAsJpeg(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }


        public async Task<CategoryImageResult> UpdateCategoryPictureAsync(Guid categoryId, IFormFile newCategoryPicture)
        {
            var category = await RetrieveCategoryById(categoryId);

            if (category == null)
            {
                return new CategoryImageResult { IsSuccess = false, Message = $"Couldn't find category with id: {categoryId}" };
            }

            if (newCategoryPicture.Length > 0)
            {
                // Ensure the 'Images' directory exists in the 'wwwroot' folder
                var imagesDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                var savedFilename = Guid.NewGuid().ToString();
                var imagePath = Path.Combine(imagesDirectory, "Uploads", savedFilename);
                Directory.CreateDirectory(imagePath);

                var fileName = Path.GetFileName(newCategoryPicture.FileName);

                // Save the uploaded image to the server
                var filePath = Path.Combine(imagePath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await newCategoryPicture.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                // Return the public URL of the uploaded image
                //var imageUrl = urlHelper.Content("~/Images/Uploads/" + savedFilename + "/" + fileName);

                var baseUrl = configuration["BaseUrl"];
                var imageUrl = $"{baseUrl}/Images/Uploads/{savedFilename}/{fileName}";

                byte[] fileContents;

                using (var memoryStream = new MemoryStream())
                {
                    await newCategoryPicture.CopyToAsync(memoryStream);
                    fileContents = memoryStream.ToArray();
                }

                category.ImageName = fileName;
                category.FileContent = fileContents;

                await categoryRepository.UpdateCategoryAsync(category);
            }

            return new CategoryImageResult { IsSuccess = true, Message = "Success" };
        }

        public async Task<List<Category>> RetrieveAllCategories()
        {
            List<Category> categories = await categoryRepository
                .SelectAllCategories()
                .ToListAsync();

            return categories;
        }

        public async Task<Category> RetrieveCategoryById(Guid id) =>
            await categoryRepository.SelectCategoryByIdAsync(id);

        public async Task<(bool, string)> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, string imageName)
        {
            Category category = await categoryRepository.SelectCategoryByIdAsync(updateCategoryDto.Id);

            if (category == null)
            {
                return (false, $"Couldn't find category with id: {updateCategoryDto.Id}");
            }

            bool exists = await categoryRepository
                .SelectAllCategories()
                .AnyAsync(x => x.Title == updateCategoryDto.Title && x.Id != updateCategoryDto.Id);

            if (exists)
            {
                return (false, "An category with the same title exists. Please choose another title!");
            }

            category.Title = updateCategoryDto.Title;
            category.Featured = updateCategoryDto.Featured;
            category.Active = updateCategoryDto.Active;
            category.ImageName = imageName;

            await categoryRepository.UpdateCategoryAsync(category);

            return (true, "Success");
        }

        public async Task<(bool, string)> DeleteCategoryByIdAsync(Guid id)
        {
            Category category = await categoryRepository.SelectCategoryByIdAsync(id);

            if (category == null)
            {
                return (false, $"Couldn't find category with id: {id}");
            }

            await categoryRepository.DeleteCategoryAsync(category);

            return (true, "Success");
        }
    }
}
