namespace Narumikazuchi;

/// <summary>
/// Exception thrown by <see cref="Singleton{T}"/> when derived type does not contain a non-public default constructor.
/// </summary>
public sealed class ConstructorNotFound : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public ConstructorNotFound([AllowNull] String? message,
                               [DisallowNull] params (String key, String? value)[] stateInformation!!) : 
        base(message: String.Format(format: "{0} - {1}", 
                                    arg0: MESSAGE, 
                                    arg1: message))
    {
        foreach ((String key, String? value) kv in stateInformation)
        {
            this.Data.Add(key: kv.key,
                          value: kv.value);
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MESSAGE = "Singleton derived types require a non-public parameterless constructor.";
}