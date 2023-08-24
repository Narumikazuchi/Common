namespace Narumikazuchi;

/// <summary>
/// Represents a 31-bit integer positive range, basically representing a non-negative integer in the range of <code>0-2'147'483'647</code>. 
/// </summary>
public readonly struct Unsigned31BitInteger
{
    /// <summary>
    /// Implicit conversion to <see cref="Int32"/> struct.
    /// </summary>
    static public implicit operator Int32(Unsigned31BitInteger source)
    {
        return source.Value;
    }

    /// <summary>
    /// Explicit conversion to <see cref="UInt32"/> struct.
    /// </summary>
    static public explicit operator UInt32(Unsigned31BitInteger source)
    {
        return (UInt32)source.Value;
    }

    /// <summary>
    /// Implicit conversion from <see cref="Int32"/> struct.
    /// </summary>
    static public implicit operator Unsigned31BitInteger(Int32 source)
    {
        if (source < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(source));
        }

        return new(source);
    }

    /// <summary>
    /// Explicit conversion from <see cref="UInt32"/> struct.
    /// </summary>
    static public explicit operator Unsigned31BitInteger(UInt32 source)
    {
        if (source > Int32.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(source));
        }

        return new((Int32)source);
    }

    /// <summary>
    /// Gets the actual value of the struct.
    /// </summary>
    public Int32 Value
    {
        get;
    }

    private Unsigned31BitInteger(Int32 source)
    {
        this.Value = source;
    }
}