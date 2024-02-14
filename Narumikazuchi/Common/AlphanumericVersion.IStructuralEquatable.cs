namespace Narumikazuchi;

public partial struct AlphanumericVersion : IStructuralEquatable
{
    Boolean IStructuralEquatable.Equals(Object? other,
                                        IEqualityComparer comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);

        return comparer.Equals(x: this,
                               y: other);
    }

    Int32 IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);

        return comparer.GetHashCode(this);
    }
}