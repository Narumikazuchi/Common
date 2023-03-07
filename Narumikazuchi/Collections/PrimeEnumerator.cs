namespace Narumikazuchi.Collections;

/// <summary>
/// Enumerates through a list of prime numbers
/// </summary>
public partial struct PrimeEnumerator
{
    internal PrimeEnumerator(Int64 startPoint,
                             Int64 endPoint)
    {
        m_StartPoint = startPoint;
        m_EndPoint = endPoint;
        m_Current = default;
        m_Index = -1;
        m_State = default;

        for (Int32 index = 0;
             index < Primes.Known.Count;
             index++)
        {
            if (Primes.Known[index] >= startPoint)
            {
                m_Index = index - 1;
                return;
            }
        }

        while (Primes.Known[^1] < startPoint)
        {
            Int64 next = Primes.FindNextPrime();
            if (next >= startPoint)
            {
                m_Index = Primes.Known.Count - 2;
                return;
            }
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int64 m_StartPoint;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int64 m_EndPoint;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Int64? m_Current;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Int32 m_Index;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Boolean? m_State;
}