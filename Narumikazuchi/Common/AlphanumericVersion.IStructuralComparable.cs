namespace Narumikazuchi;

public partial struct AlphanumericVersion : IStructuralComparable
{
    Int32 IStructuralComparable.CompareTo(Object? other,
                                          IComparer comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);

        return comparer.Compare(x: this,
                                y: other);
    }
}