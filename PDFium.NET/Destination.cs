using System;
using PDFium.NET.Native;

namespace PDFium.NET
{
    public class Destination : IDisposable
    {
        /// <summary>
        /// Document handle.
        /// </summary>
        private readonly DocumentHandle _documentHandle;

        /// <summary>
        /// Destination handle instance.
        /// </summary>
        private readonly DestinationHandle _handle;

        /// <summary>
        /// Destination index.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Creates a page and loads it.
        /// </summary>
        /// <param name="documentHandle"></param>
        /// <param name="number"></param>
        internal Destination(DocumentHandle documentHandle, int index)
        {
            _documentHandle = documentHandle;
            Index = index;

            // First time pass in |buffer| as NULL and get buflen.
            Bindings.GetNamedDestination(_documentHandle, index, out byte[] buffer, out var bufLen);
            // TODO: fix buffer length overflow.
            buffer = new byte[bufLen];

            // Second time pass in allocated |buffer| and buflen to retrieve |buffer|, which should be used as wchar_t*.
            _handle = Bindings.GetNamedDestination(_documentHandle, index, out buffer, out bufLen);
        }

        public void Dispose()
        {
        }
    }
}
