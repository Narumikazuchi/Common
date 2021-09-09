using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Narumikazuchi
{
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

        private const String MULTIPLE_INSTANCES_ARE_ALLOWED = "This method is supposed to only work with Attributes that disallow multiple instances.";
        private const String ATTRIBUTE_NOT_DEFINED_FOR_TARGET = "An Attribute of type {0} has not been defined for the specified {1}.";
    }

    // Assemblies
    partial class AttributeResolver
    {
        /// <summary>
        /// Checks if the specified <see cref="Assembly"/> has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean HasAttribute<TAttribute>([DisallowNull] Assembly assembly) 
            where TAttribute : Attribute =>
                assembly is null 
                    ? throw new ArgumentNullException(nameof(assembly)) 
                    : Attribute.IsDefined(assembly, 
                                          typeof(TAttribute));

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchAllAttributes<TAttribute>([DisallowNull] Assembly assembly) 
            where TAttribute : Attribute =>
                assembly is null 
                    ? throw new ArgumentNullException(nameof(assembly)) 
                    : assembly.GetCustomAttributes<TAttribute>();

        /// <summary>
        /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to retrieve the attribute from.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        [Pure]
        public static TAttribute FetchOnlyAllowedAttribute<TAttribute>([DisallowNull] Assembly assembly)
            where TAttribute : Attribute
        {
            if (MultipleAllowed<TAttribute>())
            {
                throw new InvalidOperationException(MULTIPLE_INSTANCES_ARE_ALLOWED);
            }
            TAttribute? attribute = assembly.GetCustomAttribute<TAttribute>();
            return attribute is not null
                        ? attribute
                        : throw new InvalidOperationException(String.Format(ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                                                            typeof(TAttribute).Name,
                                                                            nameof(Assembly)));
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
        [Pure]
        public static Boolean HasAttribute<TAttribute>([DisallowNull] MemberInfo info)
            where TAttribute : Attribute =>
                info is null
                    ? throw new ArgumentNullException(nameof(info))
                    : Attribute.IsDefined(info,
                                          typeof(TAttribute));

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchAllAttributes<TAttribute>([DisallowNull] MemberInfo info)
            where TAttribute : Attribute =>
                info is null
                    ? throw new ArgumentNullException(nameof(info))
                    : info.GetCustomAttributes<TAttribute>();

        /// <summary>
        /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="info">The <see cref="MemberInfo"/> to retrieve the attribute from.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        [Pure]
        public static TAttribute FetchOnlyAllowedAttribute<TAttribute>([DisallowNull] MemberInfo info)
            where TAttribute : Attribute
        {
            if (MultipleAllowed<TAttribute>())
            {
                throw new InvalidOperationException(MULTIPLE_INSTANCES_ARE_ALLOWED);
            }
            TAttribute? attribute = info.GetCustomAttribute<TAttribute>();
            return attribute is not null
                        ? attribute
                        : throw new InvalidOperationException(String.Format(ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                                                            typeof(TAttribute).Name,
                                                                            info.GetType().Name));
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
        [Pure]
        public static Boolean HasAttribute<TAttribute>([DisallowNull] ParameterInfo info)
            where TAttribute : Attribute =>
                info is null
                    ? throw new ArgumentNullException(nameof(info))
                    : Attribute.IsDefined(info,
                                          typeof(TAttribute));

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchAllAttributes<TAttribute>([DisallowNull] ParameterInfo info)
            where TAttribute : Attribute =>
                info is null
                    ? throw new ArgumentNullException(nameof(info))
                    : info.GetCustomAttributes<TAttribute>();

        /// <summary>
        /// Fetches the only allowed attribute of type <typeparamref name="TAttribute"/> from the specified <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="info">The <see cref="ParameterInfo"/> to retrieve the attribute from.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        [Pure]
        public static TAttribute FetchOnlyAllowedAttribute<TAttribute>([DisallowNull] ParameterInfo info)
            where TAttribute : Attribute
        {
            if (MultipleAllowed<TAttribute>())
            {
                throw new InvalidOperationException(MULTIPLE_INSTANCES_ARE_ALLOWED);
            }
            TAttribute? attribute = info.GetCustomAttribute<TAttribute>();
            return attribute is not null
                        ? attribute
                        : throw new InvalidOperationException(String.Format(ATTRIBUTE_NOT_DEFINED_FOR_TARGET,
                                                                            typeof(TAttribute).Name,
                                                                            nameof(ParameterInfo)));
        }
    }
}
