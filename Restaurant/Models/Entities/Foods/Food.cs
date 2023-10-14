using Restaurant.API.Models.Entities.Categories;

namespace Restaurant.API.Models.Entities.Foods
{
    public class Food
    {
        public Guid Id { get; set; } //guid per id unike

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageName { get; set; }

        public bool Featured { get; set; }

        public bool Active { get; set; }

        public Guid CategoryId { get; set; } //foreign key

        public virtual Category Category { get; set; } //lidhja me tablen tjeter per foreign key
    }
}
