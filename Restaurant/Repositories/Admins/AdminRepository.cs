using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Admins;

namespace Restaurant.API.Repositories.Admins
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RestaurantDbContext dbContext;

        public AdminRepository(RestaurantDbContext dbContext)
        {
            dbContext = dbContext;
        }

        public async Task InsertAdminAsync(Admin admin)
        {
            await dbContext.Admin.AddAsync(admin);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<Admin> SelectAllAdmins() =>
            dbContext.Admin;

        public async Task<Admin> SelectAdminByIdAsync(Guid id) =>
            await dbContext.Admin.FindAsync(id);

        public async Task UpdateAdminAsync(Admin admin)
        {
            dbContext.Admin.Update(admin);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAdminAsync(Admin admin)
        {
            dbContext.Remove(admin);
            await dbContext.SaveChangesAsync();
        }
    }
}
