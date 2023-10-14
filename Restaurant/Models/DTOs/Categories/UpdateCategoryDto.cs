namespace Restaurant.API.Models.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; } //guid per id unike

        public string Title { get; set; }

        public bool Featured { get; set; }

        public bool Active { get; set; }

        public IFormFile File { get; set; }
    }
}
