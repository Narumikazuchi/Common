using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Narumikazuchi
{
    /// <summary>
    /// Represents a numeric span.
    /// </summary>
    [DebuggerDisplay("Length = {Length}")]
    [Serializable]
    [XmlRoot(nameof(Span))]
    public readonly struct Span
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Span"/>.
        /// </summary>
        /// <param name="begin">The starting point of the <see cref="Span"/>.</param>
        /// <param name="end">The end point of the <see cref="Span"/>.</param>
        public Span(Int32 begin, 
                    Int32 end)
        {
            this.Begin = begin;
            this.End = end;
        }

        /// <summary>
        /// Gets the starting point of the <see cref="Span"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        [XmlAttribute(nameof(Begin))]
        public Int32 Begin { get; init; }
        /// <summary>
        /// Gets the end point of the <see cref="Span"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        [XmlAttribute(nameof(End))]
        public Int32 End { get; init; }
        /// <summary>
        /// Gets the length of the <see cref="Span"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Int32 Length => 
            this.End - this.Begin;
    }
}
