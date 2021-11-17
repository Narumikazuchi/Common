namespace Narumikazuchi;

/// <summary>
/// Exception thrown by <see cref="Singleton{T}"/> when derived type does contain one or more public constructors.
/// </summary>
public sealed class PublicConstructorFound : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublicConstructorFound"/> class.
    /// </summary>
    public PublicConstructorFound([AllowNull] String? auxMessage,
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
    private const String MESSAGE = "Singleton derived types do not allow public constructors.";
}