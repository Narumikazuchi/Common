namespace Narumikazuchi;

#if NET5_0_OR_GREATER && !NET7_0_OR_GREATER
/// <summary>
/// Wraps the value of a reference type, to minimize the use of <see langword="null"/>.
/// </summary>
[Obsolete("Deprecated in favor of MaybeNull<T>, NotNull<T> and the empty versions of these structs.", false)]
public readonly partial struct ReferenceWrapper<TReference>
    where TReference : class
{
    /// <summary>
    /// Creates a new <see cref="ReferenceWrapper{TReference}"/> and populates it with a result. Pass in <see langword="null"/>
    /// if the <see cref="Task"/> failed.
    /// </summary>
    public ReferenceWrapper() :
        this(reference: default,
             throwIfTryGetNull: true)
    { }
    /// <summary>
    /// Creates a new <see cref="ReferenceWrapper{TReference}"/> and populates it with a result. Pass in <see langword="null"/>
    /// if the <see cref="Task"/> failed.
    /// </summary>
    /// <param name="reference">The reference to wrap.</param>
    public ReferenceWrapper([AllowNull] TReference? reference) :
        this(reference: reference,
             throwIfTryGetNull: true)
    { }
    /// <summary>
    /// Creates a new <see cref="ReferenceWrapper{TReference}"/> and populates it with a result. Pass in <see langword="null"/>
    /// if the <see cref="Task"/> failed.
    /// </summary>
    /// <param name="reference">The reference to wrap.</param>
    /// <param name="throwIfTryGetNull">Whether the struct is supposed to throw a <see cref="NullReferenceException"/> upon trying to access a <see langword="null"/> reference.</param>
    public ReferenceWrapper([AllowNull] TReference? reference,
                            Boolean throwIfTryGetNull)
    {
        m_Reference = reference;
        m_Throw = throwIfTryGetNull;
    }

    /// <summary>
    /// Tries to retrieve the reference.
    /// </summary>
    /// <param name="reference">The wrapped reference.</param>
    /// <returns><see langword="true"/> if the reference is not <see langword="null"/>; otherwise, <see langword="false"/></returns>
    public Boolean TryGet([NotNullWhen(true)] out TReference? reference)
    {
        if (m_Reference is null)
        {
            reference = default;
            return false;
        }
        reference = m_Reference;
        return true;
    }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    {
        if (m_Reference is null)
        {
            return Int32.MaxValue;
        }
        else
        {
            return m_Reference.GetHashCode();
        }
    }

    /// <inheritdoc/>
    public override Boolean Equals(Object? obj)
    {
        if (obj is TReference other)
        {
            return other.Equals(m_Reference);
        }
        else if (obj is ReferenceWrapper<TReference> wrapper)
        {
            return this.Equals(wrapper);
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public override String? ToString()
    {
        if (m_Reference is null)
        {
            return "null";
        }
        return m_Reference.ToString();
    }

#pragma warning disable CS1591
    public static Boolean operator ==(ReferenceWrapper<TReference> left, ReferenceWrapper<TReference> right)
    {
        return left.Equals(right);
    }

    public static Boolean operator !=(ReferenceWrapper<TReference> left, ReferenceWrapper<TReference> right)
    {
        return !left.Equals(right);
    }

    public static implicit operator TReference?(ReferenceWrapper<TReference> wrapper)
    {
        if (wrapper.TryGet(out TReference? reference))
        {
            return reference;
        }

        if (wrapper.m_Throw)
        {
            throw new NullReferenceException();
        }
        return default;
    }

    public static implicit operator ReferenceWrapper<TReference>(TReference? reference)
    {
        return new(reference: reference);
    }
#pragma warning restore

    /// <summary>
    /// Gets whether the reference is set to anything else but <see langword="null"/>.
    /// </summary>
    public Boolean HasValue =>
        m_Reference is not null;

    /// <summary>
    /// Gets a wrapper for a <see langword="null"/> reference.
    /// </summary>
    public static ReferenceWrapper<TReference> NoValue { get; } = new(reference: default);
}

// Non-Public
partial struct ReferenceWrapper<TReference>
{
    private readonly TReference? m_Reference;
    private readonly Boolean m_Throw;
}

// IEquatable<T>
partial struct ReferenceWrapper<TReference> : IEquatable<TReference>
{
    /// <inheritdoc/>
    public Boolean Equals(TReference? other)
    {
        if (m_Reference is null &&
            other is null)
        {
            return true;
        }
        if (m_Reference is null)
        {
            return false;
        }

        return ReferenceEquals(objA: m_Reference,
                               objB: other);
    }
}

// IEquatable<T>
partial struct ReferenceWrapper<TReference> : IEquatable<ReferenceWrapper<TReference>>
{
    /// <inheritdoc/>
    public Boolean Equals(ReferenceWrapper<TReference> other)
    {
        if (m_Reference is null &&
            other.m_Reference is null)
        {
            return true;
        }
        if (m_Reference is null)
        {
            return false;
        }

        return ReferenceEquals(objA: m_Reference,
                               objB: other.m_Reference);
    }
}
#endif