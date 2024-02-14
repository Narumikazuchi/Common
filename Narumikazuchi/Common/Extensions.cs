namespace Narumikazuchi;

/// <summary>
/// Contains common extensions.
/// </summary>
static public class Extensions
{
    /// <summary>
    /// Checks whether the <paramref name="instance"/> is <see langword="null"/> or in case of a value type <see langword="default"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public Boolean IsNullOrDefault<TObject>([NotNullWhen(false)] this TObject instance)
    {
        if (instance is ValueType)
        {
            return instance.Equals(obj: default(TObject)) is true;
        }
        else
        {
            return instance is null;
        }
    }

    /// <summary>
    /// Checks whether the <paramref name="instance"/> is not <see langword="null"/> or in case of a value type not <see langword="default"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public Boolean IsNotNullOrDefault<TObject>([NotNullWhen(true)] this TObject instance)
    {
        if (instance is ValueType)
        {
            return instance.Equals(obj: default(TObject)) is false;
        }
        else
        {
            return instance is not null;
        }
    }
}