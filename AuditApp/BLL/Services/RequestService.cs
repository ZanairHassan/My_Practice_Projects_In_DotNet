using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RequestService : IRequestService


    {
        private readonly AuditAppDBContext _askaiiDBContext;
        private readonly ITokenService _tokenService;
        public RequestService(AuditAppDBContext askaiiDBContext, ITokenService tokenService)
        {
            _askaiiDBContext = askaiiDBContext;
            _tokenService = tokenService;

        }
        public string HandleUserRequest(int UserId)
        {
            var config = _askaiiDBContext.Configurations.SingleOrDefault(c => c.ConfigurationKey == "session_timeout");
            if (config == null)
            {
                throw new Exception("Configuration not found.");
            }

            TimeSpan requestInterval = ParseTime(config.ConfigurationValue);

            DateTime currentTime = DateTime.Now;

            // Get the last request from the user
            var lastRequest = _askaiiDBContext.UserRequests
                .Where(r => r.UserID == UserId)
                .OrderByDescending(r => r.LastRequestTime)
                .FirstOrDefault();
            if (lastRequest != null)
            {
                // Check if the last request is within the interval
                if (currentTime - lastRequest.LastRequestTime > requestInterval)
                {
                    _tokenService.InvalidatejwtTokenByID(UserId);
                    return "Session expired: Please login again.";
                }
            }
            // Save the new request
            var userRequest = new UserRequest
            {
                UserID = UserId,
                LastRequestTime = currentTime
            };
            _askaiiDBContext.UserRequests.Add(userRequest);
            _askaiiDBContext.SaveChanges();
            return "Request processed";
        }

        public async Task RemoveUserRequest(int Userid)
        {

            var requestsToRemove = await _askaiiDBContext.UserRequests.Where(ur => ur.UserID == Userid).ToListAsync();

            _askaiiDBContext.UserRequests.RemoveRange(requestsToRemove);

            await _askaiiDBContext.SaveChangesAsync();

        }

        private TimeSpan ParseTime(string time)
        {
            var timeParts = time.Split(' ');
            if (timeParts.Length != 2)
            {
                throw new ArgumentException("Invalid time format");
            }

            int value = int.Parse(timeParts[0]);
            string unit = timeParts[1].ToLower();

            return unit switch
            {
                "min" => TimeSpan.FromMinutes(value),
                "hour" => TimeSpan.FromHours(value),
                "day" => TimeSpan.FromDays(value),
                _ => throw new ArgumentException("Invalid time unit"),
            };
        }
    }
}