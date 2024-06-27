using Microsoft.Extensions.Configuration;

namespace PDF_TO_JPG
{
    class Program
    {
        static IConfiguration Configuration;

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide the path to the PDF file.");
                return;
            }

            // Setup configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("App.config", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            string pdfFilePath = args[0];
            string outputDirectory = Configuration["OutputDirectory"];

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            PdfConverterUtility.ConvertPdfToJpg(pdfFilePath, outputDirectory);
        }
    }
}