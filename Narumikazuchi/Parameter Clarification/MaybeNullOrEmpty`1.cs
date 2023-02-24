namespace Narumikazuchi;

/// <summary>
/// Represents a method parameter that allows <see langword="null"/> or an empty <see cref="IEnumerable"/> to be passed to it.
/// </summary>
public readonly partial struct MaybeNullOrEmpty<T>
    where T : IEnumerable
{
#pragma warning disable CS1591 // Missing comments
    static public implicit operator MaybeNullOrEmpty<T>(T? value)
    {
        return new(value);
    }

    static public implicit operator T?(MaybeNullOrEmpty<T> value)
    {
        return value.m_Value;
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of type <see cref="MaybeNullOrEmpty{T}"/>.
    /// </summary>
    /// <param name="value">The value this instance will be based on.</param>
    public MaybeNullOrEmpty(T? value)
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
        if (obj is MaybeNullOrEmpty<T> other)
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
    public readonly MaybeNullOrEmpty<T> WhenNull(Action action)
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
    public readonly MaybeNullOrEmpty<T> WhenNull<TResult>(
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
    /// Performs the specified <paramref name="action"/> when this instance does not contain any elements.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNullOrEmpty<T> WhenEmpty(Action action)
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
        else
        {
            IEnumerator enumerator = m_Value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                Action value = action;
                value.Invoke();
            }
        }

        return this;
    }

    /// <summary>
    /// Performs the specified <paramref name="transform"/> when this instance does not contains any elements.
    /// </summary>
    /// <param name="transform">The transform to perform.</param>
    /// <param name="result">The result of the <paramref name="transform"/>.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNullOrEmpty<T> WhenEmpty<TResult>(
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
            IEnumerator enumerator = m_Value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                Func<TResult> value = transform;
                result = value.Invoke();
            }
            else
            {
                result = default;
            }
        }

        return this;
    }

    /// <summary>
    /// Performs the specified <paramref name="action"/> when this instance does not represent <see langword="null"/>.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNullOrEmpty<T> WhenNotNull(Action<T> action)
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
    public readonly MaybeNullOrEmpty<T> WhenNotNull<TResult>(
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
    /// Performs the specified <paramref name="action"/> when this instance does contain any element.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNullOrEmpty<T> WhenNotEmpty(Action<T> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (m_Value is not null)
        {
            IEnumerator enumerator = m_Value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                Action<T> value = action;
                value.Invoke(m_Value);
            }
        }

        return this;
    }

    /// <summary>
    /// Performs the specified <paramref name="transform"/> when this instance does contain any element.
    /// </summary>
    /// <param name="transform">The transform to perform.</param>
    /// <param name="result">The result of the <paramref name="transform"/>.</param>
    /// <returns>Itself to fluently chain 'When*'-calls.</returns>
    public readonly MaybeNullOrEmpty<T> WhenNotEmpty<TResult>(
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
            IEnumerator enumerator = m_Value.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                Func<T, TResult> value = transform;
                result = value.Invoke(m_Value);
            }
            else
            {
                result = default;
            }
        }
        else
        {
            result = default;
        }

        return this;
    }

    /// <summary>
    /// Gets the value wrapped in the <see cref="MaybeNullOrEmpty{T}"/>.
    /// </summary>
    /// <param name="value">The value wrapped in the <see cref="MaybeNullOrEmpty{T}"/>.</param>
    /// <returns><see langword="true"/> if the <see cref="MaybeNullOrEmpty{T}"/> wraps a valid value; otherwise, <see langword="false"/>.</returns>
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

    /// <summary>
    /// Gets whether this instance has elements.
    /// </summary>
    public Boolean IsEmpty
    {
        get
        {
            if (m_Value is null)
            {
                return true;
            }

            IEnumerator enumerator = m_Value.GetEnumerator();
            return !enumerator.MoveNext();
        }
    }

    private readonly T? m_Value;
}