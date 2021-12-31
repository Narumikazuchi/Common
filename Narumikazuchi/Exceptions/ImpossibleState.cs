namespace Narumikazuchi;

/// <summary>
/// Exception thrown when an impossible state is reached inside an object.
/// </summary>
public sealed class ImpossibleState : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState() :
        base(message: MESSAGE)
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message))
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [AllowNull] Exception? innerException) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message),
             innerException: innerException)
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([DisallowNull] params (Object key, Object? value)[] stateInformation) :
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
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message))
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data
                .Add(key: kv.key,
                     value: kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [AllowNull] Exception? innerException,
                           [DisallowNull] params (Object key, Object? value)[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message),
             innerException: innerException)
    {
        foreach ((Object key, Object? value) kv in stateInformation)
        {
            this.Data
                .Add(key: kv.key,
                     value: kv.value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
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
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message))
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data
                .Add(key: kv.Key,
                     value: kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [AllowNull] Exception? innerException,
                           [DisallowNull] params KeyValuePair<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message),
             innerException: innerException)
    {
        foreach (KeyValuePair<Object, Object?> kv in stateInformation)
        {
            this.Data
                .Add(key: kv.Key,
                     value: kv.Value);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
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
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message))
    {
        foreach (Tuple<Object, Object?> tuple in stateInformation)
        {
            this.Data
                .Add(key: tuple.Item1,
                     value: tuple.Item2);
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ImpossibleState"/> class.
    /// </summary>
    public ImpossibleState([AllowNull] String? message,
                           [AllowNull] Exception? innerException,
                           [DisallowNull] params Tuple<Object, Object?>[] stateInformation) :
        base(message: String.Format(format: "{0} - {1}",
                                    arg0: MESSAGE,
                                    arg1: message),
             innerException: innerException)
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
    private const String MESSAGE = "The reached an impossible state.";
#pragma warning restore
}
