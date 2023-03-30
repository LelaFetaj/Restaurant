using Restaurant.API.Models.DTOs.Admins;
using Restaurant.API.Models.Entities.Admins;

namespace Restaurant.API.Services.Admins
{
    public interface IAdminService
    {
        Task<(bool, string)> AddAdminAsync(CreateAdminDto createAdminDto);

        Task<List<Admin>> RetrieveAllAdmins();

        Task<Admin> RetrieveAdminById(Guid id);

        Task<(bool, string)> UpdateAdminAsync(UpdateAdminDto updateAdminDto);

        Task<(bool, string)> DeleteAdminByIdAsync(Guid id);
    }
}
