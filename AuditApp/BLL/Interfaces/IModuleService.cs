using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IModuleService
    {
        Task<ModuleVM> CreateModule(ModuleVM moduleVM);
        Task<Module> GetModule(int moduleID);
        Task<List<Module>> GetAllModule();
        Task<ModuleVM> UpdateModule(int moduleID, ModuleVM moduleVM);
        Task<Module> DeleteModule(int moduleID);
        Task<List<Module>> BulkDelete(List<int> moduleId);
    }
}
