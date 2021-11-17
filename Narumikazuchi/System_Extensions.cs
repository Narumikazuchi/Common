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
    [Pure]
    [return: NotNull]
    public static T Clamp<T>(this T value, 
                             [DisallowNull] T lowBound,
                             [DisallowNull] T highBound) 
        where T : IComparable<T>
    {
        ExceptionHelpers.ThrowIfArgumentNull(lowBound);
        ExceptionHelpers.ThrowIfArgumentNull(highBound);

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

    /// <summary>
    /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
    /// </summary>
    /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
    [Pure]
    [return: NotNull]
    public static String SanitizeForFilename(this String raw)
    {
        IEnumerable<Char> invalid = Path.GetInvalidFileNameChars()
                                        .Union(Path.GetInvalidPathChars());
        String result = raw;
        foreach (Char c in invalid)
        {
            result = result.Replace(c.ToString(), 
                                    String.Empty,
                                    StringComparison.InvariantCultureIgnoreCase);
        }
        return result;
    }

    /// <summary>
    /// Determines whether this type is a <see cref="Singleton"/>.
    /// </summary>
    /// <returns><see langword="true"/> if this type is a <see cref="Singleton"/>; else, <see langword="false"/></returns>
    [Pure]
    public static Boolean IsSingleton(this Type type) => 
        type.IsAssignableTo(typeof(Singleton));
}