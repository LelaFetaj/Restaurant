using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models.Entities.Admins;
using Restaurant.API.Models.Entities.Categories;
using Restaurant.API.Models.Entities.Foods;
using Restaurant.API.Models.Entities.Orders;

namespace Restaurant.API.Data.Contexts
{
    public class RestaurantDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public RestaurantDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this._configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetTableRelations(modelBuilder);
        }

        private static void SetTableRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Foods)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Food> Food { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
