using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFium.NET.Native
{
    class DestinationHandle : SafeHandle
    {
        public DestinationHandle() : base(IntPtr.Zero, true) {}

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
