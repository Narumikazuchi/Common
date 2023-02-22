namespace Narumikazuchi;

public readonly partial struct NotNullOrEmpty<T>
    where T : IEnumerable
{
#pragma warning disable CS1591 // Missing comments
    static public implicit operator NotNullOrEmpty<T>(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [DisallowNull]
# endif
        T? value)
    {
        return new(value);
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    [return: NotNull]
# endif
    static public implicit operator T(NotNullOrEmpty<T> value)
    {
        return value.m_Value;
    }
#pragma warning restore

    public NotNullOrEmpty(T? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException();
        }
        
        IEnumerator enumerator = value.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new ArgumentException();
        }

        m_Value = value;
    }

    /// <inheritdoc/>
    public readonly override Boolean Equals(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        Object? obj)
    {
        if (obj is T value)
        {
            return this.Equals(value);
        }
        if (obj is NotNull<T> other)
        {
            return this.Equals(other);
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public readonly override Int32 GetHashCode()
    {
        if (m_Value is null)
        {
            return Int32.MaxValue;
        }
        else
        {
            return m_Value.GetHashCode();
        }
    }

    /// <inheritdoc/>
    public readonly override String? ToString()
    {
        if (m_Value is String value)
        {
            return value;
        }
        else if (m_Value is null)
        {
            return String.Empty;
        }
        else
        {
            return m_Value.ToString();
        }
    }

    private readonly T m_Value;
}