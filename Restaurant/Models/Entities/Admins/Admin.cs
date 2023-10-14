namespace Restaurant.API.Models.Entities.Admins
{
    public class Admin
    {
        public Guid Id { get; set; } //guid per id unike

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Position { get; set; }
    }
}
