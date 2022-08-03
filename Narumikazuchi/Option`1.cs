namespace Narumikazuchi;

/// <summary>
/// represents a monad approach to possibly unset values.
/// </summary>
public readonly partial struct Option<T>
{
    private Option(T value)
    {
        m_HasValue = value is not null;
        m_Value = value;
    }

    /// <inheritdoc/>
    public override Boolean Equals([NotNullWhen(true)] Object? obj)
    {
        if (obj is T value)
        {
            return this.Equals(value);
        }
        if (obj is Option<T> other)
        {
            return this.Equals(other);
        }
        else
        {
            return false;
        }
    }
    
    /// <inheritdoc/>
    public override Int32 GetHashCode()
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
    public override String? ToString() =>
        m_Value?.ToString();

    /// <summary>
    /// Maps the value of the <see cref="Option{T}"/> from it's type <typeparamref name="T"/> to a new <see cref="Option{T}"/>
    /// of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <param name="map">The function used to map from type <typeparamref name="T"/> to type <typeparamref name="TResult"/>.</param>
    /// <returns>A new <see cref="Option{T}"/> of type <typeparamref name="TResult"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    [Pure]
    [return: NotNull]
    public Option<TResult> Map<TResult>([DisallowNull] Func<T, TResult> map)
    {
        ArgumentNullException.ThrowIfNull(map);

        if (!m_HasValue)
        {
            return new();
        }
        else
        {
            return new(map.Invoke(m_Value!));
        }
    }

    /// <summary>
    /// Maps the value of the <see cref="Option{T}"/> from it's type <typeparamref name="T"/> to a new <see cref="Option{T}"/>
    /// of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <param name="map">The function used to map from type <typeparamref name="T"/> to an <see cref="Option{T}"/> of type <typeparamref name="TResult"/>.</param>
    /// <returns>A new <see cref="Option{T}"/> of type <typeparamref name="TResult"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    [Pure]
    [return: NotNull]
    public Option<TResult> MapDirect<TResult>([DisallowNull] Func<T, Option<TResult>> map)
    {
        ArgumentNullException.ThrowIfNull(map);

        if (!m_HasValue)
        {
            return new();
        }
        else
        {
            return map.Invoke(m_Value!);
        }
    }

    /// <summary>
    /// Gets the value wrapped in the <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="value">The value wrapped in the <see cref="Option{T}"/>.</param>
    /// <returns><see langword="true"/> if the <see cref="Option{T}"/> wraps a valid value; otherwise, <see langword="false"/>.</returns>
    [Pure]
    [return: NotNull]
    public Boolean TryGetValue([NotNullWhen(true)] out T? value)
    {
        value = m_Value;
        return m_HasValue;
    }

#pragma warning disable CS1591 // Missing comments
    [return: NotNull]
    public static implicit operator Option<T>(T? value)
    {
        if (value is null)
        {
            return new();
        }
        else
        {
            return new(value);
        }
    }

    public static implicit operator T?(Option<T> value)
    {
        if (value.TryGetValue(out T? result))
        {
            return result;
        }
        else
        {
            return default;
        }
    }

    [Pure]
    [return: NotNull]
    public static Boolean operator ==(Option<T> left,
                                      Option<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    [return: NotNull]
    public static Boolean operator !=(Option<T> left,
                                      Option<T> right)
    {
        return !left.Equals(right);
    }

    [Pure]
    [return: NotNull]
    public static Boolean operator ==(Option<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    [return: NotNull]
    public static Boolean operator !=(Option<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
#pragma warning restore

    /// <summary>
    /// Gets whether the <see cref="Option{T}"/> wraps an actual value.
    /// </summary>
    [Pure]
    [NotNull]
    public Boolean HasValue =>
        m_HasValue;

    private readonly Boolean m_HasValue;
    private readonly T? m_Value;
}

// IEquatable<T>
partial struct Option<T> : IEquatable<T>
{
    /// <inheritdoc/>
    [Pure]
    [return: NotNull]
    public Boolean Equals(T? other)
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

        return EqualityComparer<T>.Default.Equals(x: m_Value,
                                                  y: other);
    }
}

// IEquatable<T>
partial struct Option<T> : IEquatable<Option<T>>
{
    /// <inheritdoc/>
    [Pure]
    [return: NotNull]
    public Boolean Equals(Option<T> other)
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

        return EqualityComparer<T>.Default.Equals(x: m_Value,
                                                  y: other.m_Value);
    }
}