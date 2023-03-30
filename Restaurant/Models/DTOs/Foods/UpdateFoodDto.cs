namespace Restaurant.API.Models.DTOs.Foods
{
    public class UpdateFoodDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageName { get; set; }

        public string Featured { get; set; }

        public string Active { get; set; }

        public Guid CategoryId { get; set; }
    }
}
