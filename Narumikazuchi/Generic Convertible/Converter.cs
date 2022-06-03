namespace Narumikazuchi;

/// <summary>
/// Converts types that implement the <see cref="IConvertible{TType}"/> interface.
/// </summary>
public static partial class Converter
{
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
    [Pure]
    [return: NotNull]
    public static TResult ToType<TConvertible, TResult>([DisallowNull] TConvertible convertible) 
        where TConvertible : IConvertible<TResult> =>
            ToTypeInternal<TConvertible, TResult>(convertible: convertible, 
                                                provider: null);
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type 
    /// using the specified culture-specific formatting. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> implementation which provides culture-specific formatting.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
    [Pure]
    [return: NotNull]
    public static TResult ToType<TConvertible, TResult>([DisallowNull] TConvertible convertible, 
                                                        [DisallowNull] IFormatProvider provider)
        where TConvertible : IConvertible<TResult> =>
            ToTypeInternal<TConvertible, TResult>(convertible: convertible,
                                                provider: provider);
}

// Non-Public
partial class Converter
{
    [return: NotNull]
    private static TResult ToTypeInternal<TConvertible, TResult>(TConvertible convertible,
                                                                 IFormatProvider? provider)
        where TConvertible : IConvertible<TResult>
    {
        ArgumentNullException.ThrowIfNull(convertible);

        return convertible.ToType(provider);
    }
}