using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFium.NET.Native
{
    class BitmapHandle : SafeHandle
    {
        private BitmapHandle() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
