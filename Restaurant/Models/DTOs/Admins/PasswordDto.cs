namespace Restaurant.API.Models.DTOs.Admins
{
    public class PasswordDto
    {
        public Guid Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
