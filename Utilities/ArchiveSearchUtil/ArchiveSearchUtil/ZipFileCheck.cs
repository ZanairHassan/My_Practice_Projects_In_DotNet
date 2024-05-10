using System;
using System.IO;
using System.IO.Compression;

namespace ArchiveSearchUtil
{
    internal class ZipFileCheck
    {
       public bool IsZipFile(string filePath)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(filePath))
                {
                    return true;
                }
            }
            catch (InvalidDataException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
