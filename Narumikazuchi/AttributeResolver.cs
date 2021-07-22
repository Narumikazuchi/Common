using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Narumikazuchi
{
    /// <summary>
    /// Checks if a custom attribute is defined or fetches the custom attributes applied to a type, method, parameter etc.
    /// </summary>
    public static class AttributeResolver
    {
        #region Assemblies

        /// <summary>
        /// Checks if the specified <see cref="Assembly"/> has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="assembly">The assembly to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean AssemblyHasAttribute<TAttribute>([DisallowNull] Assembly assembly) where TAttribute : Attribute => 
            assembly is null ? throw new ArgumentNullException(nameof(assembly)) : Attribute.IsDefined(assembly, typeof(TAttribute));

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchAssemblyAttributes<TAttribute>([DisallowNull] Assembly assembly) where TAttribute : Attribute =>
            assembly is null ? throw new ArgumentNullException(nameof(assembly)) : assembly.GetCustomAttributes<TAttribute>();

        #endregion

        #region Types

        /// <summary>
        /// Checks if the specified <see cref="Type"/> has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean TypeHasAttribute<TAttribute>([DisallowNull] Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            TypeInfo? info = type.GetTypeInfo();
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchTypeAttributes<TAttribute>([DisallowNull] Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            TypeInfo? info = type.GetTypeInfo();
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Checks if the specified <see cref="Type"/> constructor has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="parameters">The parameters of the constructor to check the correct constructor.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean ConstructorHasAttribute<TAttribute>([DisallowNull] Type type, params Type[] parameters) where TAttribute : Attribute => ConstructorHasAttribute<TAttribute>(type, BindingFlags.Public | BindingFlags.Instance, parameters);
        /// <summary>
        /// Checks if the specified <see cref="Type"/> constructor has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="flags">The bindings of the constructor to check.</param>
        /// <param name="parameters">The parameters of the constructor to check the correct constructor.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean ConstructorHasAttribute<TAttribute>([DisallowNull] Type type, BindingFlags flags, params Type[] parameters) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ConstructorInfo? info = type.GetConstructor(flags, null, parameters, null);
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to retrieve the attributes from.</param>
        /// <param name="parameters">The parameters of the constructor to check the correct constructor.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchConstructorAttributes<TAttribute>([DisallowNull] Type type, params Type[] parameters) where TAttribute : Attribute => 
            FetchConstructorAttributes<TAttribute>(type, BindingFlags.Public | BindingFlags.Instance, parameters);
        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to retrieve the constructor attributes from.</param>
        /// <param name="flags">The bindings of the constructor to check.</param>
        /// <param name="parameters">The parameters of the constructor to check the correct constructor.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchConstructorAttributes<TAttribute>([DisallowNull] Type type, BindingFlags flags, params Type[] parameters) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ConstructorInfo? info = type.GetConstructor(flags, null, parameters, null);
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the specified method has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type the method belongs to.</param>
        /// <param name="methodName">The name of the method to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean MethodHasAttribute<TAttribute>([DisallowNull] Type type, [DisallowNull] String methodName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            MethodInfo? info = type.GetMethod(methodName);
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified method.
        /// </summary>
        /// <param name="type">The type the method belongs to.</param>
        /// <param name="methodName">The name of the method to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchMethodAttributes<TAttribute>([DisallowNull] Type type, [DisallowNull] String methodName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            MethodInfo? info = type.GetMethod(methodName);
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Events

        /// <summary>
        /// Checks if the specified <see langword="event"/> has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type the <see langword="event"/> belongs to.</param>
        /// <param name="eventName">The name of the <see langword="event"/> to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean EventHasAttribute<TAttribute>([DisallowNull] Type type, [DisallowNull] String eventName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (eventName is null)
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            EventInfo? info = type.GetEvent(eventName);
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified <see langword="event"/>.
        /// </summary>
        /// <param name="type">The type the <see langword="event"/> belongs to.</param>
        /// <param name="eventName">The name of the <see langword="event"/> to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchEventAttributes<TAttribute>([DisallowNull] Type type, [DisallowNull] String eventName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (eventName is null)
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            EventInfo? info = type.GetEvent(eventName);
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Checks if the specified property has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type the property belongs to.</param>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean PropertyHasAttribute<TAttribute>([DisallowNull] Type type, [DisallowNull] String propertyName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyInfo? info = type.GetProperty(propertyName);
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified property.
        /// </summary>
        /// <param name="type">The type the property belongs to.</param>
        /// <param name="propertyName">The name of the property to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchPropertyAttributes<TAttribute>([DisallowNull] Type type, [DisallowNull] String propertyName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyInfo? info = type.GetProperty(propertyName);
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Fields

        /// <summary>
        /// Checks if the specified field has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type the field belongs to.</param>
        /// <param name="fieldName">The name of the field to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean FieldHasAttribute<TAttribute>([DisallowNull] Type type, [DisallowNull] String fieldName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (fieldName is null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            FieldInfo? info = type.GetField(fieldName);
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified field.
        /// </summary>
        /// <param name="type">The type the field belongs to.</param>
        /// <param name="fieldName">The name of the field to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchFieldAttributes<TAttribute>([DisallowNull] Type type, [DisallowNull] String fieldName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (fieldName is null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            FieldInfo? info = type.GetField(fieldName);
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        #endregion

        #region Parameters

        private static ParameterInfo? GetParameterInfoInternal(Type type, String methodName, String parameterName)
        {
            MethodInfo? method = type.GetMethod(methodName);
            return method?.GetParameters().FirstOrDefault(p => p.Name == parameterName);
        }

        /// <summary>
        /// Checks if the specified method parameter has at least one <typeparamref name="TAttribute"/> defined.
        /// </summary>
        /// <param name="type">The type the method belongs to.</param>
        /// <param name="methodName">The method name the parameter belongs to.</param>
        /// <param name="parameterName">The parameter name to check.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static Boolean ParameterHasAttribute<TAttribute>([DisallowNull] Type type, [DisallowNull] String methodName, [DisallowNull] String parameterName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }
            if (parameterName is null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            ParameterInfo? info = GetParameterInfoInternal(type, methodName, parameterName);
            return info is not null && Attribute.IsDefined(info, typeof(TAttribute));
        }

        /// <summary>
        /// Fetches all attributes of type <typeparamref name="TAttribute"/> from the specified method parameter.
        /// </summary>
        /// <param name="type">The type the method belongs to.</param>
        /// <param name="methodName">The method name the parameter belongs to.</param>
        /// <param name="parameterName">The parameter name to retrieve the attributes from.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<TAttribute> FetchParameterAttributes<TAttribute>([DisallowNull] Type type, [DisallowNull] String methodName, [DisallowNull] String parameterName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }
            if (parameterName is null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            ParameterInfo? info = GetParameterInfoInternal(type, methodName, parameterName);
            return info is null ? Array.Empty<TAttribute>() : info.GetCustomAttributes<TAttribute>();
        }

        /// <summary>
        /// Fetches all <see cref="ParameterInfo"/> from the specified method where an <typeparamref name="TAttribute"/> is defined.
        /// </summary>
        /// <param name="type">The type the method belongs to.</param>
        /// <param name="methodName">The method name the parameter belongs to.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IEnumerable<ParameterInfo> FetchParametersWithAttribute<TAttribute>([DisallowNull] Type type, [DisallowNull] String methodName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            MethodInfo? method = type.GetMethod(methodName);
            return method is null ? Array.Empty<ParameterInfo>() : method.GetParameters().Where(p => p.IsDefined(typeof(TAttribute), true));
        }

        /// <summary>
        /// Fetches all <see cref="ParameterInfo"/> from the specified method where an <typeparamref name="TAttribute"/> is defined and couples them together.
        /// </summary>
        /// <param name="type">The type the method belongs to.</param>
        /// <param name="methodName">The method name the parameter belongs to.</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        public static IReadOnlyDictionary<ParameterInfo, IEnumerable<TAttribute>> FetchParametersAndAttributes<TAttribute>([DisallowNull] Type type, [DisallowNull] String methodName) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            MethodInfo? method = type.GetMethod(methodName);
            return method is null
                ? new Dictionary<ParameterInfo, IEnumerable<TAttribute>>()
                : method.GetParameters()
                    .Where(p => p.IsDefined(typeof(TAttribute), true))
                    .Select(p => new KeyValuePair<ParameterInfo, IEnumerable<TAttribute>>(p, p.GetCustomAttributes<TAttribute>()))
                    .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        #endregion
    }
}
