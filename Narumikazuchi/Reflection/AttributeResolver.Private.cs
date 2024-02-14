namespace Narumikazuchi;

static public partial class AttributeResolver
{
    static private Boolean MultipleAllowed<TAttribute>()
        where TAttribute : Attribute
    {
        if (s_MultipleAllowedCache.TryGetValue(key: typeof(TAttribute),
                                               value: out Boolean result))
        {
            return result;
        }
        else
        {
            AttributeUsageAttribute? usage = typeof(TAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            result = usage is not null &&
                     usage.AllowMultiple;

            s_MultipleAllowedCache.Add(key: typeof(TAttribute),
                                       value: result);

            return result;
        }
    }

    static private readonly Dictionary<Tuple<Assembly, Type>, Boolean> s_AssemblyHasAttributeCache = new();
    static private readonly Dictionary<Tuple<MemberInfo, Type>, Boolean> s_MemberHasAttributeCache = new();
    static private readonly Dictionary<Tuple<ParameterInfo, Type>, Boolean> s_ParameterHasAttributeCache = new();
    static private readonly Dictionary<Type, Boolean> s_MultipleAllowedCache = new();
    static private readonly Dictionary<Tuple<Assembly, Type>, ImmutableArray<Attribute>> s_AssemblyAttributesCache = new();
    static private readonly Dictionary<Tuple<MemberInfo, Type>, ImmutableArray<Attribute>> s_MemberAttributesCache = new();
    static private readonly Dictionary<Tuple<ParameterInfo, Type>, ImmutableArray<Attribute>> s_ParameterAttributesCache = new();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MULTIPLE_INSTANCES_ARE_DISALLOWED = "This method is supposed to only work with Attributes that disallow multiple instances.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String ATTRIBUTE_NOT_DEFINED_FOR_TARGET = "The specified Attribute has not been defined for the specified target.";
}