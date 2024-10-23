using DAL;
using DAL.DBContext;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class LoggingUtlity
    {
        public static void LogText(string text, IConfiguration _configuration)
        {
            string fileName = "Data/ErrorLog.txt";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-----------------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(text);
            sb.AppendLine("-----------------------------------------------");
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
        public static async void ExLog(string text, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AuditAppDBContext>();
                var log = new ExceptionLog
                {
                    LogText = text,
                    TimeStamp = DateTime.Now,
                };

                await context.ExceptionLogs.AddAsync(log);
                await context.SaveChangesAsync();
            }
        }
    }
}
