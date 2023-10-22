﻿#pragma warning disable IDE0270
namespace Narumikazuchi;

/// <summary>
/// Checks if a custom attribute is defined or fetches the custom attributes applied to a type, method, parameter etc.
/// </summary>
static public class AttributeResolver
{
    /// <summary>
    /// Checks if the specified <see cref="Assembly"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
    static public Boolean HasAttribute<TAttribute>([DisallowNull] Assembly assembly)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(assembly);

        Tuple<Assembly, Type> key = new(assembly, typeof(TAttribute));
        if (s_AssemblyHasAttributeCache.TryGetValue(key: key,
                                                    value: out Boolean result))
        {
            return result;
        }
        else
        {
            result = Attribute.IsDefined(element: assembly,
                                         attributeType: typeof(TAttribute));
            s_AssemblyHasAttributeCache.Add(key: key,
                                            value: result);
            return result;
        }
    }
    /// <summary>
    /// Checks if the specified <see cref="MemberInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
    static public Boolean HasAttribute<TAttribute>([DisallowNull] MemberInfo info)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(info);

        Tuple<MemberInfo, Type> key = new(info, typeof(TAttribute));
        if (s_MemberHasAttributeCache.TryGetValue(key: key,
                                                  value: out Boolean result))
        {
            return result;
        }
        else
        {
            result = Attribute.IsDefined(element: info,
                                         attributeType: typeof(TAttribute));
            s_MemberHasAttributeCache.Add(key: key,
                                          value: result);
            return result;
        }
    }
    /// <summary>
    /// Checks if the specified <see cref="ParameterInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
    static public Boolean HasAttribute<TAttribute>([DisallowNull] ParameterInfo info)
         where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(info);

        Tuple<ParameterInfo, Type> key = new(info, typeof(TAttribute));
        if (s_ParameterHasAttributeCache.TryGetValue(key: key,
                                                     value: out Boolean result))
        {
            return result;
        }
        else
        {
            result = Attribute.IsDefined(element: info,
                                         attributeType: typeof(TAttribute));
            s_ParameterHasAttributeCache.Add(key: key,
                                             value: result);
            return result;
        }
    }

    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>([DisallowNull] Assembly assembly)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(assembly);

        Tuple<Assembly, Type> key = new(assembly, typeof(TAttribute));
        if (s_AssemblyAttributesCache.TryGetValue(key: key,
                                                  value: out ImmutableArray<Attribute> result))
        {
            return result.Cast<TAttribute>()
                         .ToImmutableArray();
        }
        else
        {
            result = assembly.GetCustomAttributes<TAttribute>()
                             .ToImmutableArray<Attribute>();

            s_AssemblyAttributesCache.Add(key: key,
                                          value: result);

            return result.Cast<TAttribute>()
                         .ToImmutableArray();
        }
    }
    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>([DisallowNull] MemberInfo info)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(info);

        Tuple<MemberInfo, Type> key = new(info, typeof(TAttribute));
        if (s_MemberAttributesCache.TryGetValue(key: key,
                                                value: out ImmutableArray<Attribute> result))
        {
            return result.Cast<TAttribute>()
                         .ToImmutableArray();
        }
        else
        {
            result = info.GetCustomAttributes<TAttribute>()
                         .ToImmutableArray<Attribute>();

            s_MemberAttributesCache.Add(key: key,
                                        value: result);

            return result.Cast<TAttribute>()
                         .ToImmutableArray();
        }
    }
    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>([DisallowNull] ParameterInfo info)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(info);

        Tuple<ParameterInfo, Type> key = new(info, typeof(TAttribute));
        if (s_ParameterAttributesCache.TryGetValue(key: key,
                                                   value: out ImmutableArray<Attribute> result))
        {
            return result.Cast<TAttribute>()
                         .ToImmutableArray();
        }
        else
        {
            result = info.GetCustomAttributes<TAttribute>()
                         .ToImmutableArray<Attribute>();

            s_ParameterAttributesCache.Add(key: key,
                                           value: result);

            return result.Cast<TAttribute>()
                         .ToImmutableArray();
        }
    }

    /// <summary>
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    [return: NotNull]
    static public TAttribute FetchSingleAttribute<TAttribute>([DisallowNull] Assembly assembly)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(assembly);

        if (MultipleAllowed<TAttribute>())
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        TAttribute? attribute = FetchAllAttributes<TAttribute>(assembly).FirstOrDefault();
        if (attribute is null)
        {
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        return attribute;
    }
    /// <summary>
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    [return: NotNull]
    static public TAttribute FetchSingleAttribute<TAttribute>([DisallowNull] MemberInfo info)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(info);

        if (MultipleAllowed<TAttribute>())
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        TAttribute? attribute = FetchAllAttributes<TAttribute>(info).FirstOrDefault();
        if (attribute is null)
        {
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        return attribute;
    }
    /// <summary>
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    [return: NotNull]
    static public TAttribute FetchSingleAttribute<TAttribute>([DisallowNull] ParameterInfo info)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(info);

        if (MultipleAllowed<TAttribute>())
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        TAttribute? attribute = FetchAllAttributes<TAttribute>(info).FirstOrDefault();
        if (attribute is null)
        {
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        return attribute;
    }

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