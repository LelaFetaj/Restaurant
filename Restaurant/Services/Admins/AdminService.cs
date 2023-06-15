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

            // Generate a random salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createAdminDto.Password, salt);


            Admin admin = new Admin
            {
                FullName = createAdminDto.FullName,
                Username = createAdminDto.Username,
                Password = hashedPassword
            };

            await adminRepository.InsertAdminAsync(admin);

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
                .AnyAsync(x => x.Username == updateAdminDto.Username && x.Id != updateAdminDto.Id);

            if (exists)
            {
                return (false, "An admin with the same username exists. Please choose another username!");
            }

            // Generate a random salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updateAdminDto.Password, salt);

            admin.Username = updateAdminDto.Username;
            admin.Password = hashedPassword;
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

        public async Task<(bool, string)> PasswordVerification(string storedPassword, string givenPassword)
        {

            // Perform the password comparison logic here
            // You can use a library or implement your own password hashing and comparison logic

            // Example using BCrypt.Net library
            bool passwordMatches = await adminRepository.BCryptVerifyAsync(givenPassword, storedPassword);

            return (passwordMatches, "Passwords match");
        }

        public async Task<(bool, string)> UpdatePasswordAsync(PasswordDto PasswordDto)
        {
            Admin admin = await adminRepository.SelectAdminByIdAsync(PasswordDto.Id);

            if (admin == null)
            {
                return (false, $"Couldn't find admin with id: {PasswordDto.Id}");
            }

            // Generate a random salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(PasswordDto.NewPassword, salt);

            admin.Password = hashedPassword;

            await adminRepository.UpdateAdminAsync(admin);

            return (true, "Password changed successfully");
        }
    }
}
