namespace Narumikazuchi;

/// <summary>
/// Represents an object that is synchronized (thread safe).
/// </summary>
public interface ISynchronized
{
    /// <summary>
    /// Gets a value indicating whether access to this <see cref="ISynchronized"/> is synchronized (thread safe).
    /// </summary>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public Boolean IsSynchronized { get; }

    /// <summary>
    /// Gets the object mutex used to synchronize access to this <see cref="ISynchronized"/>.
    /// </summary>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP1_0_OR_GREATER
    [NotNull]
#endif
    public Mutex SyncRoot { get; }
}