using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PDF_TO_JPG
{
    class Program
    {
        static IConfiguration Configuration;

        static void Main(string[] args)
        {
            // Setup configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure you have the correct using directives
                .AddJsonFile("App.config", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            string searchDirectory = Configuration["SearchFileFolder"];
            string outputBaseDirectory = Configuration["OutputDirectory"];

            if (!Directory.Exists(outputBaseDirectory))
            {
                Directory.CreateDirectory(outputBaseDirectory);
            }

            PdfConverterUtility.SearchAndConvertPdfs(searchDirectory, outputBaseDirectory);
        }
    }
}
