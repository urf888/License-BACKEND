using BCrypt.Net;
using LoginAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LoginAPI.Services; // Importă serviciile necesare

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService; // 🔹 Adaugă această linie

        public AuthController(IUserLoginService userLoginService) // 🔹 Injectează serviciul în constructor
        {
            _userLoginService = userLoginService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            var user = await _userLoginService.AuthenticateUserAsync(loginModel.Email, loginModel.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var token = "your_generated_jwt_token";  // Placeholder pentru un token JWT

            return Ok(new { token, user });
        }

        // ✅ Adaugă endpoint-ul de înregistrare
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerModel)
        {
            if (registerModel == null || string.IsNullOrEmpty(registerModel.Email) ||
                string.IsNullOrEmpty(registerModel.Password) || string.IsNullOrEmpty(registerModel.Username))
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                var newUser = new User
                {
                    Username = registerModel.Username,
                    Email = registerModel.Email
                };

                var createdUser = await _userLoginService.CreateUserAsync(newUser, registerModel.Password);
                return Ok(new { message = "User registered successfully", user = createdUser });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
