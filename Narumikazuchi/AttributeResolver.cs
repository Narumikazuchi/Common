namespace Narumikazuchi;

#if NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
/// <summary>
/// Checks if a custom attribute is defined or fetches the custom attributes applied to a type, method, parameter etc.
/// </summary>
// Non-Public Members
public static partial class AttributeResolver
{
    private static Boolean MultipleAllowed<TAttribute>()
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

// Assemblies
partial class AttributeResolver
{
    /// <summary>
    /// Checks if the specified <see cref="Assembly"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public static Boolean HasAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        Assembly assembly)
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
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP1_0_OR_GREATER
    public static ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    public static TAttribute[] FetchAllAttributes<TAttribute>(
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        Assembly assembly)
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
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static TAttribute FetchSingleAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        Assembly assembly)
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
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 new KeyValuePair<Object, Object?>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, Object?>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        TAttribute? attribute = assembly.GetCustomAttribute<TAttribute>();
        if (attribute is null)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 new KeyValuePair<Object, Object?>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, Object?>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        return attribute;
    }
}

// MemberInfo
partial class AttributeResolver
{
    /// <summary>
    /// Checks if the specified <see cref="MemberInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public static Boolean HasAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        MemberInfo info)
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
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP1_0_OR_GREATER
    public static ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    public static TAttribute[] FetchAllAttributes<TAttribute>(
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        MemberInfo info)
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
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static TAttribute FetchSingleAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        MemberInfo info)
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
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 new KeyValuePair<Object, Object?>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, Object?>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        TAttribute? attribute = info.GetCustomAttribute<TAttribute>();
        if (attribute is null)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 new KeyValuePair<Object, Object?>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, Object?>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        return attribute;
    }
}

// Parameters
partial class AttributeResolver
{
    /// <summary>
    /// Checks if the specified <see cref="ParameterInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    public static Boolean HasAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        ParameterInfo info)
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
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP1_0_OR_GREATER
    public static ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    public static TAttribute[] FetchAllAttributes<TAttribute>(
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        ParameterInfo info)
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
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
#if NET47_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public static TAttribute FetchSingleAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        ParameterInfo info)
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
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 new KeyValuePair<Object, Object?>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, Object?>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        TAttribute? attribute = info.GetCustomAttribute<TAttribute>();
        if (attribute is null)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 new KeyValuePair<Object, Object?>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, Object?>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        return attribute;
    }
}
#endif