using Narumikazuchi.Collections;
using System.Diagnostics.CodeAnalysis;

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
    [return: NotNull]
    static public TComparable Clamp<TComparable>(this TComparable value, 
                                                 TComparable lowerBoundary,
                                                 TComparable higherBoundary) 
        where TComparable : IComparable<TComparable>
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(value);
#else
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
#endif

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

    /// <summary>
    /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
    /// </summary>
    /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
    [return: NotNull]
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
        foreach (Char character in invalid)
        {
            result = result.Replace(oldValue: character.ToString(), 
                                    newValue: String.Empty
#if NETCOREAPP2_0_OR_GREATER || NET5_0_OR_GREATER
                                    ,comparisonType: StringComparison.InvariantCultureIgnoreCase
#endif
                                    );
        }

        return result;
    }
}