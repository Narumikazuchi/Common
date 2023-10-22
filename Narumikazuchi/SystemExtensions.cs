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
    [return: NotNull]
    static public TComparable Clamp<TComparable>(this TComparable value, 
                                                 TComparable lowerBoundary,
                                                 TComparable higherBoundary) 
        where TComparable : IComparable<TComparable>
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value.CompareTo(lowerBoundary) < 0)
        {
            return lowerBoundary;
        }
        else if (value.CompareTo(higherBoundary) > 0)
        {
            return higherBoundary;
        }
        else
        {
            return value;
        }
    }

    /// <summary>
    /// Enumerates the flags, which are set in this value.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all flags that are set in this value.</returns>
    static public FlagEnumerator<TEnum> EnumerateFlags<TEnum>(this TEnum enumValue)
        where TEnum : struct, Enum
    {
        return new(enumValue);
    }

    /// <summary>
    /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
    /// </summary>
    /// <remarks>
    /// Do not confuse a filename for a filepath, since this method will also remove
    /// valid path characters like ':' or '/', which are commonly used in paths.
    /// </remarks>
    /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
    [return: NotNull]
    static public String SanitizeForFilename(this String raw)
    {
        ArgumentNullException.ThrowIfNull(raw);

        ImmutableArray<Char> invalidCharacters = s_InvalidPathCharacters.Value;
        StringBuilder result = new(raw.Length);
        foreach (Char character in raw)
        {
            if (!invalidCharacters.Contains(character))
            {
                result.Append(character);
            }
        }

        return result.ToString();
    }

    static private ImmutableArray<Char> GetInvalidPathCharacters()
    {
        return Path.GetInvalidFileNameChars()
                   .Union(Path.GetInvalidPathChars())
                   .OrderBy(character => character)
                   .ToImmutableArray();
    }

    static private readonly Lazy<ImmutableArray<Char>> s_InvalidPathCharacters = new(valueFactory: GetInvalidPathCharacters,
                                                                                     mode: LazyThreadSafetyMode.ExecutionAndPublication);
}