using Restaurant.API.Models.Entities.Foods;

namespace Restaurant.API.Repositories.Foods
{
    public interface IFoodRepository
    {
        Task InsertFoodAsync(Food food);
        IQueryable<Food> SelectAllFoods();
        Task<Food> SelectFoodByIdAsync(Guid id);
        Task UpdateFoodAsync(Food food);
        Task DeleteFoodAsync(Food food);
    }
}
