using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.DTOs.Categories;
using Restaurant.API.Models.Entities.Categories;

namespace Restaurant.API.Services.Categories
{
    public interface ICategoryService
    {
        Task<(bool, string)> AddCategoryAsync(CreateCategoryDto createCategoryDto);

        Task<List<Category>> RetrieveAllCategories();

        Task<Category> RetrieveCategoryById(Guid id);

        Task<(bool, string)> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, string imageName);

        Task<(bool, string)> DeleteCategoryByIdAsync(Guid id);

        Task<CategoryImageResult> GetCategoryImage(Guid categoryId, int maxWidth = 800, int maxHeight = 600);
        Task<CategoryImageResult> UpdateCategoryPictureAsync(Guid categoryId, IFormFile newCategoryPicture);
    }
}
