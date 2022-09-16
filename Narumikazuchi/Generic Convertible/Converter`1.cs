namespace Narumikazuchi;

/// <summary>
/// Converts types that implement the <see cref="IConvertible{TType}"/> interface.
/// </summary>
public static partial class Converter<TResult>
{
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static TResult ToType<TConvertible>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        TConvertible convertible) 
            where TConvertible : IConvertible<TResult> =>
                ToTypeInternal(convertible: convertible, 
                               provider: (IFormatProvider?)null);
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type 
    /// using the specified culture-specific formatting. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> implementation which provides culture-specific formatting.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static TResult ToType<TConvertible, TFormat>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        TConvertible convertible,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        TFormat provider)
            where TConvertible : IConvertible<TResult>
            where TFormat : IFormatProvider =>
                ToTypeInternal(convertible: convertible,
                               provider: provider);
}

// Non-Public
partial class Converter<TResult>
{
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    private static TResult ToTypeInternal<TConvertible, TFormat>(TConvertible convertible,
                                                                 TFormat? provider)
        where TConvertible : IConvertible<TResult>
        where TFormat : IFormatProvider
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(convertible);
#else
        if (convertible is null)
        {
            throw new ArgumentNullException(nameof(convertible));
        }
#endif

        return convertible.ToType(provider);
    }
}