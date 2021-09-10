using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Narumikazuchi
{
    /// <summary>
    /// Contains extensions for the System namespace.
    /// </summary>
    public static class System_Extensions
    {
        /// <summary>
        /// Returns the comparable clamped between the specified min and max value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lowBound">Low-bound for the clamping</param>
        /// <param name="highBound">High-bound for the clamping</param>
        /// <exception cref="ArgumentNullException"/>
        [Pure]
        [return: NotNull]
        public static T Clamp<T>(this T value, 
                                 [DisallowNull] T lowBound,
                                 [DisallowNull] T highBound) 
            where T : IComparable<T>
        {
            if (lowBound is null)
            {
                throw new ArgumentNullException(nameof(lowBound));
            }
            if (highBound is null)
            {
                throw new ArgumentNullException(nameof(highBound));
            }

            if (value.CompareTo(lowBound) < 0)
            {
                return lowBound;
            }
            else if (value.CompareTo(highBound) > 0)
            {
                return highBound;
            }
            return value;
        }

        /// <summary>
        /// Creates a deep copy of this object.
        /// </summary>
        /// <returns>A deep copy of this object</returns>
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
        [return: NotNull]
        public static T DeepCopy<T>(this T original)
        {
            Object result = FormatterServices.GetSafeUninitializedObject(typeof(T));
            foreach (FieldInfo? field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field is null)
                {
                    continue;
                }
                if (field.FieldType.IsPrimitive ||
                    field.FieldType.IsValueType ||
                    field.FieldType.IsEnum ||
                    field.FieldType.Equals(typeof(String)))
                {
                    field.SetValue(result, 
                                   field.GetValue(original));
                }
                else if (field.FieldType
                              .IsArray)
                {
                    Array? source = (Array?)field.GetValue(original);
                    Type? element = field.FieldType
                                         .GetElementType();
                    if (source is not null &&
                        element is not null)
                    {
                        Array destination = Array.CreateInstance(element, 
                                                                 source.Length);
                        Array.Copy(source, 
                                   destination, 
                                   source.Length);
                        field.SetValue(result, 
                                       destination);
                    }
                }
                else
                {
                    Object? value = field.GetValue(original);
                    field.SetValue(result, 
                                   value is not null 
                                        ? DeepCopy(value) 
                                        : null);
                }
            }

            return (T)result;
        }

        private static Object DeepCopy(Object original)
        {
            Object? result = FormatterServices.GetSafeUninitializedObject(original.GetType());
            foreach (FieldInfo? field in original.GetType()
                                                 .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field is null)
                {
                    continue;
                }
                if (field.FieldType.IsPrimitive ||
                    field.FieldType.IsValueType ||
                    field.FieldType.IsEnum ||
                    field.FieldType.Equals(typeof(String)))
                {
                    field.SetValue(result, 
                                   field.GetValue(original));
                }
                else if (field.FieldType
                              .IsArray)
                {
                    Array? source = (Array?)field.GetValue(original);
                    Type? element = field.FieldType
                                         .GetElementType();
                    if (source is not null &&
                        element is not null)
                    {
                        Array destination = Array.CreateInstance(element, 
                                                                 source.Length);
                        Array.Copy(source, 
                                   destination, 
                                   source.Length);
                        field.SetValue(result, 
                                       destination);
                    }
                }
                else
                {
                    Object? value = field.GetValue(original);
                    field.SetValue(result, 
                                   value is not null 
                                        ? DeepCopy(value) 
                                        : null);
                }
            }

            return result;
        }

        /// <summary>
        /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
        /// </summary>
        /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
        [Pure]
        [return: NotNull]
        public static String SanitizeForFilename(this String raw)
        {
            IEnumerable<Char> invalid = Path.GetInvalidFileNameChars()
                                            .Union(Path.GetInvalidPathChars());
            String result = raw;
            foreach (Char c in invalid)
            {
                result = raw.Replace(c.ToString(), 
                                     "");
            }
            return result;
        }

        /// <summary>
        /// Determines whether this type is a <see cref="Singleton"/>.
        /// </summary>
        /// <returns><see langword="true"/> if this type is a <see cref="Singleton"/>; else, <see langword="false"/></returns>
        [Pure]
        public static Boolean IsSingleton(this Type type) => 
            type.IsAssignableTo(typeof(Singleton));
    }
}
