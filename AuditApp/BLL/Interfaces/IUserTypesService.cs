using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IUserTypesService
    {
        Task<UserTypesVM> CreateUserTypes(UserTypesVM usertypesVM);
        Task<UserTypes> GetUserTypes(int useertypesID);
        Task<List<UserTypes>> GetAllUserTypes();
        Task<UserTypesVM> UpdateUserTypes(int usertypesID, UserTypesVM usertypesVM);
        Task<UserTypes> DeleteUserTypes(int usertypesID);
        Task<List<UserTypes>> BulkDelete(List<int> userTypeId);
    }
}
