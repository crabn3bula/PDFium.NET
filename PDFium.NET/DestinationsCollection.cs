using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using PDFium.NET.Native;

namespace PDFium.NET
{
    /// <summary>
    /// A class to work with document destinations.
    /// </summary>
    public class DestinationsCollection : IEnumerable<Destination>, IDisposable
    {
        /// <summary>
        /// Document instance.
        /// </summary>
        [NotNull] private readonly DocumentHandle _documentHandle;

        /// <summary>
        /// Document destinations.
        /// </summary>
        [NotNull] private readonly List<Destination> _destinations;

        internal DestinationsCollection([NotNull] DocumentHandle documentHandle)
        {
            _documentHandle = documentHandle;
            var destinationsCount = Bindings.CountNamedDestinations(_documentHandle);
            _destinations = new List<Destination>(destinationsCount);
            for (var destinationIndex = 0; destinationIndex < destinationsCount; ++destinationIndex)
            {
                _destinations.Add(new Destination(_documentHandle, destinationIndex));
            }
        }

        /// <summary>
        /// Page indexer with lazy initialization.
        /// </summary>
        /// <param name="index">Page index.</param>
        /// <returns>The page,</returns>
        public Destination this[int index] => _destinations[index] ?? (_destinations[index] = new Destination(_documentHandle, index));

        /// <summary>
        /// Returns pages count.
        /// </summary>
        public int Count => _destinations.Capacity;


        public IEnumerator<Destination> GetEnumerator()
        {
            return _destinations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
   
        }
    }
}
