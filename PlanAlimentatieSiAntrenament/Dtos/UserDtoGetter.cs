namespace Dtos
{
    public class UserDtoGetter
    {
        public UserDtoGetter(string name, string surname, string email, string phNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phNumber;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}