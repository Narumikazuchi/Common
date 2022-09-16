namespace Narumikazuchi;

/// <summary>
/// Enumerates through enum values.
/// </summary>
public static class EnumEnumerator
{
    /// <summary>
    /// Enumerates through all values defined for the <typeparamref name="TEnum"/>.
    /// </summary>
    /// <returns>An <see cref="EnumValues{T}"/> containing all values defined for the <typeparamref name="TEnum"/>.</returns>
#if NET5_0_OR_GREATER
    [Obsolete("Core library now has Enum.GetValues<TEnum> meaning this function will not be maintained further.", false)]
    [Pure]
#endif
    public static EnumValues<TEnum> EnumerateValues<TEnum>() 
        where TEnum : struct, Enum =>
            new(mode: 1);

#if NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
    /// <summary>
    /// Enumerates the flags, which are set in the input value.
    /// </summary>
    /// <param name="enumValue">The value to enumerate the flags of.</param>
    /// <returns>An <see cref="EnumValues{T}"/> containing all flags that are set in the input value.</returns>
    [Pure]
    public static EnumValues<TEnum> EnumerateFlags<TEnum>(TEnum enumValue)
        where TEnum : struct, Enum
    {
        if (!AttributeResolver.HasAttribute<FlagsAttribute>(typeof(TEnum)))
        {
            return new(mode: 0);
        }
        else
        {
            return new(value: enumValue,
                       mode: 2);
        }
    }
#endif
}