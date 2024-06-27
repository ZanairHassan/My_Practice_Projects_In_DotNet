using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_TO_JPG
{
    public static class PdfConverterUtility
    {
        public static  void ConvertPdfToJpg(string pdfPath, string outputDirectory)
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
    }
}
