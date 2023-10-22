namespace Narumikazuchi.Collections;

public partial struct FlagEnumerator<TEnum> : IStrongEnumerator<TEnum>
{
    /// <inheritdoc/>
    public Boolean MoveNext()
    {
        if (m_State.HasValue &&
            !m_State.Value)
        {
            return false;
        }

        if (!m_State.HasValue)
        {
            m_State = true;
        }

        while (m_Index++ < s_Values.Value.Length - 1)
        {
            TEnum current = s_Values.Value[m_Index];
            if (m_Value.HasFlag(current))
            {
                m_Current = current;
                return true;
            }
        }

        m_State = false;
        return false;
    }

    /// <inheritdoc/>
    public readonly TEnum Current
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