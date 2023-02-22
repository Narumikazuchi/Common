﻿namespace Narumikazuchi;

public partial struct MaybeNull<T> : IEquatable<T>
{
    /// <inheritdoc/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public readonly Boolean Equals(T? other)
    {
        if (m_Value is null &&
            other is null)
        {
            return true;
        }
        if (m_Value is null)
        {
            return false;
        }

        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other);
    }
}

public partial struct MaybeNull<T> : IEquatable<MaybeNull<T>>
{
    /// <inheritdoc/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public readonly Boolean Equals(MaybeNull<T> other)
    {
        if (m_Value is null &&
            other.m_Value is null)
        {
            return true;
        }
        if (m_Value is null)
        {
            return false;
        }

        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other.m_Value);
    }
}