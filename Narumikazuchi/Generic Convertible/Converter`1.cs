namespace Narumikazuchi;

/// <summary>
/// Converts types that implement the <see cref="IConvertible{TType}"/> interface.
/// </summary>
static public class Converter<TResult>
{
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public TResult ToType<TConvertible>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<TConvertible> convertible)
            where TConvertible : IConvertible<TResult>
    {
        TConvertible value = convertible;
        return value.ToType(default(MaybeNull<IFormatProvider>));
    }
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type 
    /// using the specified culture-specific formatting. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> implementation which provides culture-specific formatting.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public TResult ToType<TConvertible, TFormat>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<TConvertible> convertible,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        MaybeNull<TFormat> provider)
            where TConvertible : IConvertible<TResult>
            where TFormat : IFormatProvider
    {
        TConvertible value = convertible;
        return value.ToType<TFormat>(provider);
    }
}