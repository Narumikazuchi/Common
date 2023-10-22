namespace Narumikazuchi.Collections;

public partial struct EnumEnumerator<TEnum> : IStrongEnumerable<TEnum, EnumEnumerator<TEnum>>
{
    /// <summary>
    /// Gets the <see cref="IEnumerator{T}"/> for this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>Itself, if the enumeration has not yet started; else a clone of itself.</returns>
    public readonly EnumEnumerator<TEnum> GetEnumerator()
    {
        if (m_State.HasValue)
        {
            if (m_Mode is 0
                       or 1)
            {
                return new(m_Mode);
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