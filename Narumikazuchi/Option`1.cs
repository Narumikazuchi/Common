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
    public override Boolean Equals(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        Object? obj)
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
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public Option<TResult> Map<TResult>(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [DisallowNull]
#endif
        Func<T, TResult> map)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(map);
#else
        if (map is null)
        {
            throw new ArgumentNullException(nameof(map));
        }
#endif

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
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public Option<TResult> MapDirect<TResult>(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [DisallowNull]
#endif
        Func<T, Option<TResult>> map)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(map);
#else
        if (map is null)
        {
            throw new ArgumentNullException(nameof(map));
        }
#endif

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
    /// Performs the specified <paramref name="interaction"/> on the optional value, if <see cref="HasValue"/> is <see langword="true"/>.
    /// Otherwise, nothing will happen.
    /// </summary>
    /// <param name="interaction">The interaction to execute.</param>
    /// <exception cref="ArgumentNullException"/>
    public void Interact(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [DisallowNull]
#endif
        Action<T> interaction)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(interaction);
#else
        if (interaction is null)
        {
            throw new ArgumentNullException(nameof(interaction));
        }
#endif

        if (m_HasValue)
        {
            interaction.Invoke(m_Value!);
        }
    }

    /// <summary>
    /// Gets the value wrapped in the <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="value">The value wrapped in the <see cref="Option{T}"/>.</param>
    /// <returns><see langword="true"/> if the <see cref="Option{T}"/> wraps a valid value; otherwise, <see langword="false"/>.</returns>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public Boolean TryGetValue(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out T? value)
    {
        value = m_Value;
        return m_HasValue;
    }

#pragma warning disable CS1591 // Missing comments
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

#if !NET7_0_OR_GREATER
    [Pure]
    public static Boolean operator ==(Option<T> left,
                                      Option<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    public static Boolean operator !=(Option<T> left,
                                      Option<T> right)
    {
        return !left.Equals(right);
    }

    [Pure]
    public static Boolean operator ==(Option<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    public static Boolean operator !=(Option<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
#endif

#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public static Boolean operator ==(T? left,
                                      Option<T> right)
    {
        return right.Equals(left);
    }

#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public static Boolean operator !=(T? left,
                                      Option<T> right)
    {
        return !right.Equals(left);
    }
#pragma warning restore

    /// <summary>
    /// Gets whether the <see cref="Option{T}"/> wraps an actual value.
    /// </summary>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public Boolean HasValue =>
        m_HasValue;

    private readonly Boolean m_HasValue;
    private readonly T? m_Value;
}

#if NET7_0_OR_GREATER
// IEqualityOperators<Option<T>, T>
partial struct Option<T> : IEqualityOperators<Option<T>, T, Boolean>
{
#pragma warning disable CS1591 // Missing comments
    [Pure]
    public static Boolean operator ==(Option<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    public static Boolean operator !=(Option<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
#pragma warning restore
}

// IEqualityOperators<Option<T>, Option<T>>
partial struct Option<T> : IEqualityOperators<Option<T>, Option<T>, Boolean>
{
#pragma warning disable CS1591 // Missing comments
    [Pure]
    public static Boolean operator ==(Option<T> left,
                                      Option<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    public static Boolean operator !=(Option<T> left,
                                      Option<T> right)
    {
        return !left.Equals(right);
    }
#pragma warning restore
}
#endif

// IEquatable<T>
partial struct Option<T> : IEquatable<T>
{
    /// <inheritdoc/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
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

        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other);
    }
}

// IEquatable<T>
partial struct Option<T> : IEquatable<Option<T>>
{
    /// <inheritdoc/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
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

        return EqualityComparer<T?>.Default.Equals(x: m_Value,
                                                   y: other.m_Value);
    }
}