namespace Narumikazuchi;

/// <summary>
/// Enumerates through a list of prime numbers
/// </summary>
public partial struct PrimeEnumerator
{

    /// <summary>
    /// Gets the <see cref="IEnumerator{T}"/> for this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>Itself, if the enumeration has not yet started; else a clone of itself.</returns>
    public PrimeEnumerator GetEnumerator()
    {
        if (m_State.HasValue)
        {
            return new(startPoint: m_StartPoint,
                       endPoint: m_EndPoint);
        }
        else
        {
            return this;
        }
    }
}

// Non-Public
partial struct PrimeEnumerator
{
    internal PrimeEnumerator(Int64 startPoint,
                             Int64 endPoint)
    {
        m_StartPoint = startPoint;
        m_EndPoint = endPoint;
        m_Current = default;
        m_Index = -1;
        m_State = default;

        for (Int32 i = 0;
             i < Primes.Known.Count;
             i++)
        {
            if (Primes.Known[i] >= startPoint)
            {
                m_Index = i - 1;
                return;
            }
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        while (Primes.Known[^1] < startPoint)
#else
        while (Primes.Known.Last() < startPoint)
#endif
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

// IEnumerable
partial struct PrimeEnumerator : IEnumerable
{
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}

// IEnumerable<T>
partial struct PrimeEnumerator : IEnumerable<Int64>
{
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    IEnumerator<Int64> IEnumerable<Int64>.GetEnumerator() =>
        this.GetEnumerator();
}

// IEnumerator
partial struct PrimeEnumerator : IEnumerator
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

    void IDisposable.Dispose()
    { }

    void IEnumerator.Reset()
    { }

    Object? IEnumerator.Current =>
        this.Current;
}

// IEnumerator<T>
partial struct PrimeEnumerator : IEnumerator<Int64>
{
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
}