namespace Narumikazuchi;

/// <summary>
/// Exception thrown when an unallowed action is about to be performed.
/// </summary>
public sealed class NotAllowed : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed() :
        base(MESSAGE)
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage))
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage),
             inner)
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(MESSAGE)
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data.Add(kv.key,
                          kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage))
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data.Add(kv.key,
                          kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner,
                      [DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage),
             inner)
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data.Add(kv.key,
                          kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(MESSAGE)
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data.Add(kv.Key,
                          kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage))
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data.Add(kv.Key,
                          kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner,
                      [DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage),
             inner)
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data.Add(kv.Key,
                          kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(MESSAGE)
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data.Add(tuple.Item1,
                          tuple.Item2);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage))
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data.Add(tuple.Item1,
                          tuple.Item2);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner,
                      [DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(String.Format("{0} - {1}",
                           MESSAGE,
                           auxMessage),
             inner)
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data.Add(tuple.Item1,
                          tuple.Item2);
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MESSAGE = "The operation is not allowed.";
}