namespace Narumikazuchi;

/// <summary>
/// Enumerates through enum values.
/// </summary>
public static class EnumEnumerator
{
    /// <summary>
    /// Enumerates through all values defined for the <typeparamref name="TEnum"/>.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all values defined for the <typeparamref name="TEnum"/>.</returns>
    [Pure]
    [return: NotNull]
    public static IEnumerable<TEnum> EnumerateValues<TEnum>() 
        where TEnum : Enum =>
            __EnumChache<TEnum>.DefinedValues.Value;

    /// <summary>
    /// Enumerates the flags, which are set in the input value.
    /// </summary>
    /// <param name="enumValue">The value to enumerate the flags of.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all flags that are set in the input value.</returns>
    [Pure]
    [return: NotNull]
    public static IEnumerable<TEnum> EnumerateFlags<TEnum>([DisallowNull] TEnum enumValue)
        where TEnum : Enum
    {
        ArgumentNullException.ThrowIfNull(enumValue);

        if (!AttributeResolver.HasAttribute<FlagsAttribute>(typeof(TEnum)))
        {
            return Array.Empty<TEnum>();
        }
        else
        {
            return new __EnumEnumerator<TEnum>(enumValue);
        }
    }
}

internal partial struct __EnumEnumerator<TEnum>
    where TEnum : Enum
{
    internal __EnumEnumerator(TEnum value)
    {
        m_Enumerator = __EnumChache<TEnum>.DefinedValues.Value.GetEnumerator();
        m_Value = value;
        m_Current = default;
        m_State = null;
    }

    public __EnumEnumerator<TEnum> GetEnumerator()
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

    private readonly HashSet<TEnum>.Enumerator m_Enumerator;
    private readonly TEnum m_Value;
    private TEnum? m_Current;
    private Boolean? m_State;
}

// IEnumerable
partial struct __EnumEnumerator<TEnum> : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}

// IEnumerable<T>
partial struct __EnumEnumerator<TEnum> : IEnumerable<TEnum>
{
    IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator() =>
        this.GetEnumerator();
}

// IEnumerator
partial struct __EnumEnumerator<TEnum> : IEnumerator
{
    public Boolean MoveNext()
    {
        if (!m_State.HasValue)
        {
            m_State = true;
        }

        while (m_Enumerator.MoveNext())
        {
            TEnum current = m_Enumerator.Current;
            if (m_Value.HasFlag(current))
            {
                m_Current = current;
                return true;
            }
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
partial struct __EnumEnumerator<TEnum> : IEnumerator<TEnum>
{
    public TEnum Current
    {
        get
        {
            if (!m_State.HasValue ||
                !m_State.Value)
            {
                throw new InvalidOperationException();
            }
            return m_Current!;
        }
    }
}