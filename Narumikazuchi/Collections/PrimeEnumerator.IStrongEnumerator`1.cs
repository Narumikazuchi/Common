namespace Narumikazuchi.Collections;

public partial struct PrimeEnumerator : IStrongEnumerator<Int64>
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
            if (m_EndPoint <= 2)
            {
                m_State = false;
                return false;
            }
            else
            {
                m_State = true;
            }
        }

        m_Index++;
        if (m_Index >= Primes.Known.Count)
        {
            Int64 next = Primes.FindNextPrime();
            if (next < m_EndPoint)
            {
                m_Current = next;
                return true;
            }
            else
            {
                m_State = false;
                return false;
            }
        }

        if (Primes.Known[m_Index] < m_EndPoint)
        {
            m_Current = Primes.Known[m_Index];
            return true;
        }

        m_State = false;
        return false;
    }

    /// <inheritdoc/>
    public Int64 Current
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