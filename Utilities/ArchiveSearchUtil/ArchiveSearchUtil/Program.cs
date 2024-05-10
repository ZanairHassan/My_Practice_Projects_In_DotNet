using System;
using System.IO;
using System.IO.Compression;

namespace ArchiveSearchUtil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.LogText("================== Utility Started running ===========================");
            Logger.LogText($"Date:  {DateTime.Now.ToString("yyyyMMdd hh:mm:ss")}");
            ZipFileCheck objZipFileCheck = new ZipFileCheck();
            Console.WriteLine("Enter the complete folder path containing ZIP files: ");
            string folderPath = Console.ReadLine();
            SearchZip objSearch = new SearchZip();
            Console.WriteLine("Enter the file name to search within ZIP files: ");
            string searchFileName = Console.ReadLine();

            if (!Directory.Exists(folderPath))
            {
                objSearch.Log("Folder not found.");
                return;
            }

            var files = Directory.GetFiles(folderPath);
            Logger.LogText("this is a Zip Folder....");
            Logger.LogText("Searching the Rquired File in Zip Folder....");
            bool foundInAnyZip = false;
            foreach (var file in files)
            {

                if (objZipFileCheck.IsZipFile(file))
                {
                    bool found = objSearch.SearchFileInZip(file, searchFileName);
                    if(found)
                    {
                        foundInAnyZip = true;
                        
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
