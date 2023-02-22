namespace Narumikazuchi;

#pragma warning disable CS1591 // Missing comments
public partial struct MaybeNullOrEmpty<T>
{
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean operator ==(T? left,
                                      MaybeNullOrEmpty<T> right)
    {
        return right.Equals(left);
    }

#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean operator !=(T? left,
                                      MaybeNullOrEmpty<T> right)
    {
        return !right.Equals(left);
    }
}

#if NET7_0_OR_GREATER
public partial struct MaybeNullOrEmpty<T> : IEqualityOperators<MaybeNullOrEmpty<T>, T, Boolean>
{
    [Pure]
    static public Boolean operator ==(MaybeNullOrEmpty<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNullOrEmpty<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}

public partial struct MaybeNullOrEmpty<T> : IEqualityOperators<MaybeNullOrEmpty<T>, MaybeNullOrEmpty<T>, Boolean>
{
    [Pure]
    static public Boolean operator ==(MaybeNullOrEmpty<T> left,
                                      MaybeNullOrEmpty<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNullOrEmpty<T> left,
                                      MaybeNullOrEmpty<T> right)
    {
        return !left.Equals(right);
    }
}
#else
public partial struct MaybeNullOrEmpty<T>
{
    [Pure]
    static public Boolean operator ==(MaybeNullOrEmpty<T> left,
                                      MaybeNullOrEmpty<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNullOrEmpty<T> left,
                                      MaybeNullOrEmpty<T> right)
    {
        return !left.Equals(right);
    }

    [Pure]
    static public Boolean operator ==(MaybeNullOrEmpty<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(MaybeNullOrEmpty<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}
#endif
#pragma warning restore