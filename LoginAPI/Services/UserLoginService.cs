using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoginAPI.Data;
using LoginAPI.Models;
using BCrypt.Net;

namespace LoginAPI.Services
{
    public interface IUserLoginService
    {
        Task<User> CreateUserAsync(User user, string password);
        Task<User> AuthenticateUserAsync(string email, string password);
    }

    public class UserLoginService : IUserLoginService
    {
        private readonly AppDbContext _context;

        public UserLoginService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Creare utilizator + Hash parola
        public async Task<User> CreateUserAsync(User user, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new Exception("User already exists.");

            Console.WriteLine($"Original password: {password}");
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            Console.WriteLine($"Hashed password: {hashedPassword}");

            user.PasswordHash = hashedPassword;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }


        // ✅ Autentificare utilizator + Salvare Log
        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            Console.WriteLine($"Attempting to authenticate user with email: {email}");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                throw new Exception("Invalid email or password.");
            }

            bool isSuccessful = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isSuccessful)
            {
                Console.WriteLine("Password mismatch.");
                throw new Exception("Invalid email or password.");
            }

            Console.WriteLine("Authentication successful.");
            // Adaugă aici salvarea logului de login
            var loginLog = new LoginLog
            {
                Email = email,
                IsSuccessful = isSuccessful,
                Timestamp = DateTime.UtcNow
            };
            _context.LoginLogs.Add(loginLog);
            await _context.SaveChangesAsync();
            
            if (!isSuccessful)
            {
                return null; // Nu aruncăm excepție, returnăm `null` pentru a trata elegant în controller
            }
            return user;
        }
    }
}
