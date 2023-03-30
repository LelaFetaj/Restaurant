using Restaurant.API.Models.DTOs.Foods;
using Restaurant.API.Models.Entities.Foods;

namespace Restaurant.API.Services.Foods
{
    public interface IFoodService
    {
        Task<(bool, string)> AddFoodAsync(CreateFoodDto createFoodDto);

        Task<List<Food>> RetrieveAllFoods();

        Task<Food> RetrieveFoodById(Guid id);

        Task<(bool, string)> UpdateFoodAsync(UpdateFoodDto updateFoodDto);

        Task<(bool, string)> DeleteFoodByIdAsync(Guid id);
    }
}
