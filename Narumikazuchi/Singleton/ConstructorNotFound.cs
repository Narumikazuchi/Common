namespace Narumikazuchi;

/// <summary>
/// Exception thrown by <see cref="Singleton{T}"/> when derived type does not contain a non-public default constructor.
/// </summary>
public sealed class ConstructorNotFound : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorNotFound"/> class.
    /// </summary>
    public ConstructorNotFound([AllowNull] String? auxMessage,
                               [DisallowNull] params (String key, String? value)[] stateInformation) : 
        base(String.Format("{0} - {1}", 
                           MESSAGE, 
                           auxMessage))
    {
        foreach ((String key, String? value) kv in stateInformation)
        {
            this.Data.Add(kv.key,
                          kv.value);
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MESSAGE = "Singleton derived types require a non-public parameterless constructor.";
}