using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface ISoftwareService
    {
        Task<SoftwareVM> CreateSoftware(SoftwareVM softwareVM);
        Task<Software> GetSoftware(int softwareID);
        Task<List<Software>> GetAllSoftware();
        Task<SoftwareVM> UpdateSoftware(int softwareID, SoftwareVM softwareVM);
        Task<Software> DeleteSoftware(int softwareID);
        Task<List<Software>> BulkDelete(List<int> softwareId);
    }
}
