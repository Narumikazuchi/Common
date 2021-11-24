﻿namespace Narumikazuchi;

/// <summary>
/// Represents a reference to a struct or a reference to a reference for a class.
/// </summary>
public unsafe partial struct Pointer<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{T}"/> struct.
    /// </summary>
    public Pointer(void* pointer) => 
        this._pointer = pointer;
    /// <summary>
    /// Initializes a new instance of the <see cref="Pointer{T}"/> struct.
    /// </summary>
    public Pointer(IntPtr pointer) => 
        this._pointer = pointer.ToPointer();

    /// <inheritdoc/>
    [Pure]
    [return: MaybeNull]
    public override String? ToString() => 
        this.Value?
            .ToString();

    /// <summary>
    /// Gets or sets the value of where the pointer at the specified index points at.
    /// </summary>
    public T this[Int32 index]
    {
        get => Unsafe.Read<T>(source: Offset(pointer: this._pointer, 
                                             index: index));
        set => Unsafe.Write(destination: Offset(pointer: this._pointer, 
                                                index: index), 
                            value: value);
    }

    /// <summary>
    /// Gets the current address of this pointer.
    /// </summary>
    [Pure]
    public IntPtr Address =>
        (IntPtr)this._pointer;

    /// <summary>
    /// Gets or sets the value of where the pointer currently points at.
    /// </summary>
    [MaybeNull]
    public T? Value
    {
        get => Unsafe.Read<T>(source: this._pointer);
        set => Unsafe.Write(destination: this._pointer, 
                            value: value);
    }
}

// Non-Public
unsafe partial struct Pointer<T>
{
    private Pointer<T> Increment(Int64 amount)
    {
        this._pointer = Offset(pointer: this._pointer, 
                               index: amount);
        return this;
    }

    private Pointer<T> Decrement(Int64 amount)
    {
        this._pointer = Offset(pointer: this._pointer,
                               index: -amount);
        return this;
    }

    private static void* Offset(void* pointer,
                                Int64 index)
    {
        Int64 offset = Unsafe.SizeOf<T>() * index;
        return (void*)(((Int64)pointer) + offset);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private void* _pointer;
}

// Static
unsafe partial struct Pointer<T>
{
    /// <summary>
    /// Creates a pointer from the address of the reference to the specified <typeparamref name="T"/>.
    /// </summary>
    /// <param name="t">The object to create a <see cref="Pointer{T}"/> of.</param>
    /// <returns>A <see cref="Pointer{T}"/> to the reference of <typeparamref name="T"/></returns>
    public static Pointer<T> AddressOf(ref T t)
    {
        TypedReference tr = __makeref(t);
        return new(pointer: *(IntPtr*)&tr);
    }

#pragma warning disable
    public static Pointer<T> operator ++(Pointer<T> pointer) =>
        pointer.Increment(amount: 1);

    public static Pointer<T> operator --(Pointer<T> pointer) =>
        pointer.Decrement(amount: 1);

    public static Pointer<T> operator +(Pointer<T> pointer, 
                                        Int64 amount) =>
        pointer.Increment(amount: amount);

    public static Pointer<T> operator -(Pointer<T> pointer, 
                                        Int64 amount) =>
        pointer.Decrement(amount: amount);

    public static implicit operator Pointer<T>(IntPtr pointer) =>
        new(pointer: pointer);

    public static implicit operator Pointer<T>(void* pointer) =>
        new(pointer: pointer);
#pragma warning restore
}