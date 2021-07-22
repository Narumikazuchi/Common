using System;

namespace Narumikazuchi
{
    /// <summary>
    /// Converts types that implement the <see cref="IConvertible{TType}"/> interface.
    /// </summary>
    public static class Converter
    {
        #region Conversion

        /// <summary>
        /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TType"/> type. 
        /// </summary>
        /// <param name="convertible">The instance to convert.</param>
        /// <returns>A new instance of <typeparamref name="TType"/> with the 
        /// same value as the specified <typeparamref name="TConvertible"/></returns>
        public static TType ToType<TConvertible, TType>(TConvertible convertible) 
            where TConvertible : IConvertible<TType> =>
                ToType<TConvertible, TType>(convertible, null);
        /// <summary>
        /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TType"/> type 
        /// using the specified culture-specific formatting. 
        /// </summary>
        /// <param name="convertible">The instance to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> implementation which provides culture-specific formatting.</param>
        /// <returns>A new instance of <typeparamref name="TType"/> with the 
        /// same value as the specified <typeparamref name="TConvertible"/></returns>
        public static TType ToType<TConvertible, TType>(TConvertible convertible, IFormatProvider? provider)
            where TConvertible : IConvertible<TType> =>
                convertible.ToType(provider);

        #endregion
    }
}
