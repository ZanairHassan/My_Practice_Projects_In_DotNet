using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMobileCompanyService
    {
        Task<MobileCompany> CreateMobileCompany(MobileCompanyVM mobileCompanyVM);
        Task<List<MobileCompany>> GetAllMobileCompanies();
        Task<MobileCompany> GetMobileCompany(int mobileCompanyId);
        Task<MobileCompany> UpdateMobileCmpany(int mobileCompanyId, MobileCompanyVM mobileCompanyVM);
        Task<MobileCompany> DeleteMobileCompany(int mobileCompanyId);
    }
}
