using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.DTOs.Categories;
using Restaurant.API.Models.Entities.Categories;
using Restaurant.API.Repositories.Categories;
using Restaurant.API.Services.Categories;

namespace Restaurant.API.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddCategory([FromForm] CreateCategoryDto createCategoryDto)
        {
            try
            {
                // Call your service to add the category, passing the image URL
                (bool result, string message) = await categoryService.AddCategoryAsync(createCategoryDto);

                if (result)
                {
                    return Ok(new { message });
                }

                    
                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{categoryId}/get-image")]
        public async Task<IActionResult> GetCategoryImage(Guid categoryId, int maxWidth = 800, int maxHeight = 600)
        {
            var categoryImageResult = await categoryService.GetCategoryImage(categoryId, maxWidth, maxHeight);

            if (categoryImageResult.IsSuccess)
            {
                if (categoryImageResult.FileContent != null)
                {
                    return File(categoryImageResult.FileContent, "image/jpeg");
                }
                else if (!string.IsNullOrEmpty(categoryImageResult.ImageUrl))
                {
                    return Redirect(categoryImageResult.ImageUrl);
                }
            }

            return NotFound(categoryImageResult.Message);
        }


        [HttpPut("{categoryId}/update-picture")]
        public async Task<IActionResult> UpdateCategoryPicture([FromRoute] Guid categoryId, IFormFile newCategoryPicture)
        {
            try
            {
                var result = await categoryService.UpdateCategoryPictureAsync(categoryId, newCategoryPicture);

                if (result.IsSuccess)
                {
                    return Ok(new { message = result.Message });
                }

                return BadRequest(new { message = result.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                List<Category> category = await categoryService.RetrieveAllCategories();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            try
            {
                Category category = await categoryService.RetrieveCategoryById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateCategory([FromForm] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (updateCategoryDto.File.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\Images\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\Images\\");
                    }

                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\Images\\" + updateCategoryDto.File.FileName))
                    {
                        updateCategoryDto.File.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }

                // Get the filename of the uploaded file
                var fileName = Path.GetFileName(updateCategoryDto.File.FileName);
                var savedFilename = Guid.NewGuid().ToString();

                // TODO: Associate the fileName with the savedFilename
                // and probably the currently connected user in the database

                var path = Path.Combine(webHostEnvironment.WebRootPath + "\\Images\\", "Uploads", savedFilename);
                Directory.CreateDirectory(path);

                string imageName = fileName;

                (bool result, string message) = await categoryService.UpdateCategoryAsync(updateCategoryDto, imageName);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(Guid id)
        {
            try
            {
                (bool result, string message) = await categoryService.DeleteCategoryByIdAsync(id);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
