using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(UserVM userVM);
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task<User> UpdateUser(int userId, UserVM userVM);
        Task<User> DeleteUser(int userId);
    }
}
