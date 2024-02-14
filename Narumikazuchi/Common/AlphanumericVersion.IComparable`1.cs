namespace Narumikazuchi;

public partial struct AlphanumericVersion : IComparable
{
    Int32 IComparable.CompareTo([AllowNull] Object? obj)
    {
        if (obj is AlphanumericVersion other)
        {
            return this.CompareTo(other);
        }
        else
        {
            return 1;
        }
    }
}

public partial struct AlphanumericVersion : IComparable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Int32 CompareTo(AlphanumericVersion other)
    {
        if (m_Value is null)
        {
            if (other.m_Value is null)
            {
                return 0;
            }
            else
            {
                throw new FailedInitialization();
            }
        }
        else
        {
            return m_Value.CompareTo(other.m_Value);
        }
    }
}