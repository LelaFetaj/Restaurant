using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models.DTOs.Admins;
using Restaurant.API.Models.Entities.Admins;
using Restaurant.API.Repositories.Admins;

namespace Restaurant.API.Services.Admins
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public async Task<(bool, string)> AddAdminAsync(CreateAdminDto createAdminDto)
        {
            bool exists = await adminRepository
                .SelectAllAdmins()
                .AnyAsync(x => x.Username == createAdminDto.Username);

            if (exists)
            {
                return (false, "An admin with the same username exists. Please choose another username!");
            }

            Admin admin = new Admin
            {
                FullName = createAdminDto.FullName,
                Username = createAdminDto.Username,
                Password = createAdminDto.Password
            };

            return (true, "Success");
        }

        public async Task<List<Admin>> RetrieveAllAdmins()
        {
            List<Admin> admins = await adminRepository
                .SelectAllAdmins()
                .ToListAsync();
        
            return admins;
        }

        public async Task<Admin> RetrieveAdminById(Guid id) =>
            await adminRepository.SelectAdminByIdAsync(id);

        public async Task<(bool, string)> UpdateAdminAsync(UpdateAdminDto updateAdminDto)
        {
            Admin admin = await adminRepository.SelectAdminByIdAsync(updateAdminDto.Id);

            if (admin == null)
            {
                return (false, $"Couldn't find admin with id: {updateAdminDto.Id}");
            }

            bool exists = await adminRepository
                .SelectAllAdmins()
                .AnyAsync(x => x.Username == updateAdminDto.Username);

            if (exists)
            {
                return (false, "An admin with the same username exists. Please choose another username!");
            }

            admin.Username = updateAdminDto.Username;
            admin.Password = updateAdminDto.Password;
            admin.FullName = updateAdminDto.FullName;
            await adminRepository.UpdateAdminAsync(admin);

            return (true, "Success");
        }

        public async Task<(bool, string)> DeleteAdminByIdAsync(Guid id)
        {
            Admin admin = await adminRepository.SelectAdminByIdAsync(id);

            if (admin == null)
            {
                return (false, $"Couldn't find admin with id: {id}");
            }

            await adminRepository.DeleteAdminAsync(admin);

            return (true, "Success");
        }
    }
}
