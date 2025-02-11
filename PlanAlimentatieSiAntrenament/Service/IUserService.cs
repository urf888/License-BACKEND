using models;
using data;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        public Task<User> GetUserByID(int id);
        public Task<List<User>> GetAllUsers();
        public Task<User> EditUser(User user);
        public Task<User> CheckIfUserExists(int? id);
        public Task<bool> ChangeUserPassword(int idUser, string oldPassword, string newPassword, object hasher);
        public Task<bool> DeleteUser(User u);

    }
}