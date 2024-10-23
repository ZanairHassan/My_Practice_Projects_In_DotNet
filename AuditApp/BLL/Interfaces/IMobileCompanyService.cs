using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IMobileCompanyService
    {
        Task<MobileCompanyVM> CreateMobileCompany(MobileCompanyVM incomeVM);
        Task<MobileCompany> GetMobileCompany(int mobilecompanyID);
        Task<List<MobileCompany>> GetAllMobileCompany();
        Task<MobileCompanyVM> UpdateMobileCompany(int mobilecompanyID, MobileCompanyVM mobilecompanyVM);
        Task<MobileCompany> DeleteMobileCompany(int mobilecompanyID);
        Task<List<MobileCompany>> BulkDelete(List<int> mobileCompanyId);
    }
}
