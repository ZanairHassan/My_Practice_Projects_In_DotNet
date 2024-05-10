using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientUtility
{
    internal class SearchZipFileFromFolder
    {
        private string logFolderPath;
        private string searchFileFolder;
        DatabaseClass objdatabaseClass=new DatabaseClass();
        public void InitializeLoggings()
        {
            string logFolder = ConfigurationManager.AppSettings["LogFolderPath"];
            //Directory.CreateDirectory(logFolder);
            logFolderPath = logFolder;
        }
        public void SearchZipFolder()
        {
            string searchFolder = ConfigurationManager.AppSettings["SearchFileFolder"];
            searchFileFolder = searchFolder;
        }
        public void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFolderPath, true))
                {
                    //writer.WriteLine($"{DateTime.Now} - {message}");
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"This file has not found: {ex.Message}");
            }
        }
        public bool SearchFileInZip(string zipFilePath, string searchFileName)
        {
            try
            {
                if (!File.Exists(zipFilePath))
                {
                    Log("Zip file not found.");
                    return false;
                }

                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    try
                    {
                        InitializeLoggings();
                        SearchZipFolder();
                        var currentTimeFolder = DateTime.Now.ToString("yyyyMMddHHmmss");
                        var destinFolder = Path.Combine(searchFileFolder, currentTimeFolder);
                        Directory.CreateDirectory(destinFolder);
                        Log($"\n {currentTimeFolder} \n {destinFolder} \n This is done.");
                        bool found = false;

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName.ToLower().Contains(searchFileName.ToLower()))
                            {
                                found = true;
                                var destinFile = Path.Combine(destinFolder, entry.Name);
                                entry.ExtractToFile(destinFile);
                                objdatabaseClass.InsertPatientData(destinFile);
                                Logger.LogText("Searched File Found and Extracted.... ");

                            }
                        }
                        if (found)
                        {
                            return true;
                        }
                    }
                    catch (InvalidDataException ex)
                    {
                        Log($"maybe the file is cruppeted: {ex.Message}");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Log($"the file did not exist {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Log($"the file is not supported for the purpose: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Log($"Unauthorized access: {ex.Message}");
            }
            Log($"File {searchFileName} has not found in {zipFilePath}");
            return false;
        }

    }
}


