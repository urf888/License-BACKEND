namespace Dtos
{
    public class UserEditRequest
    {
        public int? IdUser { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}