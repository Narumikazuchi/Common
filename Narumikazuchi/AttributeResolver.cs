namespace Narumikazuchi;

#if NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
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
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean HasAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<Assembly> assembly)
            where TAttribute : Attribute
    {
        return Attribute.IsDefined(element: assembly,
                                   attributeType: typeof(TAttribute));
    }
    /// <summary>
    /// Checks if the specified <see cref="MemberInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean HasAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<MemberInfo> info)
            where TAttribute : Attribute
    {
        return Attribute.IsDefined(element: info,
                                   attributeType: typeof(TAttribute));
    }
    /// <summary>
    /// Checks if the specified <see cref="ParameterInfo"/> has at least one <typeparamref name="TAttribute"/> defined.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to check.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
    static public Boolean HasAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<ParameterInfo> info)
            where TAttribute : Attribute
    {
        return Attribute.IsDefined(element: info,
                                   attributeType: typeof(TAttribute));
    }

    /// <summary>
    /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attributes from.</param>
    /// <exception cref="ArgumentNullException"/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP1_0_OR_GREATER
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    static public TAttribute[] FetchAllAttributes<TAttribute>(
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<Assembly> assembly)
            where TAttribute : Attribute
    {
        Assembly value = assembly;
        return value.GetCustomAttributes<TAttribute>()
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
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP1_0_OR_GREATER
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    static public TAttribute[] FetchAllAttributes<TAttribute>(
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<MemberInfo> info)
            where TAttribute : Attribute
    {
        MemberInfo value = info;
        return value.GetCustomAttributes<TAttribute>()
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
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP1_0_OR_GREATER
    static public ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(
#else
    static public TAttribute[] FetchAllAttributes<TAttribute>(
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<ParameterInfo> info)
            where TAttribute : Attribute
    {
        ParameterInfo value = info;
        return value.GetCustomAttributes<TAttribute>()
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
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public TAttribute FetchSingleAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<Assembly> assembly)
            where TAttribute : Attribute
    {
        if (MultipleAllowed<TAttribute>())
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }

        Assembly value = assembly;
        TAttribute? attribute = value.GetCustomAttribute<TAttribute>();
        if (attribute is null)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        return attribute;
    }
    /// <summary>
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public TAttribute FetchSingleAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<MemberInfo> info)
            where TAttribute : Attribute
    {
        if (MultipleAllowed<TAttribute>())
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }

        MemberInfo value = info;
        TAttribute? attribute = value.GetCustomAttribute<TAttribute>();
        if (attribute is null)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }
        return attribute;
    }
    /// <summary>
    /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attribute from.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
#if NET48_OR_GREATER || NET5_0_OR_GREATER
    [Pure]
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    static public TAttribute FetchSingleAttribute<TAttribute>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<ParameterInfo> info)
            where TAttribute : Attribute
    {
        if (MultipleAllowed<TAttribute>())
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_DISALLOWED,
                                 innerException: null,
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
        }

        ParameterInfo value = info;
        TAttribute? attribute = value.GetCustomAttribute<TAttribute>();
        if (attribute is null)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 ("Typename", typeof(TAttribute).FullName),
                                 ("Assembly", typeof(TAttribute).AssemblyQualifiedName));
#else
            throw new NotAllowed(message: ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                 innerException: null,
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Typename", value: typeof(TAttribute).FullName),
                                 new KeyValuePair<Object, MaybeNull<Object>>(key: "Assembly", value: typeof(TAttribute).AssemblyQualifiedName));
#endif
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
#endif