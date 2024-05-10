
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientUtility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.LogText("================== Utility Started running ===========================");
            Logger.LogText($"Date:  {DateTime.Now.ToString("yyyyMMdd hh:mm:ss")}");
            CheckZipFile objCheckZipFile = new CheckZipFile();
            DatabaseClass objDatabaseClass = new DatabaseClass();
            Console.WriteLine("Enter the path that contains ZIP files: ");
            string folderPath = Console.ReadLine();
            SearchZipFileFromFolder objSearchZipFileFromFolder = new SearchZipFileFromFolder();
            Console.WriteLine("Enter the file name to search within ZIP files: ");
            string searchFileName = Console.ReadLine();
            if (!Directory.Exists(folderPath))
            {
                objSearchZipFileFromFolder.Log("Folder not found.");
                return;
            }
            var files = Directory.GetFiles(folderPath);
            Logger.LogText("this is a Zip Folder....");
            Logger.LogText("Searching the Required File in Zip Folder....");
            bool foundInAnyZip = false;
            foreach (var file in files)
            {

                if (objCheckZipFile.IsZipFile(file))
                {
                    bool found = objSearchZipFileFromFolder.SearchFileInZip(file, searchFileName);
                    if (found)
                    {
                        foundInAnyZip = true;
                        objDatabaseClass.InsertPatientData(Path.Combine(ConfigurationManager.AppSettings["SearchFileFolder"], file));
                    }
                }
                else
                {
                    Console.WriteLine("this is not a zip file " + file);
                }
            }
            if (!foundInAnyZip)
            {
                {
                    Logger.LogText($"But the File has not found in any zip file.");
                }
            }
            Logger.LogText("================== Utility Ended ==========================\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        
        }
    }
}