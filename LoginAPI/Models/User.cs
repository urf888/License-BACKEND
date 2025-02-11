
namespace LoginAPI.Models


{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Vom folosi hashing mai târziu
        public string Email { get; set; } = string.Empty;
    }
}
