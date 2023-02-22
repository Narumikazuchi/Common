namespace Narumikazuchi.Collections;

public partial struct EnumEnumerator<TEnum>
{
    internal EnumEnumerator(Int32 mode)
    {
        m_Mode = mode;
        m_Index = -1;
        m_Value = default;
        m_Current = default;
        m_State = null;
    }
    internal EnumEnumerator(TEnum value,
                            Int32 mode)
    {
        m_Mode = mode;
        m_Index = -1;
        m_Value = value;
        m_Current = default;
        m_State = null;
    }

#if !NET5_0_OR_GREATER
    static private TEnum[] GetValues()
    {
        return Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .ToArray();
    }
#endif

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

    static private String? Typename
    {
        get
        {
            return typeof(TEnum).FullName;
        }
    }

#if NET5_0_OR_GREATER
    static private readonly Lazy<TEnum[]> s_Values = new(valueFactory: Enum.GetValues<TEnum>,
                                                         mode: LazyThreadSafetyMode.ExecutionAndPublication);
#else
    static private readonly Lazy<TEnum[]> s_Values = new(valueFactory: GetValues,
                                                         mode: LazyThreadSafetyMode.ExecutionAndPublication);
#endif

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