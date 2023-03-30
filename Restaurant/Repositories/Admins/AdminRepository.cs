using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Admins;

namespace Restaurant.API.Repositories.Admins
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public AdminRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertAdminAsync(Admin admin)
        {
            await _dbContext.Admin.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Admin> SelectAllAdmins()=>
            _dbContext.Admin;

        public async Task<Admin> SelectAdminByIdAsync(Guid id) =>
            await _dbContext.Admin.FindAsync(id);

        public async Task UpdateAdminAsync(Admin admin)
        {
            _dbContext.Admin.Update(admin);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAdminAsync(Admin admin)
        {
            _dbContext.Remove(admin);
            await _dbContext.SaveChangesAsync();
        }
    }
}
