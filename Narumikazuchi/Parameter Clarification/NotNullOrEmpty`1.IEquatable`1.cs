namespace Narumikazuchi;

public partial struct NotNullOrEmpty<T> : IEquatable<T>
{
    /// <inheritdoc/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public readonly Boolean Equals(T? other)
    {
        if (other is null)
        {
            return false;
        }

        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other);
    }
}

public partial struct NotNullOrEmpty<T> : IEquatable<NotNullOrEmpty<T>>
{
    /// <inheritdoc/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public readonly Boolean Equals(NotNullOrEmpty<T> other)
    {
        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other.m_Value);
    }
}