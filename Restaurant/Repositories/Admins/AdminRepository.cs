using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Admins;
using BCrypt.Net;

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
            IQueryable<Admin> adminsToUpdate = SelectAllAdmins().Where(x => x.Position >= admin.Position);

            foreach (Admin adminToUpdate in adminsToUpdate)
            {
                adminToUpdate.Position++;
            }

            await _dbContext.Admin.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Admin> SelectAllAdmins()=>
            _dbContext.Admin.OrderBy(x => x.Position);

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

        public Task<bool> BCryptVerifyAsync(string givenPassword, string hashedPassword)
        {
            return Task.Run(() => BCrypt.Net.BCrypt.Verify(givenPassword, hashedPassword));
        }
    }
}
