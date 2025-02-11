using Microsoft.AspNetCore.Mvc;
using LoginAPI.Services;
using LoginAPI.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserLoginController : ControllerBase
{
    private readonly IUserLoginService _userLoginService;

    public UserLoginController(IUserLoginService userLoginService)
    {
        _userLoginService = userLoginService;
    }

   /* [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            var newUser = await _userLoginService.CreateUserAsync(user, user.PasswordHash);
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
*/
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _userLoginService.AuthenticateUserAsync(request.Email, request.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
