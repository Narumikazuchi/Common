using Narumikazuchi.Collections;

namespace Narumikazuchi;

/// <summary>
/// Contains extensions for the System namespace.
/// </summary>
static public class SystemExtensions
{
    /// <summary>
    /// Returns the comparable clamped between the specified min and max value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lowerBoundary">Low-bound for the clamping</param>
    /// <param name="higherBoundary">High-bound for the clamping</param>
    /// <exception cref="ArgumentNullException"/>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public T Clamp<T>(this T value, 
                             NotNull<T> lowerBoundary,
                             NotNull<T> higherBoundary) 
        where T : IComparable<T>
    {
        if (value.CompareTo(lowerBoundary) < 0)
        {
            return lowerBoundary;
        }
        else if (value.CompareTo(higherBoundary) > 0)
        {
            return higherBoundary;
        }
        return value;
    }

#if NET48_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
    /// <summary>
    /// Enumerates the flags, which are set in this value.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all flags that are set in this value.</returns>
    static public EnumEnumerator<TEnum> EnumerateFlags<TEnum>(this TEnum enumValue)
        where TEnum : struct, Enum
    {
        if (!AttributeResolver.HasAttribute<FlagsAttribute>(typeof(TEnum)))
        {
            return new(0);
        }
        else
        {
            return new(value: enumValue,
                       mode: 2);
        }
    }
#endif

    /// <summary>
    /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
    /// </summary>
    /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public String SanitizeForFilename(this String raw)
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
                             .Union(Path.GetInvalidPathChars())
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