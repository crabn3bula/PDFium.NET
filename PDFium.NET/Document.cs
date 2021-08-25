using System;
using System.IO;
using JetBrains.Annotations;
using PDFium.NET.Native;

namespace PDFium.NET
{
    /// <summary>
    /// Represents a PDF document.
    /// </summary>
    public class Document : IDisposable
    {
        /// <summary>
        /// Handle to opened pdf document.
        /// </summary>
        private readonly DocumentHandle _handle;

        /// <summary>
        /// Handles opened stream.
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Gets the count of PDF document pages. 
        /// </summary>
        public int PagesCount { get; }

        /// <summary>
        /// Loads PDF document from file path.
        /// </summary>
        /// <param name="filePath">Path to PDF file.</param>
        /// <returns>The new document.</returns>
        [PublicAPI]
        public static Document Load(string filePath)
        {
            return Load(filePath, null);
        }

        /// <summary>
        /// Loads PDF document with password protection from file path.
        /// </summary>
        /// <param name="filePath">Path to PDF file.</param>
        /// <param name="password">Optional password for PDF file.</param>
        /// <returns>The new document.</returns>
        [PublicAPI]
        public static Document Load(string filePath, string password)
        {
            return new Document(filePath, password);
        }

        /// <summary>
        /// Loads PDF document from stream.
        /// </summary>
        /// <param name="dataStream">PDF stream.</param>
        /// <returns>The new document.</returns>
        [PublicAPI]
        public static Document Load(Stream dataStream)
        {
            return new Document(dataStream, null);
        }

        /// <summary>
        /// Loads PDF document with password protection from stream.
        /// </summary>
        /// <param name="dataStream">PDF stream.</param>
        /// <param name="password">Optional password for PDF file.</param>
        /// <returns>The new document.</returns>
        [PublicAPI]
        public static Document Load(Stream dataStream, string password)
        {
            return new Document(dataStream, password);
        }

        /// <summary>
        /// Creates new instance and loads pdf document from path.
        /// </summary>
        /// <param name="filePath">Path to PDF file.</param>
        /// <param name="password">Optional password for PDF file.</param>
        private Document(string filePath, string password)
        {
            Bindings.InitLibrary();
            _handle = Bindings.LoadDocument(filePath, password);
            PagesCount = Bindings.GetPageCount(_handle);
        }

        /// <summary>
        /// Creates new instance and loads pdf document from path.
        /// </summary>
        /// <param name="dataStream">PDF stream.</param>
        /// <param name="password">Optional password for PDF file.</param>
        private Document(Stream dataStream, string password)
        {
            if (dataStream == null)
            {
                throw new ArgumentNullException(nameof(dataStream), "Data stream cant be null");
            }

            _stream = new MemoryStream();
            dataStream.CopyTo(_stream);

            Bindings.InitLibrary();
            // TODO: handle stream
            //_handle = Bindings.LoadMemDocument(_stream, _stream.Length, password);
            PagesCount = Bindings.GetPageCount(_handle);
        }

        /// <summary>
        /// Loads document page by index and returns it.
        /// </summary>
        /// <param name="pageIndex">Requested document's page index.</param>
        /// <returns>The new page.</returns>
        public Page GetPage(int pageIndex)
        {
            var pageHandle = Bindings.LoadPage(_handle, pageIndex);
            return new Page(pageHandle);
        }

        /// <summary>
        /// Closes pdf document and releases all handles.
        /// </summary>
        public void Dispose()
        {
            Bindings.CloseDocument(_handle);
            Bindings.DestroyLibrary();
            _handle?.Dispose();
            _stream?.Dispose();
        }
    }
}
