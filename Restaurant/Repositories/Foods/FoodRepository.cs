using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Foods;

namespace Restaurant.API.Repositories.Foods
{
    public class FoodRepository : IFoodRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public FoodRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertFoodAsync(Food food)
        {
            await _dbContext.Food.AddAsync(food);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Food> SelectAllFoods() =>
            _dbContext.Food;

        public async Task<Food> SelectFoodByIdAsync(Guid id) =>
            await _dbContext.Food.FindAsync(id);

        public async Task UpdateFoodAsync(Food food)
        {
            _dbContext.Food.Update(food);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFoodAsync(Food food)
        {
            _dbContext.Remove(food);
            await _dbContext.SaveChangesAsync();
        }

    }
}
