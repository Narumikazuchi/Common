namespace Narumikazuchi;

/// <summary>
/// Represents a method parameter that allows <see langword="null"/> to be passed to it.
/// </summary>
public readonly partial struct MaybeNull<T>
{
#pragma warning disable CS1591 // Missing comments
    static public implicit operator MaybeNull<T>(T? value)
    {
        return new(value);
    }

    static public implicit operator MaybeNull<T>(NotNull<T> value)
    {
        return new((T)value);
    }

    static public implicit operator T?(MaybeNull<T> value)
    {
        return value.m_Value;
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of type <see cref="MaybeNull{T}"/>.
    /// </summary>
    /// <param name="value">The value this instance will be based on.</param>
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
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: MaybeNull]
#endif
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

    /// <summary>
    /// Performs the specified <paramref name="action"/> when this instance represents <see langword="null"/>.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNull<T> WhenNull(Action action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (m_Value is null)
        {
            Action value = action;
            value.Invoke();
        }

        return this;
    }

    /// <summary>
    /// Performs the specified <paramref name="transform"/> when this instance represents <see langword="null"/>.
    /// </summary>
    /// <param name="transform">The transform to perform.</param>
    /// <param name="result">The result of the <paramref name="transform"/>.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNull<T> WhenNull<TResult>(
        Func<TResult> transform,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out TResult? result)
    {
        if (transform is null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

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

    /// <summary>
    /// Performs the specified <paramref name="action"/> when this instance does not represent <see langword="null"/>.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNull<T> WhenNotNull(Action<T> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (m_Value is not null)
        {
            Action<T> value = action;
            value.Invoke(m_Value);
        }

        return this;
    }

    /// <summary>
    /// Performs the specified <paramref name="transform"/> when this instance does not represent <see langword="null"/>.
    /// </summary>
    /// <param name="transform">The transform to perform.</param>
    /// <param name="result">The result of the <paramref name="transform"/>.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNull<T> WhenNotNull<TResult>(
        Func<T, TResult> transform,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out TResult? result)
    {
        if (transform is null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

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
    public readonly Boolean TryGetValue(
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out T? value)
    {
        value = m_Value;
        return m_Value is not null;
    }

    /// <summary>
    /// Gets whether this instance represents <see langword="null"/>.
    /// </summary>
    public Boolean IsNull
    {
        get
        {
            return m_Value is null;
        }
    }

    private readonly T? m_Value;
}