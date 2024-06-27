using PdfiumViewer;
using System;
using System.Drawing.Imaging;
using System.IO;

namespace PDF_TO_JPG
{
    public static class PdfConverterUtility
    {
        public static void ConvertPdfToJpg(string pdfPath, string outputDirectory)
        {
            using (var document = PdfDocument.Load(pdfPath))
            {
                for (int pageIndex = 0; pageIndex < document.PageCount; pageIndex++)
                {
                    using (var page = document.Render(pageIndex, 300, 300, true))
                    {
                        string outputFilePath = Path.Combine(outputDirectory, $"Page-{pageIndex + 1}.jpg");
                        page.Save(outputFilePath, ImageFormat.Jpeg);
                        Console.WriteLine($"Saved: {outputFilePath}");
                    }
                }
            }
        }

        public static void SearchAndConvertPdfs(string searchDirectory, string outputBaseDirectory)
        {
            var pdfFiles = Directory.GetFiles(searchDirectory, "*.pdf", SearchOption.AllDirectories);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string outputDirectory = Path.Combine(outputBaseDirectory, timestamp);

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            foreach (var pdfFile in pdfFiles)
            {
                ConvertPdfToJpg(pdfFile, outputDirectory);
            }
        }
    }
}
