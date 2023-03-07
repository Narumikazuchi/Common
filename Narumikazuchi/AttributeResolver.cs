using System.Diagnostics.CodeAnalysis;

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
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(assembly);
#else
        if (assembly is null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }
#endif

        return Attribute.IsDefined(element: assembly,
                                   attributeType: typeof(TAttribute));
    }
    /// <summary>
    /// Checks if the specified <see cref="MemberInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
    static public Boolean HasAttribute<TAttribute>([DisallowNull] MemberInfo info)
        where TAttribute : Attribute
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(info);
#else
        if (info is null)
        {
            throw new ArgumentNullException(nameof(info));
        }
#endif

        return Attribute.IsDefined(element: info,
                                   attributeType: typeof(TAttribute));
    }
    /// <summary>
    /// Checks if the specified <see cref="ParameterInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
    static public Boolean HasAttribute<TAttribute>([DisallowNull] ParameterInfo info)
         where TAttribute : Attribute
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(info);
#else
        if (info is null)
        {
            throw new ArgumentNullException(nameof(info));
        }
#endif

        return Attribute.IsDefined(element: info,
                                   attributeType: typeof(TAttribute));
    }

    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NETCOREAPP1_0_OR_GREATER
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    static public TAttribute[] FetchAllAttributes<TAttribute>(
#endif
        [DisallowNull] Assembly assembly)
            where TAttribute : Attribute
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(assembly);
#else
        if (assembly is null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }
#endif

        return assembly.GetCustomAttributes<TAttribute>()
#if NETCOREAPP1_0_OR_GREATER
                       .ToImmutableArray();
#else
                       .ToArray();
#endif
    }
    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NETCOREAPP1_0_OR_GREATER
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    static public TAttribute[] FetchAllAttributes<TAttribute>(
#endif
        [DisallowNull] MemberInfo info)
            where TAttribute : Attribute
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(info);
#else
        if (info is null)
        {
            throw new ArgumentNullException(nameof(info));
        }
#endif

        return info.GetCustomAttributes<TAttribute>()
#if NETCOREAPP1_0_OR_GREATER
                   .ToImmutableArray();
#else
                   .ToArray();
#endif
    }
    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NETCOREAPP1_0_OR_GREATER
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    static public TAttribute[] FetchAllAttributes<TAttribute>(
#endif
        [DisallowNull] ParameterInfo info)
            where TAttribute : Attribute
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(info);
#else
        if (info is null)
        {
            throw new ArgumentNullException(nameof(info));
        }
#endif

        return info.GetCustomAttributes<TAttribute>()
#if NETCOREAPP1_0_OR_GREATER
                   .ToImmutableArray();
#else
                   .ToArray();
#endif
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
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(assembly);
#else
        if (assembly is null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }
#endif

        if (MultipleAllowed<TAttribute>())
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        TAttribute? attribute = assembly.GetCustomAttribute<TAttribute>();
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
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(info);
#else
        if (info is null)
        {
            throw new ArgumentNullException(nameof(info));
        }
#endif

        if (MultipleAllowed<TAttribute>())
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        TAttribute? attribute = info.GetCustomAttribute<TAttribute>();
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
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(info);
#else
        if (info is null)
        {
            throw new ArgumentNullException(nameof(info));
        }
#endif

        if (MultipleAllowed<TAttribute>())
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
        }

        TAttribute? attribute = info.GetCustomAttribute<TAttribute>();
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
        AttributeUsageAttribute? usage = typeof(TAttribute).GetCustomAttribute<AttributeUsageAttribute>();

        return usage is not null &&
               usage.AllowMultiple;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MULTIPLE_INSTANCES_ARE_DISALLOWED = "This method is supposed to only work with Attributes that disallow multiple instances.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String ATTRIBUTE_NOT_DEFINED_FOR_TARGET = "The specified Attribute has not been defined for the specified target.";
}