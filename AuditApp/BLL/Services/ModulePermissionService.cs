using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ModulePermissionService : IModulePermissionService
    {
        private readonly AuditAppDBContext _auditDBContext;

        public ModulePermissionService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }

        public async Task<ModulePermissionVM> CreateModulePermssion(ModulePermissionVM modulePermissionVM)
        {
            if (modulePermissionVM == null)
            {
                throw new ArgumentNullException(nameof(modulePermissionVM), "modulePermissionVM cannot be null");
            }
            ModulePermission modulePermission = new ModulePermission();
            modulePermission.ModuleID = modulePermissionVM.ModuleID;
            modulePermission.UserID = modulePermissionVM.UserID;
            await _auditDBContext.ModulePermissions.AddAsync(modulePermission);
            await _auditDBContext.SaveChangesAsync();
            modulePermissionVM.ErrorMessage = string.Empty;
            modulePermissionVM.SuccessMessage = "modulePermission is created successfully";
            return modulePermissionVM;
        }

        public async Task<ModulePermission> DeleteModulePermission(int modulepermissionID)
        {
            var modulepermission = await _auditDBContext.ModulePermissions.FirstOrDefaultAsync(x => x.ModulePermissionID == modulepermissionID);
            if (modulepermission != null)
            {
                modulepermission.IsDeleted = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return modulepermission;
        }

        public async Task<List<ModulePermission>> GetAllModulePermission()
        {
            return await _auditDBContext.ModulePermissions.ToListAsync();
        }

        public async Task<ModulePermission> GetModulePermission(int modulepermissionID)
        {
            return await _auditDBContext.ModulePermissions.FirstOrDefaultAsync(x => x.ModulePermissionID == modulepermissionID);
        }

        public async Task<List<ModulePermission>> GetModulePermissionbyUserid(int userId)
        {
           return await _auditDBContext.ModulePermissions.Where(x=> x.UserID == userId).ToListAsync();
        }
        public async Task<ModulePermissionVM> UpdateModulePermission(int modulepermissionID, ModulePermissionVM modulePermissionVM)
        {
            var modulepermission = await _auditDBContext.ModulePermissions.FirstOrDefaultAsync(x => x.ModulePermissionID == modulepermissionID);
            if (modulepermission is null)
            {
                return null;
            }
            modulepermission.ModulePermissionID = modulePermissionVM.UserID;
            _auditDBContext.ModulePermissions.Update(modulepermission);
            await _auditDBContext.SaveChangesAsync();
            modulePermissionVM.ErrorMessage = string.Empty;
            modulePermissionVM.SuccessMessage = "ModPermission is created successfully";
            return modulePermissionVM;
        }


        public async Task<List<ModulePermission>> BulkDelete(List<int> modPermissionId)
        {
            var modPermission = await _auditDBContext.ModulePermissions.Where(f => modPermissionId.Contains(f.ModulePermissionID) && (f.IsDeleted == null || f.IsDeleted == false)).ToListAsync();
            foreach (var modPer in modPermission)
            {
                modPer.IsDeleted = true;
            }

            _auditDBContext.ModulePermissions.UpdateRange(modPermission);
            await _auditDBContext.SaveChangesAsync();
            return modPermission;
        }

    }
}
