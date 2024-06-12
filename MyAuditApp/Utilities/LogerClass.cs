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
    public static class LogerClass
    {
        public static void LogText(string text,IConfiguration _configuration)
        {
            string path = _configuration["MessageFolderPath"];
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"Date: {DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")} |||| Message:   {text}");
            }
        }
        public static async void ExcLog(string text, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                var log = new ExceptionLog
                {
                    Logtext = text,
                    TimeStamp = DateTime.Now,
                };
                await context.ExceptionLogs.AddAsync(log);
                await context.SaveChangesAsync();
                
            }
        }
    }
}
