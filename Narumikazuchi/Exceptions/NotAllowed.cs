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
        base(message: MESSAGE)
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage))
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage),
             innerException: inner)
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(message: MESSAGE)
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data
                .Add(key: kv.key,
                     value: kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage))
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data
                .Add(key: kv.key,
                     value: kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner,
                      [DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage),
             innerException: inner)
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data
                .Add(key: kv.key,
                     value: kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(message: MESSAGE)
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data
                .Add(key: kv.Key,
                     value: kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage))
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data
                .Add(key: kv.Key,
                     value: kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner,
                      [DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage),
             innerException: inner)
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data
                .Add(key: kv.Key,
                     value: kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(message: MESSAGE)
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data
                .Add(key: tuple.Item1,
                     value: tuple.Item2);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage))
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data
                .Add(key: tuple.Item1,
                     value: tuple.Item2);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public NotAllowed([AllowNull] String? auxMessage,
                      [AllowNull] Exception? inner,
                      [DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: auxMessage),
             innerException: inner)
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data
                .Add(key: tuple.Item1,
                     value: tuple.Item2);
        }
    }

#pragma warning disable IDE1006
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MESSAGE = "The operation is not allowed.";
#pragma warning restore
}