using System;
using System.Runtime.InteropServices;

namespace PDFium.NET.Native
{
    class DocumentHandle: SafeHandle
    {
        private DocumentHandle(IntPtr invalidHandleValue) : base(invalidHandleValue, true)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            Bindings.CloseDocument(this);
            return true;
        }
    }
}
