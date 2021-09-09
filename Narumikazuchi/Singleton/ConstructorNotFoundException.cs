using System;

namespace Narumikazuchi
{
    /// <summary>
    /// Exception thrown by <see cref="Singleton{T}"/> when derived type does not contain a non-public default constructor.
    /// </summary>
    public class ConstructorNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorNotFoundException"/> class.
        /// </summary>
        public ConstructorNotFoundException() :
            base(MESSAGE) 
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorNotFoundException"/> class.
        /// </summary>
        public ConstructorNotFoundException(String? auxMessage) : 
            base(String.Format("{0} - {1}", 
                               MESSAGE, 
                               auxMessage)) 
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorNotFoundException"/> class.
        /// </summary>
        public ConstructorNotFoundException(String? auxMessage, 
                                            Exception? inner) : 
            base(String.Format("{0} - {1}", 
                               MESSAGE, 
                               auxMessage), 
                inner) 
        { }

        private const String MESSAGE = "Singleton derived types require a non-public parameterless constructor.";
    }
}
