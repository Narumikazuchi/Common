namespace Narumikazuchi;

/// <summary>
/// Defines a method to convert the implementing type to the type <typeparamref name="TType"/>.
/// </summary>
public interface IConvertible<TType>
{
    /// <summary>
    /// Converts this instance into a new instance of <typeparamref name="TType"/> with the same value using the specified culture-specific formatting.
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> implementation which provides culture-specific formatting.</param>
    /// <returns>A new instance of <typeparamref name="TType"/> with the same value as this instance</returns>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public TType ToType<TFormat>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<TFormat> provider)
            where TFormat : IFormatProvider;
}