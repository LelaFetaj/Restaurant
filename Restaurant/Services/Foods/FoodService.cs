using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models.DTOs.Foods;
using Restaurant.API.Models.Entities.Foods;
using Restaurant.API.Repositories.Foods;

namespace Restaurant.API.Services.Foods
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }

        public async Task<(bool, string)> AddFoodAsync(CreateFoodDto createFoodDto)
        {
            bool exists = await foodRepository
                .SelectAllFoods()
                .AnyAsync(x => x.Title == createFoodDto.Title);

            if (exists)
            {
                return (false, "A food with the same title exists. Please choose another title!");
            }

            Food food = new Food
            {
                Title = createFoodDto.Title,
                Description = createFoodDto.Description,
                Price = createFoodDto.Price,
                ImageName = createFoodDto.ImageName,
                Featured = createFoodDto.Featured,
                Active = createFoodDto.Active,
                CategoryId = createFoodDto.CategoryId
            };

            await foodRepository.InsertFoodAsync(food);

            return (true, "Success");
        }

        public async Task<List<Food>> RetrieveAllFoods()
        {
            List<Food> foods = await foodRepository
                .SelectAllFoods()
                .ToListAsync();

            return foods;
        }

        public async Task<Food> RetrieveFoodById(Guid id) =>
            await foodRepository.SelectFoodByIdAsync(id);

        public async Task<(bool, string)> UpdateFoodAsync(UpdateFoodDto updateFoodDto)
        {
            Food food = await foodRepository.SelectFoodByIdAsync(updateFoodDto.Id);

            if (food == null)
            {
                return (false, $"Couldn't find food with id: {updateFoodDto.Id}");
            }

            bool exists = await foodRepository
                .SelectAllFoods()
                .AnyAsync(x => x.Title == updateFoodDto.Title && x.Id != updateFoodDto.Id);

            if (exists)
            {
                return (false, "An food with the same title exists. Please choose another title!");
            }

            food.Title = updateFoodDto.Title;
            food.Description = updateFoodDto.Description;
            food.Price = updateFoodDto.Price;
            food.ImageName = updateFoodDto.ImageName;
            food.Featured = updateFoodDto.Featured;
            food.Active = updateFoodDto.Active;
            food.CategoryId = updateFoodDto.CategoryId;

            await foodRepository.UpdateFoodAsync(food);

            return (true, "Success");
        }

        public async Task<(bool, string)> DeleteFoodByIdAsync(Guid id)
        {
            Food food = await foodRepository.SelectFoodByIdAsync(id);

            if (food == null)
            {
                return (false, $"Couldn't find food with id: {id}");
            }

            await foodRepository.DeleteFoodAsync(food);

            return (true, "Success");
        }
    }
}
