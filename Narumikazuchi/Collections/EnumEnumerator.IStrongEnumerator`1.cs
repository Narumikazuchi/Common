namespace Narumikazuchi.Collections;

public partial struct EnumEnumerator<TEnum> : IStrongEnumerator<TEnum>
{
    /// <inheritdoc/>
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

#if !NETCOREAPP3_1_OR_GREATER
    void IDisposable.Dispose()
    { }

    void IEnumerator.Reset()
    { }

    Object? IEnumerator.Current
    {
        get
        {
            return this.Current;
        }
    }
#endif
}