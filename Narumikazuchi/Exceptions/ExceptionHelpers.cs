namespace Narumikazuchi;

/// <summary>
/// Contains helpers for throwing exceptions.
/// </summary>
public static class ExceptionHelpers
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the source parameter is null.
    /// </summary>
    /// <param name="source">The source to check against <see langword="null"/>.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    public static void ThrowIfNull(Object? source, [CallerArgumentExpression("source")] String? paramName = null)
    {
        if (source == null)
        {
            throw new NullReferenceException(paramName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the source parameter is null.
    /// </summary>
    /// <param name="source">The source to check against <see langword="null"/>.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    public static void ThrowIfArgumentNull(Object? source, [CallerArgumentExpression("source")] String? paramName = null)
    {
        if (source == null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the source parameter is null or completely made of whitespace.
    /// </summary>
    /// <param name="source">The source to check against <see langword="null"/>.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    public static void ThrowIfNullOrEmpty(String? source, [CallerArgumentExpression("source")] String? paramName = null)
    {
        if (String.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Extracts precise Inforamtion from the specified <see cref="Exception"/>.
    /// </summary>
    /// <param name="source">The exception to extract data from.</param>
    /// <returns>An information object, containing detailed data on the exception</returns>
    public static ExceptionInformation ExtractInformation(Exception source)
    {
        ThrowIfArgumentNull(source);
        return new(source);
    }
}