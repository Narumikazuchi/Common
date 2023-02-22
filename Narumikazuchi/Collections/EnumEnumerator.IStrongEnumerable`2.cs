namespace Narumikazuchi.Collections;

public partial struct EnumEnumerator<TEnum> : IStrongEnumerable<TEnum, EnumEnumerator<TEnum>>
{
    /// <summary>
    /// Gets the <see cref="IEnumerator{T}"/> for this <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>Itself, if the enumeration has not yet started; else a clone of itself.</returns>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public EnumEnumerator<TEnum> GetEnumerator()
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

#if !NETCOREAPP3_1_OR_GREATER
    IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
#endif
}