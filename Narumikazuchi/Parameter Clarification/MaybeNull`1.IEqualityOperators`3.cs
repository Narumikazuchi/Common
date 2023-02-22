namespace Narumikazuchi;

#pragma warning disable CS1591 // Missing comments
public partial struct MaybeNull<T>
{
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean operator ==(T? left,
                                      MaybeNull<T> right)
    {
        return right.Equals(left);
    }

#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean operator !=(T? left,
                                      MaybeNull<T> right)
    {
        return !right.Equals(left);
    }
}

#if NET7_0_OR_GREATER
public partial struct MaybeNull<T> : IEqualityOperators<MaybeNull<T>, T, Boolean>
{
    [Pure]
    static public Boolean operator ==(MaybeNull<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNull<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}

public partial struct MaybeNull<T> : IEqualityOperators<MaybeNull<T>, MaybeNull<T>, Boolean>
{
    [Pure]
    static public Boolean operator ==(MaybeNull<T> left,
                                      MaybeNull<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNull<T> left,
                                      MaybeNull<T> right)
    {
        return !left.Equals(right);
    }
}
#else
public partial struct MaybeNull<T>
{
    [Pure]
    static public Boolean operator ==(MaybeNull<T> left,
                                      MaybeNull<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNull<T> left,
                                      MaybeNull<T> right)
    {
        return !left.Equals(right);
    }

    [Pure]
    static public Boolean operator ==(MaybeNull<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNull<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}
#endif
#pragma warning restore