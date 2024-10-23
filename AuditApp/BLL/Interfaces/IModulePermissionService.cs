using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IModulePermissionService
    {
        Task<ModulePermissionVM> CreateModulePermssion(ModulePermissionVM modulePermissionVM);
        Task<ModulePermission> GetModulePermission(int modulepermissionID);
        Task<List<ModulePermission>> GetAllModulePermission();
        Task<ModulePermissionVM> UpdateModulePermission(int modulepermissionID, ModulePermissionVM modulePermissionVM);
        Task<ModulePermission> DeleteModulePermission(int modulepermissionID);
        Task<List<ModulePermission>> GetModulePermissionbyUserid(int userId);
        Task<List<ModulePermission>> BulkDelete(List<int> modPermissionId);


    }
}
