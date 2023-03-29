using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Categories;

namespace Restaurant.API.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantDbContext dbContext;

        public CategoryRepository(RestaurantDbContext dbContext)
        {
            dbContext = dbContext;
        }

        public async Task InsertCategoryAsync(Category category)
        {
            await dbContext.Category.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<Category> SelectAllCategories() =>
            dbContext.Category;

        public async Task<Category> SelectCategoryByIdAsync(Guid id) =>
            await dbContext.Category.FindAsync(id);

        public async Task UpdateCategoryAsync(Category category)
        {
            dbContext.Category.Update(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            dbContext.Remove(category);
            await dbContext.SaveChangesAsync();
        }
    }
}
