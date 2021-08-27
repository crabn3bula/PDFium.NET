using System;
using System.Runtime.InteropServices;

namespace PDFium.NET.Native
{
    internal class DocumentHandle : SafeHandle
    {
        private DocumentHandle() : base(IntPtr.Zero, true) {}

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
