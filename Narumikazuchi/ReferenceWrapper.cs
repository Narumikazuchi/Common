namespace Narumikazuchi;

/// <summary>
/// Wraps the value of a reference type, to minimize the use of <see langword="null"/>.
/// </summary>
public readonly partial struct ReferenceWrapper<TReference>
        where TReference : class
{
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
                            in Boolean throwIfTryGetNull)
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

#pragma warning disable CS1591
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
    public static ReferenceWrapper<TReference> NoValue { get; } = new(reference: null);
}

// Non-Public
partial struct ReferenceWrapper<TReference>
{
    private readonly TReference? m_Reference;
    private readonly Boolean m_Throw;
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