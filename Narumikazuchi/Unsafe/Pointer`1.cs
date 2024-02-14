namespace Narumikazuchi;

#pragma warning disable CS8500
/// <summary>
/// Represents a reference to a struct or a reference to a reference for a class. Can be used in a safe context unlike regular pointers.
/// </summary>
public unsafe readonly struct Pointer<TAny>
{
    /// <summary>
    /// Creates a pointer from the address of the reference to the specified <typeparamref name="TAny"/>.
    /// </summary>
    /// <param name="t">The object to create a <see cref="Pointer{TAny}"/> of.</param>
    /// <returns>A <see cref="Pointer{TAny}"/> to the reference of <typeparamref name="TAny"/></returns>
    static public Pointer<TAny> AddressOf(ref TAny t)
    {
        TypedReference tr = __makeref(t);
        return new(*(IntPtr*)&tr);
    }

#pragma warning disable CS1591
    static public Pointer<TAny> operator ++(Pointer<TAny> pointer)
    {
        return pointer.Increment(1);
    }

    static public Pointer<TAny> operator --(Pointer<TAny> pointer)
    {
        return pointer.Decrement(1);
    }

    static public Pointer<TAny> operator +(Pointer<TAny> pointer,
                                           Int64 amount)
    {
        return pointer.Increment(amount);
    }

    static public Pointer<TAny> operator -(Pointer<TAny> pointer,
                                           Int64 amount)
    {
        return pointer.Decrement(amount);
    }

    static public implicit operator Pointer<TAny>(IntPtr pointer)
    {
        return new(pointer);
    }

    static public implicit operator Pointer<TAny>(UIntPtr pointer)
    {
        return new(pointer);
    }

    static public implicit operator Pointer<TAny>(TAny* pointer)
    {
        return new(pointer);
    }

    static public implicit operator Pointer<TAny>(void* pointer)
    {
        return new(pointer);
    }

    static public implicit operator IntPtr(Pointer<TAny> pointer)
    {
#pragma warning disable CA2020 // No need to check for overflow here
        return (IntPtr)(Int64)pointer.Address;
#pragma warning restore CA2020
    }

    static public implicit operator UIntPtr(Pointer<TAny> pointer)
    {
        return pointer.Address;
    }

    static public implicit operator TAny*(Pointer<TAny> pointer)
    {
        return (TAny*)pointer.Address.ToPointer();
    }

    static public implicit operator void*(Pointer<TAny> pointer)
    {
        return pointer.Address.ToPointer();
    }
#pragma warning restore CS1591

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{TAny}"/> struct.
    /// </summary>
    public Pointer(void* pointer)
    {
        m_Pointer = pointer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{TAny}"/> struct.
    /// </summary>
    public Pointer(TAny* pointer)
    {
        m_Pointer = pointer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{TAny}"/> struct.
    /// </summary>
    public Pointer(IntPtr pointer)
    {
        m_Pointer = pointer.ToPointer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{TAny}"/> struct.
    /// </summary>
    public Pointer(UIntPtr pointer)
    {
        m_Pointer = pointer.ToPointer();
    }

    /// <inheritdoc/>
    [return: MaybeNull]
    public override String? ToString()
    {
        if (this.Value is null)
        {
            return "null";
        }
        else
        {
            return this.Value.ToString();
        }
    }

    /// <summary>
    /// Gets or sets the value of where the pointer at the specified index points at.
    /// </summary>
    public TAny this[Int32 index]
    {
        get
        {
            return Unsafe.Read<TAny>(Offset(pointer: m_Pointer,
                                         index: index));
        }
        set
        {
            Unsafe.Write(destination: Offset(pointer: m_Pointer,
                                             index: index),
                         value: value);
        }
    }

    /// <summary>
    /// Gets the current address of this pointer.
    /// </summary>
    public UIntPtr Address
    {
        get
        {
            return (UIntPtr)m_Pointer;
        }
    }

    /// <summary>
    /// Gets or sets the value of where the pointer currently points at.
    /// </summary>
    /// <exception cref="NullReferenceException"/>
    public TAny Value
    {
        get
        {
            return Unsafe.Read<TAny>(m_Pointer);
        }
        set
        {
            Unsafe.Write(destination: m_Pointer,
                         value: value);
        }
    }

    static private void* Offset(void* pointer,
                                Int64 index)
    {
        Int64 offset = Unsafe.SizeOf<TAny>() * index;
        return (void*)((Int64)pointer + offset);
    }

    private readonly Pointer<TAny> Increment(Int64 amount)
    {
        return new(Offset(pointer: m_Pointer,
                          index: amount));
    }

    private readonly Pointer<TAny> Decrement(Int64 amount)
    {
        return new(Offset(pointer: m_Pointer,
                          index: -amount));
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly void* m_Pointer;
}