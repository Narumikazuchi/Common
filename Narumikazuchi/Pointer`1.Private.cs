namespace Narumikazuchi;

#if NETCOREAPP3_0_OR_GREATER
public unsafe partial struct Pointer<T>
{
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
#endif