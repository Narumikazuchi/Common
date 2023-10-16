namespace Narumikazuchi.Collections;

/// <summary>
/// Represents the base class for an <see cref="IStrongEnumerable{TElement, TEnumerator}"/>.
/// </summary>
public abstract class StrongEnumerable<TElement, TEnumerator> : IStrongEnumerable<TElement, TEnumerator>
    where TEnumerator : struct, IStrongEnumerator<TElement>
{
    /// <inheritdoc/>
    public abstract TEnumerator GetEnumerator();
}