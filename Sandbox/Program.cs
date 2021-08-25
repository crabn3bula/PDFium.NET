using System;
using PDFium.NET;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            const string testFile = "example.pdf";
            Console.WriteLine("PDFium.NET sandbox");

            using (var document = Document.Load(testFile, ""))
            {
                using (var page = document.GetPage(0))
                {

                }
                Console.WriteLine($"Document {testFile} is opened");
                Console.WriteLine($"Pages count is {document.PagesCount}");
            }

            Console.ReadLine();
        }
    }
}
