namespace Narumikazuchi;

/// <summary>
/// Exception thrown when an unallowed action is about to be performed.
/// </summary>
public sealed class NotAllowed : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public NotAllowed(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<String> message = default,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<Exception> innerException = default)
            : base(message: $"{MESSAGE} - {message}",
                   innerException: innerException)
    { }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// Initializes a new instance of the <see cref="NotAllowed"/> class.
    /// </summary>
    public NotAllowed([AllowNull] MaybeNull<String> message = default,
                      [AllowNull] MaybeNull<Exception> innerException = default,
                      [DisallowNull] params (Object key, MaybeNull<Object> value)[] stateInformation)
        : base(message: $"{MESSAGE} - {message}",
               innerException: innerException)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(stateInformation);
#else
        if (stateInformation is null)
        {
            throw new ArgumentNullException(nameof(stateInformation));
        }
#endif

        foreach ((Object key, MaybeNull<Object> value) kv in stateInformation)
        {
            if (kv.value.TryGetValue(out Object? embedded))
            {
                this.Data.Add(key: kv.key,
                              value: embedded);
            }
            else
            {
                this.Data.Add(key: kv.key,
                              value: null);
            }
        }
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="NotAllowed"/> class.
    /// </summary>
    public NotAllowed(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<String> message = default,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<Exception> innerException = default,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        params KeyValuePair<Object, MaybeNull<Object>>[] stateInformation)
            : base(message: $"{MESSAGE} - {message}",
                   innerException: innerException)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(stateInformation);
#else
        if (stateInformation is null)
        {
            throw new ArgumentNullException(nameof(stateInformation));
        }
#endif

        foreach (KeyValuePair<Object, MaybeNull<Object>> kv in stateInformation)
        {
            if (kv.Value.TryGetValue(out Object? embedded))
            {
                this.Data.Add(key: kv.Key,
                              value: embedded);
            }
            else
            {
                this.Data.Add(key: kv.Key,
                              value: null);
            }
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="NotAllowed"/> class.
    /// </summary>
    public NotAllowed(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<String> message = default,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        MaybeNull<Exception> innerException = default,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        params Tuple<Object, MaybeNull<Object>>[] stateInformation)
            : base(message: $"{MESSAGE} - {message}",
                   innerException: innerException)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(stateInformation);
#else
        if (stateInformation is null)
        {
            throw new ArgumentNullException(nameof(stateInformation));
        }
#endif

        foreach (Tuple<Object, MaybeNull<Object>> tuple in stateInformation)
        {
            if (tuple.Item2.TryGetValue(out Object? embedded))
            {
                this.Data.Add(key: tuple.Item1,
                              value: embedded);
            }
            else
            {
                this.Data.Add(key: tuple.Item1,
                              value: null);
            }
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MESSAGE = "The operation is not allowed.";
}