using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        public int Number { get; }

        /// <summary>
        /// Page width.
        /// </summary>
        public float Width { get; }

        /// <summary>
        /// Page height.
        /// </summary>
        public float Height { get; }

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
            Number = number;
            _handle = Bindings.LoadPage(_documentHandle, number);
            Bindings.GetPageSizeByIndex(_documentHandle, number, out var width, out var height);
            Width = (float) width;
            Height = (float) height;
        }

        /// <summary>
        /// Renders page to bitmap.
        /// </summary>
        /// <param name="xDpi">Dpi value by x-axis.</param>
        /// <param name="yDpi">Dpi value by y-axis.</param>
        /// <param name="flags">Rendering flags</param>
        /// <returns>The bitmap.</returns>
        public Image Render(int xDpi, int yDpi, PageRenderingFlags flags = 0)
        {
            var width = (int) Width * xDpi / 72;
            var height = (int) Height * yDpi / 72;

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bitmap.SetResolution(xDpi, yDpi);

            var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            var bitmapHandle = Bindings.BitmapCreateEx(width, height, BitmapFormat.BGRA, data.Scan0, data.Stride);
            Bindings.BitmapFillRect(bitmapHandle, 0, 0, width, height, 0x00FFFFFF);

            Bindings.RenderPageBitmap(bitmapHandle, _handle, 0, 0, width, height, 0, flags);
            Bindings.BitmapDestroy(bitmapHandle);

            bitmap.UnlockBits(data);
            return bitmap;
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
