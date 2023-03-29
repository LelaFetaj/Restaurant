using Restaurant.API.Models.Entities.Admins;

namespace Restaurant.API.Repositories.Admins
{
    public interface IAdminRepository
    {
        Task InsertAdminAsync(Admin admin);
        IQueryable<Admin> SelectAllAdmins();
        Task<Admin> SelectAdminByIdAsync(Guid id);
        Task UpdateAdminAsync(Admin admin);
        Task DeleteAdminAsync(Admin admin);
    }
}
