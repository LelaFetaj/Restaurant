using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models.DTOs.Categories;
using Restaurant.API.Models.Entities.Categories;
using Restaurant.API.Repositories.Categories;

namespace Restaurant.API.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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

            Category category = new Category
            {
                Title = createCategoryDto.Title,
                Featured = createCategoryDto.Featured,
                Active = createCategoryDto.Active
            };

            await categoryRepository.InsertCategoryAsync(category);

            return (true, "Success");
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

        public async Task<(bool, string)> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
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
