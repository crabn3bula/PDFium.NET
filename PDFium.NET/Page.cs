using System;
using PDFium.NET.Native;

namespace PDFium.NET
{
    public class Page : IDisposable
    {
        private readonly PageHandle _handle;

        internal Page(PageHandle handle)
        {
            _handle = handle;
        }

        public void Dispose()
        {
            Bindings.ClosePage(_handle);
            _handle.Dispose();
        }
    }
}
