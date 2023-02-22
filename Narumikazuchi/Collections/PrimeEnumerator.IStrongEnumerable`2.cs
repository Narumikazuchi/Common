namespace Narumikazuchi.Collections;

public partial struct PrimeEnumerator : IStrongEnumerable<Int64, PrimeEnumerator>
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

#if !NETCOREAPP3_1_OR_GREATER
    IEnumerator<Int64> IEnumerable<Int64>.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
#endif
}