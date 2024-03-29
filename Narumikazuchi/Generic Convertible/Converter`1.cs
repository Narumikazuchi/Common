﻿
namespace Narumikazuchi;

/// <summary>
/// Converts types that implement the <see cref="IConvertible{TType}"/> interface.
/// </summary>
[Obsolete($"The generic convertible feature is superceded by a source generator in the 'Narumikazuchi.Generators.Convertibles' package.")]
static public class Converter<TResult>
{
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
    [return: NotNull]
    static public TResult ToType<TConvertible>([DisallowNull] TConvertible convertible)
        where TConvertible : IConvertible<TResult>
    {
        return convertible.ToType(default(IFormatProvider));
    }
    /// <summary>
    /// Converts the specified <typeparamref name="TConvertible"/> into the <typeparamref name="TResult"/> type 
    /// using the specified culture-specific formatting. 
    /// </summary>
    /// <param name="convertible">The instance to convert.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> implementation which provides culture-specific formatting.</param>
    /// <returns>A new instance of <typeparamref name="TResult"/> with the 
    /// same value as the specified <typeparamref name="TConvertible"/></returns>
    [return: NotNull]
    static public TResult ToType<TConvertible, TFormat>([DisallowNull] TConvertible convertible,
                                                        [AllowNull] TFormat? provider)
        where TConvertible : IConvertible<TResult>
        where TFormat : IFormatProvider
    {
        return convertible.ToType(provider);
    }
}