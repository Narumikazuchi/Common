namespace Narumikazuchi;

public partial struct AlphanumericVersion : IStructuralComparable
{
    Int32 IStructuralComparable.CompareTo(Object? other,
                                          IComparer comparer)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(comparer);
#else
        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
#endif

        return comparer.Compare(x: this,
                                y: other);
    }
}