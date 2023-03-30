namespace Restaurant.API.Models.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; } //guid per id unike

        public string Title { get; set; }

        public string Featured { get; set; }

        public string Active { get; set; }
    }
}
