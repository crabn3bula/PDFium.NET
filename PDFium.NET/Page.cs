using System;
using PDFium.NET.Native;

namespace PDFium.NET
{
    public class Page : IDisposable
    {
        /// <summary>
        /// Document handle.
        /// </summary>
        private readonly DocumentHandle _documentHandle;

        /// <summary>
        /// Page number.
        /// </summary>
        private readonly int _number;

        /// <summary>
        /// Page handle instance.
        /// </summary>
        private readonly PageHandle _handle;

        /// <summary>
        /// Creates a page and loads it.
        /// </summary>
        /// <param name="documentHandle"></param>
        /// <param name="number"></param>
        internal Page(DocumentHandle documentHandle, int number)
        {
            _documentHandle = documentHandle;
            _number = number;
            _handle = Bindings.LoadPage(_documentHandle, number);
        }

        /// <summary>
        /// Closes page and release all handles.
        /// </summary>
        public void Dispose()
        {
            Bindings.ClosePage(_handle);
            _handle?.Dispose();
        }
    }
}
