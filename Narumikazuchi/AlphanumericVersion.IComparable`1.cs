namespace Narumikazuchi;

public partial struct AlphanumericVersion : IComparable
{
    Int32 IComparable.CompareTo(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        Object? obj)
    {
        if (obj is AlphanumericVersion other)
        {
            return this.CompareTo(other);
        }
        return 1;
    }
}

public partial struct AlphanumericVersion : IComparable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Int32 CompareTo(AlphanumericVersion other)
    {
        Int32 result = CompareComponent(me: m_Major,
                                        other: other.m_Major);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(me: m_Minor,
                                  other: other.m_Minor);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(me: m_Build,
                                  other: other.m_Build);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(me: m_Revision,
                                  other: other.m_Revision);

        return result;
    }
}