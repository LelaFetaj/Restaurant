using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.DTOs.Foods;
using Restaurant.API.Models.Entities.Foods;
using Restaurant.API.Services.Foods;

namespace Restaurant.API.Controllers.Foods
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService foodService;

        public FoodController(IFoodService foodService)
        {
            this.foodService = foodService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddFood(CreateFoodDto createFoodDto)
        {
            try
            {
                (bool result, string message) = await foodService.AddFoodAsync(createFoodDto);

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
        public async Task<ActionResult<List<Food>>> GetAllFoods()
        {
            try
            {
                List<Food> food = await foodService.RetrieveAllFoods();
                return Ok(food);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(Guid id)
        {
            try
            {
                Food food = await foodService.RetrieveFoodById(id);
                return Ok(food);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateFood(UpdateFoodDto updateFoodDto)
        {
            try
            {
                (bool result, string message) = await foodService.UpdateFoodAsync(updateFoodDto);

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
        public async Task<ActionResult<string>> DeleteFood(Guid id)
        {
            try
            {
                (bool result, string message) = await foodService.DeleteFoodByIdAsync(id);

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
