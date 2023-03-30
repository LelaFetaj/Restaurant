using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Categories;

namespace Restaurant.API.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public CategoryRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertCategoryAsync(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Category> SelectAllCategories() =>
            _dbContext.Category;

        public async Task<Category> SelectCategoryByIdAsync(Guid id) =>
            await _dbContext.Category.FindAsync(id);

        public async Task UpdateCategoryAsync(Category category)
        {
            _dbContext.Category.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _dbContext.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
