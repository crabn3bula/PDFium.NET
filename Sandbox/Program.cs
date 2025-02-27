﻿using System;
using System.Drawing.Imaging;
using PDFium.NET;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            const string simpleFile = "example.pdf";
            const string destinationsFile = "example-dest.pdf";
            Console.WriteLine("PDFium.NET sandbox");

            var xDpi = 300;
            var yDpi = 300;

            using (var document = Document.Load(simpleFile))
            {
                Console.WriteLine($"Document {simpleFile} is opened");
                Console.WriteLine($"PDF version is {document.Version}");
                Console.WriteLine($"Title is {document.Title}");
                Console.WriteLine($"Author is {document.Author}");
                Console.WriteLine($"Subject is {document.Subject}");
                Console.WriteLine($"Keywords is {document.Keywords}");
                Console.WriteLine($"Creator is {document.Creator}");
                Console.WriteLine($"Producer is {document.Producer}");
                Console.WriteLine($"CreationDate is {document.CreationDate}");
                Console.WriteLine($"ModDate is {document.ModDate}");
                Console.WriteLine($"Trapped is {document.Trapped}");
                Console.WriteLine($"Pages count is {document.Pages.Count}");
                foreach (var page in document.Pages)
                {
                    var bitmap = page.Render(300, 300);
                    bitmap?.Save($"{simpleFile}-{page.Number}.png", ImageFormat.Png);
                    page.Thumbnail.Save($"{simpleFile}-{page.Number}-thumb.png", ImageFormat.Png);
                }
            }

            using (var document = Document.Load(destinationsFile))
            {
                Console.WriteLine($"Document {destinationsFile} is opened");
                Console.WriteLine($"PDF version is {document.Version}");
                Console.WriteLine($"Title is {document.Title}");
                Console.WriteLine($"Author is {document.Author}");
                Console.WriteLine($"Subject is {document.Subject}");
                Console.WriteLine($"Keywords is {document.Keywords}");
                Console.WriteLine($"Creator is {document.Creator}");
                Console.WriteLine($"Producer is {document.Producer}");
                Console.WriteLine($"CreationDate is {document.CreationDate}");
                Console.WriteLine($"ModDate is {document.ModDate}");
                Console.WriteLine($"Trapped is {document.Trapped}");
                Console.WriteLine($"Destinations count is {document.Destinations.Count}");
                foreach (var destination in document.Destinations)
                {
                    Console.WriteLine($"Destination with index {destination.Index}");
                }
            }

            Console.ReadLine();
        }
    }
}
