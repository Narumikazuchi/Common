namespace Narumikazuchi;

/// <summary>
/// Contains helpers for throwing exceptions.
/// </summary>
public static class ExceptionHelpers
{
    /// <summary>
    /// Throws an <see cref="NullReferenceException"/> if the source parameter is null.
    /// </summary>
    /// <param name="source">The source to check against <see langword="null"/>.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="varName">The name of the parameter.</param>
    /// <exception cref="NullReferenceException" />
    /// <remarks>
    /// The custom message will be formatted using <see cref="String.Format(String, Object?)"/>. If you provide a custom message, the '{0}' placeholder 
    /// will be filled with the name of the variable by this method.
    /// </remarks>
    public static void ThrowIfNullOrEmpty(this String? source,
                                          [AllowNull] String? message = null,
                                          [CallerArgumentExpression("source")] String? varName = "")
    {
        if (String.IsNullOrWhiteSpace(source))
        {
            if (message is null)
            {
                throw new NullReferenceException(message: String.Format(format: NULLREF_MESSAGE,
                                                                        arg0: varName));
            }
            throw new NullReferenceException(message: String.Format(format: message,
                                                                    arg0: varName));
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
        where T : notnull, IComparable<T>
    {
        if (source.CompareTo(lowBound) < 0 ||
            source.CompareTo(highBound) > 0)
        {
            if (message is null)
            {
                throw new ArgumentOutOfRangeException(paramName: paramName);
            }
            throw new ArgumentOutOfRangeException(message: message,
                                                  paramName: paramName);
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidCastException"/> if the source parameter can't be cast to the generic type parameter <typeparamref name="TResult"/>.
    /// </summary>
    /// <param name="source">The source to cast to type <typeparamref name="TResult"/>.</param>
    /// <param name="message">An optional message for the <see cref="Exception"/> that will be thrown.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="InvalidCastException" />
    /// <remarks>
    /// The custom message will be formatted using <see cref="String.Format(String, Object?, Object?)"/>. If you provide a custom message, the '{0}' placeholder 
    /// will be filled with the name of the parameter and the '{1}' placeholder will be filled with the name of the type the method failed to cast the object to.
    /// </remarks>
    public static void ThrowIfNotCastable<TAny, TResult>(this TAny source,
                                                         [AllowNull] String? message = null,
                                                         [CallerArgumentExpression("source")] String? paramName = "")
        where TAny : notnull
    {
        if (source is not TResult &&
            !source.GetType()
                   .IsAssignableTo(typeof(TResult)))
        {
            if (message is null)
            {
                throw new InvalidCastException(String.Format(format: CAST_MESSAGE,
                                                             arg0: paramName,
                                                             arg1: typeof(TResult).Name));
            }
            throw new InvalidCastException(String.Format(format: message,
                                                         arg0: paramName,
                                                         arg1: typeof(TResult).Name));
        }
    }

    /// <summary>
    /// Extracts precise Inforamtion from the specified <see cref="Exception"/>.
    /// </summary>
    /// <param name="source">The exception to extract data from.</param>
    /// <returns>An information object, containing detailed data on the exception</returns>
    public static ExceptionInformation ExtractInformation(this Exception source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new(source: source);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NULLREF_MESSAGE = "The specified variable with the name {0} is null.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String CAST_MESSAGE = "The specified variable with the name {0} is can't be cast to the type {1}.";
}