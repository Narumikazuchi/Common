namespace Narumikazuchi
{
    /// <summary>
    /// Defines a method to convert the implementing type to the type <typeparamref name="TType"/>.
    /// </summary>
    public interface IConvertible<TType>
    {
        /// <summary>
        /// Converts this instance into a new instance of <typeparamref name="TType"/> with the same value using the specified culture-specific formatting.
        /// </summary>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> implementation which provides culture-specific formatting.</param>
        /// <returns>A new instance of <typeparamref name="TType"/> with the same value as this instance</returns>
        internal protected TType ToType(System.IFormatProvider? provider);
    }
}
