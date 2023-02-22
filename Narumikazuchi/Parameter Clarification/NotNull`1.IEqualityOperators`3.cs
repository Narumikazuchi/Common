namespace Narumikazuchi;

#pragma warning disable CS1591 // Missing comments
public partial struct NotNull<T>
{
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean operator ==(T? left,
                                      NotNull<T> right)
    {
        return right.Equals(left);
    }

#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean operator !=(T? left,
                                      NotNull<T> right)
    {
        return !right.Equals(left);
    }
}

#if NET7_0_OR_GREATER
public partial struct NotNull<T> : IEqualityOperators<NotNull<T>, T, Boolean>
{
    [Pure]
    static public Boolean operator ==(NotNull<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(NotNull<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}

public partial struct NotNull<T> : IEqualityOperators<NotNull<T>, NotNull<T>, Boolean>
{
    [Pure]
    static public Boolean operator ==(NotNull<T> left,
                                      NotNull<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(NotNull<T> left,
                                      NotNull<T> right)
    {
        return !left.Equals(right);
    }
}
#else
public partial struct NotNull<T>
{
    [Pure]
    static public Boolean operator ==(NotNull<T> left,
                                      NotNull<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(NotNull<T> left,
                                      NotNull<T> right)
    {
        return !left.Equals(right);
    }

    [Pure]
    static public Boolean operator ==(NotNull<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    [Pure]
    static public Boolean operator !=(NotNull<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}
#endif
#pragma warning restore