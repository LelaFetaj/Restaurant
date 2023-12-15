namespace Restaurant.API.Models.Entities.Categories
{
    public class CategoryImageResult
    {
        public bool IsSuccess { get; set; }
        public byte[] FileContent { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
    }
}
