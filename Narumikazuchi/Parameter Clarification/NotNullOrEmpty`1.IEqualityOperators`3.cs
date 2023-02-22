namespace Narumikazuchi;

#pragma warning disable CS1591 // Missing comments
public partial struct NotNullOrEmpty<T>
{
    static public Boolean operator ==(T? left,
                                      NotNullOrEmpty<T> right)
    {
        return right.Equals(left);
    }

    static public Boolean operator !=(T? left,
                                      NotNullOrEmpty<T> right)
    {
        return !right.Equals(left);
    }
}

#if NET7_0_OR_GREATER
public partial struct NotNullOrEmpty<T> : IEqualityOperators<NotNullOrEmpty<T>, T, Boolean>
{
    static public Boolean operator ==(NotNullOrEmpty<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    static public Boolean operator !=(NotNullOrEmpty<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}

public partial struct NotNullOrEmpty<T> : IEqualityOperators<NotNullOrEmpty<T>, NotNullOrEmpty<T>, Boolean>
{
    static public Boolean operator ==(NotNullOrEmpty<T> left,
                                      NotNullOrEmpty<T> right)
    {
        return left.Equals(right);
    }

    static public Boolean operator !=(NotNullOrEmpty<T> left,
                                      NotNullOrEmpty<T> right)
    {
        return !left.Equals(right);
    }
}
#else
public partial struct NotNullOrEmpty<T>
{
    static public Boolean operator ==(NotNullOrEmpty<T> left,
                                      NotNullOrEmpty<T> right)
    {
        return left.Equals(right);
    }

    static public Boolean operator !=(NotNullOrEmpty<T> left,
                                      NotNullOrEmpty<T> right)
    {
        return !left.Equals(right);
    }

    static public Boolean operator ==(NotNullOrEmpty<T> left,
                                      T? right)
    {
        return left.Equals(right);
    }

    static public Boolean operator !=(NotNullOrEmpty<T> left,
                                      T? right)
    {
        return !left.Equals(right);
    }
}
#endif
#pragma warning restore