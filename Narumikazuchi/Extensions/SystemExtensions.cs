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

        Span<Char> result = stackalloc Char[raw.Length];
        Int32 position = 0;
        foreach (Char character in raw)
        {
            if (character is <= (Char)31
                          or (Char)34
                          or (Char)42
                          or (Char)47
                          or (Char)58
                          or (Char)60
                          or (Char)62
                          or (Char)63
                          or (Char)92
                          or (Char)124)
            {
                continue;
            }

            result[position] = character;
            position++;
        }

        return result[..position].ToString();
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
    static public String SanitizeForFilename(this String raw,
                                             Char replaceWith)
    {
        ArgumentNullException.ThrowIfNull(raw);
        if (replaceWith is <= (Char)31
                        or (Char)34
                        or (Char)42
                        or (Char)47
                        or (Char)58
                        or (Char)60
                        or (Char)62
                        or (Char)63
                        or (Char)92
                        or (Char)124)
        {
            throw new ArgumentException(paramName: nameof(replaceWith),
                                        message: CANNOT_REPLACE_WITH_INVALID_CHARACTER);
        }

        Span<Char> result = stackalloc Char[raw.Length];
        Int32 position = 0;
        foreach (Char character in raw)
        {
            if (character is <= (Char)31
                          or (Char)34
                          or (Char)42
                          or (Char)47
                          or (Char)58
                          or (Char)60
                          or (Char)62
                          or (Char)63
                          or (Char)92
                          or (Char)124)
            {
                result[position] = replaceWith;
                continue;
            }
            else
            {
                result[position] = character;
            }

            position++;
        }

        return result.ToString();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String CANNOT_REPLACE_WITH_INVALID_CHARACTER = "The character can not be used to replace invalid path characters since itself is an invalid character.";
}