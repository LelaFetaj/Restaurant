using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Foods;

namespace Restaurant.API.Repositories.Foods
{
    public class FoodRepository : IFoodRepository
    {
        private readonly RestaurantDbContext dbContext;

        public FoodRepository(RestaurantDbContext dbContext)
        {
            dbContext = dbContext;
        }

        public async Task InsertFoodAsync(Food food)
        {
            await dbContext.Food.AddAsync(food);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<Food> SelectAllFoods() =>
            dbContext.Food;

        public async Task<Food> SelectFoodByIdAsync(Guid id) =>
            await dbContext.Food.FindAsync(id);

        public async Task UpdateFoodAsync(Food food)
        {
            dbContext.Food.Update(food);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteFoodAsync(Food food)
        {
            dbContext.Remove(food);
            await dbContext.SaveChangesAsync();
        }

    }
}
