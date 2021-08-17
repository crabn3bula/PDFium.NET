using System;
using PDFium.NET.Native;

namespace PDFium.NET
{
    public class Document : IDisposable
    {
        private readonly DocumentHandle _handle;

        public int PagesCount { get; }

        public static Document Load(string filePath, string password)
        {
            Bindings.InitLibrary();
            return new Document(filePath, password);
        }

        private Document(string filePath, string password)
        {
            _handle = Bindings.LoadDocument(filePath, password);
            PagesCount = Bindings.GetPageCount(_handle);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}
