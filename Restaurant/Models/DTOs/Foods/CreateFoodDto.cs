namespace Restaurant.API.Models.DTOs.Foods
{
    public class CreateFoodDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageName { get; set; }

        public bool Featured { get; set; }

        public bool Active { get; set; }

        public Guid CategoryId { get; set; }
    }
}
