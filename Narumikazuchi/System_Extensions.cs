using System;
using System.Collections.Generic;
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
        #region Clamp

        /// <summary>
        /// Returns the comparable clamped between the specified min and max value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lowBound">Low-bound for the clamping</param>
        /// <param name="highBound">High-bound for the clamping</param>
        public static T Clamp<T>(this T value, T lowBound, T highBound) where T : IComparable<T>
        {
            T result = value;
            if (result.CompareTo(lowBound) < 0)
            {
                result = lowBound;
            }
            else if (result.CompareTo(highBound) > 0)
            {
                result = highBound;
            }
            return result;
        }

        #endregion

        #region Deep Copy

        /// <summary>
        /// Creates a deep copy of this object.
        /// </summary>
        /// <returns>A deep copy of this object</returns>
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
                    field.SetValue(result, field.GetValue(original));
                }
                else if (field.FieldType.IsArray)
                {
                    Array? source = (Array?)field.GetValue(original);
                    Type? element = field.FieldType.GetElementType();
                    if (source is not null &&
                        element is not null)
                    {
                        Array destination = Array.CreateInstance(element, source.Length);
                        Array.Copy(source, destination, source.Length);
                        field.SetValue(result, destination);
                    }
                }
                else
                {
                    Object? value = field.GetValue(original);
                    field.SetValue(result, value is not null ? DeepCopy(value) : null);
                }
            }
            foreach (PropertyInfo? property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (property is null ||
                    !property.CanRead ||
                    !property.CanWrite ||
                    property.GetIndexParameters().Length > 0)
                {
                    continue;
                }
                if (property.PropertyType.IsPrimitive ||
                    property.PropertyType.IsValueType ||
                    property.PropertyType.IsEnum ||
                    property.PropertyType.Equals(typeof(String)))
                {
                    property.SetValue(result, property.GetValue(original));
                }
                else if (property.PropertyType.IsArray)
                {
                    Array? source = (Array?)property.GetValue(original);
                    Type? element = property.PropertyType.GetElementType();
                    if (source is not null &&
                        element is not null)
                    {
                        Array destination = Array.CreateInstance(element, source.Length);
                        Array.Copy(source, destination, source.Length);
                        property.SetValue(result, destination);
                    }
                }
                else
                {
                    Object? value = property.GetValue(original);
                    property.SetValue(result, value is not null ? DeepCopy(value) : null);
                }
            }

            return (T)result;
        }

        private static Object DeepCopy(Object original)
        {
            Object? result = FormatterServices.GetSafeUninitializedObject(original.GetType());
            foreach (FieldInfo? field in original.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
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
                    field.SetValue(result, field.GetValue(original));
                }
                else if (field.FieldType.IsArray)
                {
                    Array? source = (Array?)field.GetValue(original);
                    Type? element = field.FieldType.GetElementType();
                    if (source is not null &&
                        element is not null)
                    {
                        Array destination = Array.CreateInstance(element, source.Length);
                        Array.Copy(source, destination, source.Length);
                        field.SetValue(result, destination);
                    }
                }
                else
                {
                    Object? value = field.GetValue(original);
                    field.SetValue(result, value is not null ? DeepCopy(value) : null);
                }
            }
            foreach (PropertyInfo? property in original.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (property is null ||
                    !property.CanRead ||
                    !property.CanWrite ||
                    property.GetIndexParameters().Length > 0)
                {
                    continue;
                }
                if (property.PropertyType.IsPrimitive ||
                    property.PropertyType.IsValueType ||
                    property.PropertyType.IsEnum ||
                    property.PropertyType.Equals(typeof(String)))
                {
                    property.SetValue(result, property.GetValue(original));
                }
                else if (property.PropertyType.IsArray)
                {
                    Array? source = (Array?)property.GetValue(original);
                    Type? element = property.PropertyType.GetElementType();
                    if (source is not null &&
                        element is not null)
                    {
                        Array destination = Array.CreateInstance(element, source.Length);
                        Array.Copy(source, destination, source.Length);
                        property.SetValue(result, destination);
                    }
                }
                else
                {
                    Object? value = property.GetValue(original);
                    property.SetValue(result, value is not null ? DeepCopy(value) : null);
                }
            }

            return result;
        }

        #endregion

        #region Sanitize File String

        /// <summary>
        /// Sanitizes this <see cref="String"/> to be able to use as valid filename.
        /// </summary>
        /// <returns>Another <see cref="String"/> which represents a valid filename.</returns>
        public static String SanitizeForFilename(this String raw)
        {
            IEnumerable<Char> invalid = Path.GetInvalidFileNameChars().Union(Path.GetInvalidPathChars());
            String result = raw;
            foreach (Char c in invalid)
            {
                result = raw.Replace(c.ToString(), "");
            }
            return result;
        }

        #endregion

        #region Singleton

        /// <summary>
        /// Determines whether this type is a <see cref="Singleton"/>.
        /// </summary>
        /// <returns><see langword="true"/> if this type is a <see cref="Singleton"/>; else, <see langword="false"/></returns>
        public static Boolean IsSingleton(this Type type) => type.IsAssignableTo(typeof(Singleton));

        #endregion
    }
}
