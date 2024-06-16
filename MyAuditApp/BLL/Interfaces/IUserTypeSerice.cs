using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserTypeSerice
    {
        Task<UserTypes> CreateUserTypes(UserTypeVm userTypeVM);
        Task<UserTypes> GetAllUserTypes();
        Task<UserTypes> GetUserType(int userTypesId);
        Task<UserTypes> UpdateUserTypes(int userTypesId,UserTypeVm userTypeVM);
        Task<UserTypes> DeleteUserTypes(int userTypesId);
    }
}
