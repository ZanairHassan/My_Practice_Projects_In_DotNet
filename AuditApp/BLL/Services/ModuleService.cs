using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Services
{
    public class ModuleService : IModuleService
    {
        private readonly AuditAppDBContext _auditDBContext;
        public ModuleService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }

        public async Task<ModuleVM> CreateModule(ModuleVM moduleVM)
        {
            if (moduleVM == null)
            {
                throw new ArgumentNullException(nameof(moduleVM), "moduleVM cannot be null");
            }
            Module module = new Module();
            module.ModuleName = moduleVM.ModuleName;
            await _auditDBContext.Modules.AddAsync(module);
            await _auditDBContext.SaveChangesAsync();
            moduleVM.ErrorMessage = string.Empty;
            moduleVM.SuccessMessage = "Module is created successfully";
            return moduleVM;
        }

        public async Task<Module> DeleteModule(int moduleID)
        {
            var module = await _auditDBContext.Modules.FirstOrDefaultAsync(x => x.ModuleID == moduleID);
            if (module != null)
            {
                module.IsDeleted = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return module;
        }
        public async Task<List<Module>> GetAllModule()
        {
            return await _auditDBContext.Modules.ToListAsync();
        }

        public async Task<Module> GetModule(int moduleID)
        {
            return await _auditDBContext.Modules.FirstOrDefaultAsync(x => x.ModuleID == moduleID);
        }

        public async Task<ModuleVM> UpdateModule(int moduleID, ModuleVM moduleVM)
        {
            var module = await _auditDBContext.Modules.FirstOrDefaultAsync(x => x.ModuleID == moduleID);
            if (module is null)
            {
                return null;
            }
            module.ModuleName = moduleVM.ModuleName;
            _auditDBContext.Modules.Update(module);
            await _auditDBContext.SaveChangesAsync();
            moduleVM.ErrorMessage = string.Empty;
            moduleVM.SuccessMessage = "Module is created successfully";
            return moduleVM;
        }

        public async Task<List<Module>> BulkDelete(List<int> moduleId)
        {
            var modules = await _auditDBContext.Modules.Where(f => moduleId.Contains(f.ModuleID) && (f.IsDeleted == null || f.IsDeleted == false)).ToListAsync();
            foreach (var module in modules)
            {
                module.IsDeleted = true;
            }

            _auditDBContext.Modules.UpdateRange(modules);
            await _auditDBContext.SaveChangesAsync();
            return modules;
        }

    }
}
