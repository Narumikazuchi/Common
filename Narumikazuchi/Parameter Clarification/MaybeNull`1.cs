namespace Narumikazuchi;

public readonly partial struct MaybeNull<T>
{
#pragma warning disable CS1591 // Missing comments
    static public implicit operator MaybeNull<T>(T? value)
    {
        return new(value);
    }

    static public implicit operator T?(MaybeNull<T> value)
    {
        return value.m_Value;
    }
#pragma warning restore

    public MaybeNull(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [AllowNull]
# endif
        T? value)
    {
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
        if (obj is MaybeNull<T> other)
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

    public readonly MaybeNull<T> WhenNull(NotNull<Action> action)
    {
        if (m_Value is null)
        {
            Action value = action;
            value.Invoke();
        }

        return this;
    }

    public readonly MaybeNull<T> WhenNull<TResult>(
        NotNull<Func<TResult>> transform,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out TResult? result)
    {
        if (m_Value is null)
        {
            Func<TResult> value = transform;
            result = value.Invoke();
        }
        else
        {
            result = default;
        }

        return this;
    }

    public readonly MaybeNull<T> WhenNotNull(NotNull<Action<T>> action)
    {
        if (m_Value is not null)
        {
            Action<T> value = action;
            value.Invoke(m_Value);
        }

        return this;
    }

    public readonly MaybeNull<T> WhenNotNull<TResult>(
        NotNull<Func<T, TResult>> transform,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out TResult? result)
    {
        if (m_Value is not null)
        {
            Func<T, TResult> value = transform;
            result = value.Invoke(m_Value);
        }
        else
        {
            result = default;
        }

        return this;
    }

    /// <summary>
    /// Gets the value wrapped in the <see cref="MaybeNull{T}"/>.
    /// </summary>
    /// <param name="value">The value wrapped in the <see cref="MaybeNull{T}"/>.</param>
    /// <returns><see langword="true"/> if the <see cref="MaybeNull{T}"/> wraps a valid value; otherwise, <see langword="false"/>.</returns>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public readonly Boolean TryGetValue(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out T? value)
    {
        value = m_Value;
        return m_Value is not null;
    }

    public Boolean IsNull
    {
        get
        {
            return m_Value is null;
        }
    }

    private readonly T? m_Value;
}