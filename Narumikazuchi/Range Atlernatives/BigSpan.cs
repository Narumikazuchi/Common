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
    [XmlRoot(nameof(BigSpan))]
    public readonly struct BigSpan
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BigSpan"/>.
        /// </summary>
        /// <param name="begin">The starting point of the <see cref="BigSpan"/>.</param>
        /// <param name="end">The end point of the <see cref="BigSpan"/>.</param>
        public BigSpan(Int64 begin, Int64 end)
        {
            this.Begin = begin;
            this.End = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the starting point of the <see cref="BigSpan"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        [XmlAttribute(nameof(Begin))]
        public Int64 Begin { get; init; }
        /// <summary>
        /// Gets the end point of the <see cref="BigSpan"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        [XmlAttribute(nameof(End))]
        public Int64 End { get; init; }
        /// <summary>
        /// Gets the length of the <see cref="BigSpan"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        public Int64 Length => this.End - this.Begin;

        #endregion
    }
}
