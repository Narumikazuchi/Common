namespace Narumikazuchi;

public partial struct NotNull<T> : IEquatable<T>
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

public partial struct NotNull<T> : IEquatable<NotNull<T>>
{
    /// <inheritdoc/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public readonly Boolean Equals(NotNull<T> other)
    {
        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other.m_Value);
    }
}