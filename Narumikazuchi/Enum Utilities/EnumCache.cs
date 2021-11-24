namespace Narumikazuchi;

internal static class __EnumChache<TEnum> where TEnum : Enum
{
    static __EnumChache() => 
        DefinedValues = new HashSet<TEnum>(collection: Enum.GetValues(typeof(TEnum))
                                                           .Cast<TEnum>());

    public static HashSet<TEnum> DefinedValues { get; }
}