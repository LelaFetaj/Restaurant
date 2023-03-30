namespace Restaurant.API.Models.DTOs.Admins
{
    public class UpdateAdminDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
