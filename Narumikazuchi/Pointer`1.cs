namespace Narumikazuchi;

#pragma warning disable CS8500
/// <summary>
/// Represents a reference to a struct or a reference to a reference for a class. Can be used in a safe context unlike regular pointers.
/// </summary>
public unsafe readonly struct Pointer<T>
{
    /// <summary>
    /// Creates a pointer from the address of the reference to the specified <typeparamref name="T"/>.
    /// </summary>
    /// <param name="t">The object to create a <see cref="Pointer{T}"/> of.</param>
    /// <returns>A <see cref="Pointer{T}"/> to the reference of <typeparamref name="T"/></returns>
    static public Pointer<T> AddressOf(ref T t)
    {
        TypedReference tr = __makeref(t);
        return new(*(IntPtr*)&tr);
    }

#pragma warning disable CS1591 // Missing comments
    static public Pointer<T> operator ++(Pointer<T> pointer)
    {
        return pointer.Increment(1);
    }

    static public Pointer<T> operator --(Pointer<T> pointer)
    {
        return pointer.Decrement(1);
    }

    static public Pointer<T> operator +(Pointer<T> pointer,
                                        Int64 amount)
    {
        return pointer.Increment(amount);
    }

    static public Pointer<T> operator -(Pointer<T> pointer,
                                        Int64 amount)
    {
        return pointer.Decrement(amount);
    }

    static public implicit operator Pointer<T>(IntPtr pointer)
    {
        return new(pointer);
    }

    static public implicit operator Pointer<T>(UIntPtr pointer)
    {
        return new(pointer);
    }

    static public implicit operator Pointer<T>(void* pointer)
    {
        return new(pointer);
    }

    static public implicit operator IntPtr(Pointer<T> pointer)
    {
#pragma warning disable CA2020 // No need to check for overflow here
        return (IntPtr)(Int64)pointer.Address;
#pragma warning restore CA2020
    }

    static public implicit operator UIntPtr(Pointer<T> pointer)
    {
        return pointer.Address;
    }

    static public implicit operator void*(Pointer<T> pointer)
    {
        return pointer.Address.ToPointer();
    }
#pragma warning restore

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{T}"/> struct.
    /// </summary>
    public Pointer(void* pointer)
    {
        m_Pointer = pointer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{T}"/> struct.
    /// </summary>
    public Pointer(IntPtr pointer)
    {
        m_Pointer = pointer.ToPointer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{T}"/> struct.
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

        return this.Value.ToString();
    }

    /// <summary>
    /// Gets or sets the value of where the pointer at the specified index points at.
    /// </summary>
    public T this[Int32 index]
    {
        get
        {
            return Unsafe.Read<T>(Offset(pointer: m_Pointer,
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
    public T Value
    {
        get
        {
            return Unsafe.Read<T>(m_Pointer);
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
        Int64 offset = Unsafe.SizeOf<T>() * index;
        return (void*)((Int64)pointer + offset);
    }

    private readonly Pointer<T> Increment(Int64 amount)
    {
        return new(Offset(pointer: m_Pointer,
                          index: amount));
    }

    private readonly Pointer<T> Decrement(Int64 amount)
    {
        return new(Offset(pointer: m_Pointer,
                          index: -amount));
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly void* m_Pointer;
}