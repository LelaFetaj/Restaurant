using Restaurant.API.Models.Entities.Categories;

namespace Restaurant.API.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task InsertCategoryAsync(Category category);
        IQueryable<Category> SelectAllCategories();
        Task<Category> SelectCategoryByIdAsync(Guid id);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}
