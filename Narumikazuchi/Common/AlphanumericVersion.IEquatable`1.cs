namespace Narumikazuchi;

public partial struct AlphanumericVersion : IEquatable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Boolean Equals(AlphanumericVersion other)
    {
        if (m_Value is null)
        {
            return other.m_Value is null;
        }
        else
        {
            return String.Equals(a: m_Value,
                                 b: other.m_Value,
                                 comparisonType: StringComparison.InvariantCultureIgnoreCase);
        }
    }
}