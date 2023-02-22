namespace Narumikazuchi.Collections;

public struct EmptyEnumerator<TElement> : IStrongEnumerator<TElement>
{
    public TElement Current
    {
        get
        {
            throw new InvalidOperationException();
        }
    }

    public Boolean MoveNext()
    {
        return false;
    }

#if !NETCOREAPP3_1_OR_GREATER
    void IDisposable.Dispose()
    { }

    void IEnumerator.Reset()
    { }

    Object? IEnumerator.Current
    {
        get
        {
            return this.Current;
        }
    }
#endif
}