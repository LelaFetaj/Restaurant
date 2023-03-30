using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.DTOs.Categories;
using Restaurant.API.Models.Entities.Categories;
using Restaurant.API.Services.Categories;

namespace Restaurant.API.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddCategory(CreateCategoryDto createCategoryDto)
        {
            try
            {
                (bool result, string message) = await categoryService.AddCategoryAsync(createCategoryDto);

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
        public async Task<ActionResult<string>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                (bool result, string message) = await categoryService.UpdateCategoryAsync(updateCategoryDto);

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
