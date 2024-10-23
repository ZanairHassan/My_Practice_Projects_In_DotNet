using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AllowedURLService : IAllowedURLService
    {
        private readonly AuditAppDBContext _auditAppDBContext;

        public AllowedURLService(AuditAppDBContext auditAppDBContext)
        {
            _auditAppDBContext = auditAppDBContext;
        }

        public async Task<TBLAllowedURL> AddAllowedUrl(AllowedURLVM allowedUrlVM)
        {
            var allowedUrl = new TBLAllowedURL
            {
                AllowedUrl = allowedUrlVM.AllowedUrl,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                DeletedReason = null
            };

            await _auditAppDBContext.AllowedUrls.AddAsync(allowedUrl);
            await _auditAppDBContext.SaveChangesAsync();

            return allowedUrl;
        }

        public async Task<TBLAllowedURL> DeleteAllowedUrl(int id, string deletedReason)
        {
            var allowedUrl = await _auditAppDBContext.AllowedUrls.FindAsync(id);
            if (allowedUrl != null)
            {
                allowedUrl.IsDeleted = true;
                allowedUrl.DeletedReason = deletedReason;
                allowedUrl.ModifiedDate = DateTime.UtcNow;

                _auditAppDBContext.AllowedUrls.Update(allowedUrl);
                await _auditAppDBContext.SaveChangesAsync();
            }
            return allowedUrl;
        }

        public async Task<List<TBLAllowedURL>> GetAllAllowedUrl()
        {
            return await _auditAppDBContext.AllowedUrls.Where(u => u.IsDeleted==null).ToListAsync();
        }

        public async Task<TBLAllowedURL> GetAllowedUrlById(int id)
        {
            return await _auditAppDBContext.AllowedUrls.FirstOrDefaultAsync(u => u.AllowedUrlId == id);
        }

        public async Task<TBLAllowedURL> UpdateAllowedUrl(int id, AllowedURLVM allowedUrlVM)
        {
            var allowedUrl = await _auditAppDBContext.AllowedUrls.FindAsync(id);
            if (allowedUrl != null)
            {
                allowedUrl.AllowedUrl = allowedUrlVM.AllowedUrl;
                allowedUrl.ModifiedDate = DateTime.UtcNow;

                _auditAppDBContext.AllowedUrls.Update(allowedUrl);
                await _auditAppDBContext.SaveChangesAsync();
            }
            return allowedUrl;
        }

        public async Task<TBLAllowedURL> GetAllowedUrlByUrl(string url)
        {
            return await _auditAppDBContext.AllowedUrls.FirstOrDefaultAsync(u => u.AllowedUrl.Contains(url));
        }

        public async Task<bool> AddDeclinedIP(string ipAddress)
        {
            var declinedUrl = new DeclinedUrl
            {
                IPAddress = ipAddress,
                CreatedDate = DateTime.UtcNow
            };

            await _auditAppDBContext.DeclinedUrls.AddAsync(declinedUrl);
            await _auditAppDBContext.SaveChangesAsync();

            return true;
        }
    }
}
