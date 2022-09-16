namespace Narumikazuchi;

/// <summary>
/// Enumerates through enum values.
/// </summary>
public static class EnumEnumerator
{
    /// <summary>
    /// Enumerates through all values defined for the <typeparamref name="TEnum"/>.
    /// </summary>
    /// <remarks>
    /// Technically already included in the core libary as <see cref="Enum.GetValues{TEnum}"/>.
    /// Because of that, this is now deprecated.
    /// </remarks>
    /// <returns>An <see cref="EnumValues{T}"/> containing all values defined for the <typeparamref name="TEnum"/>.</returns>
    [Obsolete("Core library now has Enum.GetValues<TEnum> meaning this function will not be developed further.", false)]
    [Pure]
    [return: NotNull]
    public static EnumValues<TEnum> EnumerateValues<TEnum>() 
        where TEnum : struct, Enum =>
            new(mode: 1);

    /// <summary>
    /// Enumerates the flags, which are set in the input value.
    /// </summary>
    /// <param name="enumValue">The value to enumerate the flags of.</param>
    /// <returns>An <see cref="EnumValues{T}"/> containing all flags that are set in the input value.</returns>
    [Pure]
    [return: NotNull]
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
}