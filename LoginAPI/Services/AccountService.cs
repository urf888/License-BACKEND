using BCrypt.Net;
using LoginAPI.Models;
using LoginAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace LoginAPI.Services
{   
    public interface IAccountService
    {
        Task<User> AuthenticateAsync(string username, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null; // Dacă nu există utilizator sau parola nu se potrivește
            }

            return user; // Returnează utilizatorul dacă autentificarea este validă
        }
    }
}
