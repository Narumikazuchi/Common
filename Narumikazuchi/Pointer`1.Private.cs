namespace Narumikazuchi;

#if NETCOREAPP3_0_OR_GREATER
public unsafe partial struct Pointer<T>
{
    private Pointer<T> Increment(Int64 amount)
    {
        m_Pointer = Offset(pointer: m_Pointer,
                           index: amount);
        return this;
    }

    private Pointer<T> Decrement(Int64 amount)
    {
        m_Pointer = Offset(pointer: m_Pointer,
                           index: -amount);
        return this;
    }

    static private void* Offset(void* pointer,
                                Int64 index)
    {
        Int64 offset = Unsafe.SizeOf<T>() * index;
        return (void*)((Int64)pointer + offset);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private void* m_Pointer;
}
#endif