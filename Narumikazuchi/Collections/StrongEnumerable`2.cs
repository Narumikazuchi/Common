namespace Narumikazuchi.Collections;

/// <summary>
/// Represents the base class for an <see cref="IStrongEnumerable{TElement, TEnumerator}"/>.
/// </summary>
public abstract class StrongEnumerable<TElement, TEnumerator> : IStrongEnumerable<TElement, TEnumerator>
    where TEnumerator : struct, IStrongEnumerator<TElement>
{
    /// <inheritdoc/>
    public abstract TEnumerator GetEnumerator();

#if !NETCOREAPP3_1_OR_GREATER
    IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
#endif
}