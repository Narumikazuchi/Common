using System.Diagnostics.CodeAnalysis;

namespace Narumikazuchi;

#if NETCOREAPP3_0_OR_GREATER
/// <summary>
/// Represents a reference to a struct or a reference to a reference for a class.
/// </summary>
public unsafe partial struct Pointer<T>
{
    /// <summary>
    /// Creates a pointer from the address of the reference to the specified <typeparamref name="T"/>.
    /// </summary>
    /// <param name="t">The object to create a <see cref="Pointer{T}"/> of.</param>
    /// <returns>A <see cref="Pointer{T}"/> to the reference of <typeparamref name="T"/></returns>
    static public Pointer<T> AddressOf(ref T t)
    {
        TypedReference tr = __makeref(t);
        return new(pointer: *(IntPtr*)&tr);
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
        return new(pointer: pointer);
    }

    static public implicit operator Pointer<T>(void* pointer)
    {
        return new(pointer: pointer);
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

    /// <inheritdoc/>
    [return: MaybeNull]
    public override String? ToString()
    {
        if (this.Value is null)
        {
            return null;
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
    public IntPtr Address
    {
        get
        {
            return (IntPtr)m_Pointer;
        }
    }

    /// <summary>
    /// Gets or sets the value of where the pointer currently points at.
    /// </summary>
    [MaybeNull]
    public T? Value
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
}
#endif