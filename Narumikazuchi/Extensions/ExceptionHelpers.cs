namespace Narumikazuchi;

/// <summary>
/// Contains helpers for throwing exceptions.
/// </summary>
static public class ExceptionHelpers
{
    /// <summary>
    /// Throws an <see cref="NullReferenceException"/> if the source parameter is null.
    /// </summary>
    /// <param name="source">The source to check against <see langword="null"/>.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="asArgumentException">Whether the method should throw an <see cref="ArgumentNullException"/> or a <see cref="NullReferenceException"/>.</param>
    /// <param name="varName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="NullReferenceException" />
    /// <remarks>
    /// The custom message will be formatted using <see cref="String.Format(String, Object?)"/>. If you provide a custom message, the '{0}' placeholder 
    /// will be filled with the name of the variable by this method.
    /// </remarks>
    static public void ThrowIfNullOrEmpty(this String? source,
                                          [AllowNull] String message = default,
                                          Boolean asArgumentException = default,
                                          [CallerArgumentExpression(nameof(source))] String? varName = "")
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
                                                    message: String.Format(format: message!,
                                                                           arg0: varName));
                }
                else
                {
                    throw new NullReferenceException(message: String.Format(format: message!,
                                                                            arg0: varName));
                }
            }
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the source parameter is less than the specified bounds.
    /// </summary>
    /// <param name="source">The source to check against the <paramref name="boundary"/>.</param>
    /// <param name="boundary">The low bound to check against.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    static public void ThrowIfLesserThan<TComparable>(this TComparable source,
                                                      [DisallowNull] TComparable boundary,
                                                      [AllowNull] String message = default,
                                                      [CallerArgumentExpression(nameof(source))] String? paramName = "")
        where TComparable : IComparable<TComparable>
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(boundary);

        if (source.CompareTo(boundary) < 0)
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
    /// <param name="source">The source to check against the <paramref name="boundary"/>.</param>
    /// <param name="boundary">The high bound to check against.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    static public void ThrowIfBiggerThan<TComparable>(this TComparable source,
                                                      [DisallowNull] TComparable boundary,
                                                      [AllowNull] String message = default,
                                                      [CallerArgumentExpression(nameof(source))] String? paramName = "")
        where TComparable : IComparable<TComparable>
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(boundary);

        if (source.CompareTo(boundary) > 0)
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
    /// <param name="source">The source to check against the <paramref name="lowerBoundary"/> and <paramref name="higherBoundary"/>.</param>
    /// <param name="lowerBoundary">The low bound to check against.</param>
    /// <param name="higherBoundary">The high bound to check against.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentOutOfRangeException" />
    static public void ThrowIfOutOfRange<TComparable>(this TComparable source,
                                                      [DisallowNull] TComparable lowerBoundary,
                                                      [DisallowNull] TComparable higherBoundary,
                                                      [AllowNull] String message = default,
                                                      [CallerArgumentExpression(nameof(source))] String? paramName = "")
        where TComparable : IComparable<TComparable>
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(lowerBoundary);
        ArgumentNullException.ThrowIfNull(higherBoundary);

        if (source.CompareTo(lowerBoundary) < 0 ||
            source.CompareTo(higherBoundary) > 0)
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
    /// Extracts precise Inforamtion from the specified <see cref="Exception"/>.
    /// </summary>
    /// <param name="source">The exception to extract data from.</param>
    /// <returns>An information object, containing detailed data on the exception</returns>
    /// <exception cref="ArgumentNullException" />
#pragma warning disable
    [Obsolete($"The feature of '{nameof(ExceptionInformation)}' will be removed in future releases.")]
#pragma warning restore
    public static ExceptionInformation ExtractInformation(this Exception source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new(source: source);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NULLREF_MESSAGE = "The specified variable with the name {0} is null.";
}