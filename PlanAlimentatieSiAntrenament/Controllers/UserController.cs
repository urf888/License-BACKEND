using models;
using Service;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-user-by-id")]
        public async Task<ActionResult<UserDtoGetter>> GetUserByID(int id)
        {
            var users = await _userService.GetUserByID(id);
            UserDtoGetter requestedUser = new UserDtoGetter(users.Name, users.Surname, users.Email, users.PhoneNumber);
            return (users == null) ? NotFound("No user found") : requestedUser;
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<List<UserDtoGetter>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            List<UserDtoGetter> requestedUsers = new List<UserDtoGetter>();
            
            for (int i = 0; i < users.Count; i++)
            {
                UserDtoGetter u = new UserDtoGetter(users[i].Name, users[i].Surname, users[i].Email, users[i].PhoneNumber);
                requestedUsers.Add(u);
            }
            return (requestedUsers == null) ? NotFound("No users found") : requestedUsers;
        }


        [HttpPut("edit-user")]
        public async Task<bool> EditUser([FromBody] UserEditRequest userDto)
        {
            User u = new User();
            u.IdUser = userDto.IdUser;
            u.Name = userDto.Name;
            u.Surname = userDto.Surname;
            u.PhoneNumber = userDto.PhoneNumber;
            u.Email = userDto.Email;

            if (u == null)
                return false;
            else
            {
                var result = await _userService.EditUser(u);
                return true;
            }
        }

        [HttpPut("change-password-user")]
        public async Task<bool> ChangeUserPassword([FromBody]ChangePasswordRequest req)
        {

            int id = req.Id;
            string oldPassword = req.OldPassword;
            string newPassword = req.NewPassword;

            var result = await _userService.ChangeUserPassword(id,oldPassword,newPassword);
            if (result == true)
                return true;
            else
                return false;

        }
        [HttpDelete("delete-User")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            User result = await _userService.CheckIfUserExists(id);

            if (result != null)
            {
                bool delete = await _userService.DeleteUser(result);
                return Ok(delete);
            }

            else
            {
                return BadRequest("failed to delete");
            }

        }
    }
}