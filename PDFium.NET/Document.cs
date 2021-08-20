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

        public Page GetPage(int pageIndex)
        {
            var pageHandle = Bindings.LoadPage(_handle, pageIndex);
            return new Page(pageHandle);
        }

        public void Dispose()
        {
            Bindings.CloseDocument(_handle);
            _handle.Dispose();
        }
    }
}
