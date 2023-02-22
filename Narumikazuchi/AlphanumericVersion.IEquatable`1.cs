namespace Narumikazuchi;

public partial struct AlphanumericVersion : IEquatable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Boolean Equals(AlphanumericVersion other)
    {
        return this.CompareTo(other) == 0;
    }
}