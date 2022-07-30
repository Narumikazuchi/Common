namespace Narumikazuchi;

internal static class __EnumChache<TEnum> 
    where TEnum : Enum
{
    private static HashSet<TEnum> EnumerateValues() =>
        new(collection: Enum.GetValues(typeof(TEnum))
                            .Cast<TEnum>());

    internal static Lazy<HashSet<TEnum>> DefinedValues { get; } = new(valueFactory: EnumerateValues,
                                                                      mode: LazyThreadSafetyMode.ExecutionAndPublication);
}