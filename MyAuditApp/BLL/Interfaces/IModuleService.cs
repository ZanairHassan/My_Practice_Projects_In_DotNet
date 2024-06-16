using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IModuleService
    {
        Task<Module> CreateModule(ModuleVM moduleVM);
        Task<List<Module>> GetAllModules();
        Task<Module> GetModule(int moduleId);
        Task<Module> UpdateModule(int moduleId,ModuleVM moduleVM);
        Task<Module> DeleteModule(int moduleId, ModuleVM moduleVM);
    }
}
