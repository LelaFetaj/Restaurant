using Restaurant.API.Models.Entities.Foods;

namespace Restaurant.API.Models.Entities.Categories
{
    public class Category
    {
        public Guid Id { get; set; } //guid per id unike

        public string Title { get; set; }

        public string Featured { get; set; }

        public string Active { get; set; }

        public virtual ICollection<Food> Foods { get; set; } //lidhja me tabelen tjeter per foreign key

    }
}
