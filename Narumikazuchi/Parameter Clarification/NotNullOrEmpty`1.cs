namespace Narumikazuchi;

/// <summary>
/// Represents a method parameter that neither allows <see langword="null"/> nor an empty <see cref="IEnumerable"/> to be passed to it.
/// </summary>
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

    static public implicit operator MaybeNull<T>(NotNullOrEmpty<T> value)
    {
        return new((T)value);
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    [return: NotNull]
# endif
    static public implicit operator T(NotNullOrEmpty<T> value)
    {
        return value.m_Value;
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of type <see cref="NotNullOrEmpty{T}"/>.
    /// </summary>
    /// <param name="value">The value this instance will be based on.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public NotNullOrEmpty(T? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(message: "This parameter is not supposed to be null.",
                                            paramName: null);
        }
        
        IEnumerator enumerator = value.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new ArgumentException(message: "This parameter represents an empty collection of elements, which is not allowed in this method.");
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

    private readonly T m_Value;
}