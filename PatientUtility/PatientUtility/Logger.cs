using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientUtility
{
    public class Logger
    {
        public static void LogText(string text)
        {
            string path = ConfigurationManager.AppSettings["LogFolderPath"];
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
            }
            Console.WriteLine(text);
        }
    }
}
