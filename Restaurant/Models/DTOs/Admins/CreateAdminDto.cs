namespace Restaurant.API.Models.DTOs.Admins
{
    public class CreateAdminDto
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
