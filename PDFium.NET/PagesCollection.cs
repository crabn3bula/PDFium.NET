using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using JetBrains.Annotations;
using PDFium.NET.Native;

namespace PDFium.NET
{
    /// <summary>
    /// A class to work with document pages.
    /// </summary>
    public class PagesCollection : IEnumerable<Page>, IDisposable
    {
        /// <summary>
        /// Document instance.
        /// </summary>
        [NotNull] private readonly DocumentHandle _documentHandle;

        /// <summary>
        /// Collection of document pages.
        /// </summary>
        [NotNull] private readonly List<Page> _pages;

        /// <summary>
        /// Initialized pages collection.
        /// </summary>
        /// <param name="document">Reference to document.</param>
        internal PagesCollection([NotNull] DocumentHandle document)
        {
            _documentHandle = document;
            var pagesCount = Bindings.GetPageCount(_documentHandle);
            _pages = new List<Page>(pagesCount);
            for (var pageNumber = 0; pageNumber < pagesCount - 1; ++pageNumber)
            {
                _pages.Add(new Page(_documentHandle, pageNumber));
            }
        }

        /// <summary>
        /// Page indexer with lazy initialization.
        /// </summary>
        /// <param name="index">Page index.</param>
        /// <returns>The page,</returns>
        public Page this[int index]=> _pages[index] ?? (_pages[index] = new Page(_documentHandle, index));

        /// <summary>
        /// Returns pages count.
        /// </summary>
        public int Count => _pages.Capacity;

        /// <summary>
        /// Disposes created resources.
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <inheritdoc />
        public IEnumerator<Page> GetEnumerator()
        {
            return _pages.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
