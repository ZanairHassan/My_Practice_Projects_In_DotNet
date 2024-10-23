using DAL.Models;
using DAL.ViewModels;
namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(UserVM userVM);
        Task<User> GetUser(int useerID);
        Task<List<User>> GetAllUser();
        Task<User> UpdateUser(int userID, UserVM userVM);
        Task<User> DeleteUser(int userID);
       Task<User> GetUserByEmail(string email);
        Task<User> GetUserByName(string name);
        Task<UserVM> RegisterUser(string usertype, UserVM user);
        Task<User> BlockUser(int userID);
        Task<User> Login(LoginVM loginVm);
        Task<ChangePasswordVM> ChangePassword(ChangePasswordVM changePasswordVM);
        Task<User> GuestUser(GuestUserVM guestUserVM);
        Task<User> GetUserByToken(string jwtToken);
        Task<List<User>> BulkActive(List<int> userId, bool isActive);
        Task<List<User>> BulkDeleteUser(List<int> usersId);
        Task<List<User>> GetAllBlockUsers();
    }
}
