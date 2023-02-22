namespace Narumikazuchi;

public partial struct AlphanumericVersion : IStructuralEquatable
{
    Boolean IStructuralEquatable.Equals(Object? other,
                                        IEqualityComparer comparer)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(comparer);
#else
        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
#endif

        return comparer.Equals(x: this,
                               y: other);
    }

    Int32 IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(comparer);
#else
        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
#endif

        return comparer.GetHashCode(this);
    }
}