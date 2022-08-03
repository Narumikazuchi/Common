namespace Narumikazuchi;

/// <summary>
/// Represents an <see cref="IEnumerable{T}"/> and <see cref="IEnumerator{T}"/> of type
/// <typeparamref name="TEnum"/>.
/// </summary>
[DebuggerDisplay("{Typename}[]")]
public partial struct EnumValues<TEnum>
    where TEnum : struct, Enum
{
    /// <inheritdoc/>
    [return: NotNull]
    public override String ToString()
    {
        if (m_Mode is 0)
        {
            return "- empty -";
        }
        else if (m_Mode is 1)
        {
            return String.Join(" | ", s_Values.Value);
        }
        else if (m_Mode is 2)
        {
            return m_Value!.ToString()!;
        }
        else
        {
            throw new ImpossibleState();
        }
    }

    /// <summary>
    /// Gets the <see cref="IEnumerator{T}"/> for this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>Itself, if the enumeration has not yet started; else a clone of itself.</returns>
    [return: NotNull]
    public EnumValues<TEnum> GetEnumerator()
    {
        if (m_State.HasValue)
        {
            if (m_Mode is 0
                       or 1)
            {
                return new(mode: m_Mode);
            }
            else if (m_Mode is 2)
            {
                return new(value: m_Value!.Value,
                           mode: 2);
            }
            else
            {
                throw new ImpossibleState();
            }
        }
        else
        {
            return this;
        }
    }
}

// Non-Public
partial struct EnumValues<TEnum>
{
    internal EnumValues(Int32 mode)
    {
        m_Mode = mode;
        m_Index = -1;
        m_Value = default;
        m_Current = default;
        m_State = null;
    }
    internal EnumValues(TEnum value,
                        Int32 mode)
    {
        m_Mode = mode;
        m_Index = -1;
        m_Value = value;
        m_Current = default;
        m_State = null;
    }

    private Boolean EvaluateItem(TEnum item)
    {
        if (m_Mode is 1)
        {
            m_Current = item;
            return true;
        }
        else if (m_Mode is 2 &&
                 m_Value!.Value.HasFlag(item))
        {
            m_Current = item;
            return true;
        }
        else
        {
            return false;
        }
    }

    private static Option<String> Typename =>
        typeof(TEnum).FullName;

    private static readonly Lazy<TEnum[]> s_Values = new(valueFactory: Enum.GetValues<TEnum>,
                                                         mode: LazyThreadSafetyMode.ExecutionAndPublication);

    // Modes: 0 = nothing, 1 = Enum Values, 2 = Enum Set Bits in Flag
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int32 m_Mode;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TEnum? m_Value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Int32 m_Index;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TEnum? m_Current;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Boolean? m_State;
}

// IEnumerable
partial struct EnumValues<TEnum> : IEnumerable
{
    [return: NotNull]
    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}

// IEnumerable<T>
partial struct EnumValues<TEnum> : IEnumerable<TEnum>
{
    [return: NotNull]
    IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator() =>
        this.GetEnumerator();
}

// IEnumerator
partial struct EnumValues<TEnum> : IEnumerator
{
    /// <inheritdoc/>
    [return: NotNull]
    public Boolean MoveNext()
    {
        if (m_State.HasValue &&
            !m_State.Value)
        {
            return false;
        }

        if (m_Mode is not 1
                   and not 2)
        {
            m_State = false;
            return false;
        }

        if (!m_State.HasValue)
        {
            m_State = true;
        }

        while (m_Index++ < s_Values.Value.Length - 1)
        {
            TEnum current = s_Values.Value[m_Index];
            if (this.EvaluateItem(current))
            {
                return true;
            }
        }

        m_State = false;
        return false;
    }

    void IDisposable.Dispose()
    { }

    void IEnumerator.Reset()
    { }

    Object? IEnumerator.Current =>
        this.Current;
}

// IEnumerator<T>
partial struct EnumValues<TEnum> : IEnumerator<TEnum>
{
    /// <inheritdoc/>
    public TEnum Current
    {
        get
        {
            if (!m_State.HasValue ||
                !m_State.Value)
            {
                throw new InvalidOperationException();
            }
            return m_Current!.Value;
        }
    }
}