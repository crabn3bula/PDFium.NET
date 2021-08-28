using System;
using System.Drawing.Imaging;
using PDFium.NET;
using PDFium.NET.Native;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            const string testFile = "example.pdf";
            Console.WriteLine("PDFium.NET sandbox");

            var xDpi = 300;
            var yDpi = 300;

            using (var document = Document.Load(testFile))
            {
                Console.WriteLine($"Document {testFile} is opened");
                Console.WriteLine($"Pages count is {document.Pages.Count}");
                foreach (var page in document.Pages)
                {
                    var bitmap = page.Render(300, 300);
                    bitmap?.Save($"{testFile}-{page.Number}.png", ImageFormat.Png);
                }
            }

            Console.ReadLine();
        }
    }
}
