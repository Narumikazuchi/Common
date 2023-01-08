namespace Narumikazuchi;

/// <summary>
/// Contains extensions for the System namespace.
/// </summary>
public static class System_Extensions
{
    /// <summary>
    /// Returns the comparable clamped between the specified min and max value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lowBound">Low-bound for the clamping</param>
    /// <param name="highBound">High-bound for the clamping</param>
    /// <exception cref="ArgumentNullException"/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static T Clamp<T>(this T value, 
                             T lowBound,
                             T highBound) 
        where T : IComparable<T>
    {
        if (value.CompareTo(lowBound) < 0)
        {
            return lowBound;
        }
        else if (value.CompareTo(highBound) > 0)
        {
            return highBound;
        }
        return value;
    }

#if NET48_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
    /// <summary>
    /// Enumerates the flags, which are set in this value.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all flags that are set in this value.</returns>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static EnumValues<TEnum> EnumerateFlags<TEnum>(this TEnum enumValue)
        where TEnum : struct, Enum =>
            EnumEnumerator.EnumerateFlags(enumValue);
#endif

    /// <summary>
    /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
    /// </summary>
    /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static String SanitizeForFilename(this String raw)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(raw);
#else
        if (raw is null)
        {
            throw new ArgumentNullException(nameof(raw));
        }
#endif

        Char[] invalid = Path.GetInvalidFileNameChars()
                             .Union(second: Path.GetInvalidPathChars())
                             .ToArray();
        String result = raw;
        foreach (Char c in invalid)
        {
            result = result.Replace(oldValue: c.ToString(), 
                                    newValue: String.Empty
#if NETCOREAPP2_0_OR_GREATER || NET5_0_OR_GREATER
                                    ,comparisonType: StringComparison.InvariantCultureIgnoreCase
#endif
                                    );
        }
        return result;
    }
}