namespace Narumikazuchi.Collections;

/// <summary>
/// Represents an <see cref="IStrongEnumerable{TElement, TEnumerator}"/> and <see cref="IStrongEnumerator{TElement}"/> of type
/// <typeparamref name="TEnum"/>.
/// </summary>
[DebuggerDisplay("{Typename}[]")]
public partial struct FlagEnumerator<TEnum>
    where TEnum : struct, Enum
{
    /// <summary>
    /// Creates a new instance of the <see cref="FlagEnumerator{TEnum}"/> struct.
    /// </summary>
    /// <param name="value">The value to enumerate the flags of.</param>
    public FlagEnumerator(TEnum value)
    {
        if (!AttributeResolver.HasAttribute<FlagsAttribute>(typeof(TEnum)))
        {
            throw new NotAllowed(message: "The enum you are trying to enumerate the flags of has no 'FlagsAttribute' set.",
                                 innerException: null,
                                 ("Typename", typeof(TEnum).FullName));
        }

        m_Index = -1;
        m_Value = value;
        m_Current = default;
        m_State = null;
    }

    /// <inheritdoc/>
    [return: NotNull]
    public override readonly String ToString()
    {
        return m_Value.ToString()!;
    }

    static private readonly Lazy<TEnum[]> s_Values = new(valueFactory: Enum.GetValues<TEnum>,
                                                         mode: LazyThreadSafetyMode.ExecutionAndPublication);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TEnum m_Value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Int32 m_Index;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TEnum? m_Current;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Boolean? m_State;
}