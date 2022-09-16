namespace Narumikazuchi;

/// <summary>
/// Contains helpers for throwing exceptions.
/// </summary>
public static class ExceptionHelpers
{
#if NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// Throws an <see cref="NullReferenceException"/> if the source parameter is null.
    /// </summary>
    /// <param name="source">The source to check against <see langword="null"/>.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="asArgumentException">Whether the method should throw an <see cref="ArgumentNullException"/> or a <see cref="NullReferenceException"/>.</param>
    /// <param name="varName">The name of the parameter.</param>
    /// <exception cref="NullReferenceException" />
    /// <remarks>
    /// The custom message will be formatted using <see cref="String.Format(String, Object?)"/>. If you provide a custom message, the '{0}' placeholder 
    /// will be filled with the name of the variable by this method.
    /// </remarks>
    public static void ThrowIfNullOrEmpty(this String? source,
                                          [AllowNull] String? message = null,
                                          Boolean asArgumentException = default,
                                          [CallerArgumentExpression("source")] String? varName = "")
    {
        if (String.IsNullOrWhiteSpace(source))
        {
            if (message is null)
            {
                if (asArgumentException)
                {
                    throw new ArgumentNullException(paramName: varName,
                                                    message: String.Format(format: NULLREF_MESSAGE,
                                                                            arg0: varName));
                }
                else
                {
                    throw new NullReferenceException(message: String.Format(format: NULLREF_MESSAGE,
                                                                            arg0: varName));
                }
            }
            else
            {
                if (asArgumentException)
                {
                    throw new ArgumentNullException(paramName: varName,
                                                    message: String.Format(format: message,
                                                                            arg0: varName));
                }
                else
                {
                    throw new NullReferenceException(message: String.Format(format: message,
                                                                            arg0: varName));
                }
            }
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the source parameter is less than the specified bounds.
    /// </summary>
    /// <param name="source">The source to check against the <paramref name="bound"/>.</param>
    /// <param name="bound">The low bound to check against.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void ThrowIfLesserThan<T>(this T source,
                                            T bound,
                                            [AllowNull] String? message = null,
                                            [CallerArgumentExpression("source")] String? paramName = "")
        where T : IComparable<T>
    {
        if (source.CompareTo(bound) < 0)
        {
            if (message is null)
            {
                throw new ArgumentOutOfRangeException(paramName: paramName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(message: message,
                                                      paramName: paramName);
            }
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the source parameter is bigger than the specified bounds.
    /// </summary>
    /// <param name="source">The source to check against the <paramref name="bound"/>.</param>
    /// <param name="bound">The high bound to check against.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void ThrowIfBiggerThan<T>(this T source,
                                            T bound,
                                            [AllowNull] String? message = null,
                                            [CallerArgumentExpression("source")] String? paramName = "")
        where T : IComparable<T>
    {
        if (source.CompareTo(bound) > 0)
        {
            if (message is null)
            {
                throw new ArgumentOutOfRangeException(paramName: paramName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(message: message,
                                                      paramName: paramName);
            }
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the source parameter is outside of the specified bounds.
    /// </summary>
    /// <param name="source">The source to check against the <paramref name="lowBound"/> and <paramref name="highBound"/>.</param>
    /// <param name="lowBound">The low bound to check against.</param>
    /// <param name="highBound">The high bound to check against.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void ThrowIfOutOfRange<T>(this T source,
                                            T lowBound,
                                            T highBound,
                                            [AllowNull] String? message = null,
                                            [CallerArgumentExpression("source")] String? paramName = "")
        where T : IComparable<T>
    {
        if (source.CompareTo(lowBound) < 0 ||
            source.CompareTo(highBound) > 0)
        {
            if (message is null)
            {
                throw new ArgumentOutOfRangeException(paramName: paramName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(message: message,
                                                      paramName: paramName);
            }
        }
    }
#endif

#if NET5_0_OR_GREATER
    /// <summary>
    /// Extracts precise Inforamtion from the specified <see cref="Exception"/>.
    /// </summary>
    /// <param name="source">The exception to extract data from.</param>
    /// <returns>An information object, containing detailed data on the exception</returns>
    public static ExceptionInformation ExtractInformation(this Exception source)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
#else
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }
#endif

        return new(source: source);
    }
#endif

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NULLREF_MESSAGE = "The specified variable with the name {0} is null.";
}