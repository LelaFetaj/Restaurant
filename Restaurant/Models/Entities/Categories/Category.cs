using Restaurant.API.Models.Entities.Foods;

namespace Restaurant.API.Models.Entities.Categories
{
    public class Category
    {
        public Guid Id { get; set; } //guid per id unike

        public string ImageName { get; set; }

        public string Title { get; set; }

        public bool Featured { get; set; }

        public bool Active { get; set; }

        public byte[] FileContent { get; set; }

        public virtual ICollection<Food> Foods { get; set; } //lidhja me tabelen tjeter per foreign key

    }
}
