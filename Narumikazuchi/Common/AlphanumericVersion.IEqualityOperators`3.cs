namespace Narumikazuchi;

#pragma warning disable CS1591 // Missing comments
#if NET7_0_OR_GREATER
public partial struct AlphanumericVersion : IEqualityOperators<AlphanumericVersion, AlphanumericVersion, Boolean>
{
    /// <inheritdoc/>
    static public Boolean operator ==(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) == 0;
    }

    /// <inheritdoc/>
    static public Boolean operator !=(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) != 0;
    }
}
#else
public partial struct AlphanumericVersion
{
    static public Boolean operator ==(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) == 0;
    }

    static public Boolean operator !=(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) != 0;
    }
}
#endif
#pragma warning restore