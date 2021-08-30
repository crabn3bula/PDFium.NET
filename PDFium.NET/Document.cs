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
        /// Document's title.
        /// </summary>
        public string Title => Bindings.GetMetaText(_handle, DocumentMetaTag.Title);

        /// <summary>
        /// Document's author.
        /// </summary>
        public string Author => Bindings.GetMetaText(_handle, DocumentMetaTag.Author);

        /// <summary>
        /// Document's subject.
        /// </summary>
        public string Subject => Bindings.GetMetaText(_handle, DocumentMetaTag.Subject);

        /// <summary>
        /// Document's keywords.
        /// </summary>
        public string Keywords => Bindings.GetMetaText(_handle, DocumentMetaTag.Keywords);

        /// <summary>
        /// Document's creator.
        /// </summary>
        public string Creator => Bindings.GetMetaText(_handle, DocumentMetaTag.Creator);

        /// <summary>
        /// Document's producer.
        /// </summary>
        public string Producer => Bindings.GetMetaText(_handle, DocumentMetaTag.Producer);

        /// <summary>
        /// Document's creation date.
        /// </summary>
        public string CreationDate => Bindings.GetMetaText(_handle, DocumentMetaTag.CreationDate);

        /// <summary>
        /// Document's modification date.
        /// </summary>
        public string ModDate => Bindings.GetMetaText(_handle, DocumentMetaTag.ModDate);

        /// <summary>
        /// Document's trapped flag.
        /// </summary>
        public string Trapped => Bindings.GetMetaText(_handle, DocumentMetaTag.Trapped);

        /// <summary>
        /// PDF document version
        /// 14 for 1.4, 15 for 1.5, ...
        /// </summary>
        public int Version { get; }

        /// <summary>
        /// Collection of document pages.
        /// </summary>
        public PagesCollection Pages { get; }

        /// <summary>
        /// Collection of document destinations.
        /// </summary>
        public DestinationsCollection Destinations { get; }

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
            if (_handle == null)
            {
                throw new Exception("Cant load document");
            }

            Bindings.GetFileVersion(_handle, out var version);
            Version = version;
            Pages = new PagesCollection(_handle);
            Destinations = new DestinationsCollection(_handle);
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
            if (_handle == null)
            {
                throw new Exception("Cant load document");
            }

            Pages = new PagesCollection(_handle);
            Destinations = new DestinationsCollection(_handle);
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
