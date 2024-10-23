using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAllowedURLService
    {
        Task<TBLAllowedURL> AddAllowedUrl(AllowedURLVM allowedUrlVM);
        Task<TBLAllowedURL> DeleteAllowedUrl(int id, string deletedReason);
        Task<List<TBLAllowedURL>> GetAllAllowedUrl();
        Task<TBLAllowedURL> GetAllowedUrlById(int id);
        Task<TBLAllowedURL> UpdateAllowedUrl(int id, AllowedURLVM allowedUrlVM);
        Task<TBLAllowedURL> GetAllowedUrlByUrl(string url);

        Task<bool> AddDeclinedIP(string ipAddress);
    }
}
