using System;

namespace Narumikazuchi
{
    /// <summary>
    /// Exception thrown by <see cref="ISingleton{T}"/> when derived type does contain one or more public constructors.
    /// </summary>
    public sealed class PublicConstructorFoundException : Exception
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicConstructorFoundException"/> class.
        /// </summary>
        public PublicConstructorFoundException() : base(MESSAGE) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicConstructorFoundException"/> class.
        /// </summary>
        public PublicConstructorFoundException(String? auxMessage) : base(String.Format("{0} - {1}", MESSAGE, auxMessage)) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicConstructorFoundException"/> class.
        /// </summary>
        public PublicConstructorFoundException(String? auxMessage, Exception? inner) : base(String.Format("{0} - {1}", MESSAGE, auxMessage), inner) { }

        #endregion

        #region Constants

        private const String MESSAGE = "Singleton<T> derived types do not allow public constructors.";

        #endregion
    }
}
