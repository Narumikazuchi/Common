namespace Narumikazuchi.Collections;

public partial struct FlagEnumerator<TEnum> : IStrongEnumerable<TEnum, FlagEnumerator<TEnum>>
{
    /// <summary>
    /// Gets the <see cref="IEnumerator{T}"/> for this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>Itself, if the enumeration has not yet started; else a clone of itself.</returns>
    public readonly FlagEnumerator<TEnum> GetEnumerator()
    {
        if (m_State.HasValue)
        {
            return new(m_Value);
        }
        else
        {
            return this;
        }
    }
}