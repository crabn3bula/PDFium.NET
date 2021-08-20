using System;
using System.Runtime.InteropServices;

namespace PDFium.NET.Native
{
    class PageHandle : SafeHandle
    {
        private PageHandle() : base(IntPtr.Zero, true) {}

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
