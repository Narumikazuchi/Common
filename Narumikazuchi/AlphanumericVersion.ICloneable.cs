namespace Narumikazuchi;

public partial struct AlphanumericVersion : ICloneable
{
    /// <summary>
    /// Creates a new object that is an exact copy of this instance.
    /// </summary>
    /// <returns>A new object that is an exact copy of this instance.</returns>
    public AlphanumericVersion Clone()
    {
        return new(this);
    }

    /// <inheritdoc/>
    Object ICloneable.Clone()
    {
        return new AlphanumericVersion(this);
    }
}