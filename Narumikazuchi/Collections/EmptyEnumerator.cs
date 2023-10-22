namespace Narumikazuchi.Collections;

/// <summary>
/// Represents an <see cref="IStrongEnumerator{TElement}"/> for a collection without any elements.
/// </summary>
public struct EmptyEnumerator<TElement> : IStrongEnumerator<TElement>
{
    /// <inheritdoc/>
    public readonly Boolean MoveNext()
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
}