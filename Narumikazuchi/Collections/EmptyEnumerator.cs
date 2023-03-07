using System.Diagnostics.CodeAnalysis;

namespace Narumikazuchi.Collections;

/// <summary>
/// Represents an <see cref="IStrongEnumerator{TElement}"/> for a collection without any elements.
/// </summary>
public struct EmptyEnumerator<TElement> : IStrongEnumerator<TElement>
{
    /// <inheritdoc/>
    public Boolean MoveNext()
    {
        return false;
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"/>
    [NotNull]
    public TElement Current
    {
        get
        {
            throw new InvalidOperationException();
        }
    }

#if !NETCOREAPP3_1_OR_GREATER
    void IDisposable.Dispose()
    { }

    void IEnumerator.Reset()
    { }

    Object? IEnumerator.Current
    {
        get
        {
            return this.Current;
        }
    }
#endif
}