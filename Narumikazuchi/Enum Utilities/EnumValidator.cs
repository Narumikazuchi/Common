using System;
using System.Diagnostics.Contracts;

namespace Narumikazuchi
{
    /// <summary>
    /// Checks an enum value for validity.
    /// </summary>
    public static class EnumValidator
    {
        /// <summary>
        /// Checks if the specified enum value is valid/defined in the enum.
        /// </summary>
        /// <param name="enumValue">The value to check.</param>
        /// <returns><see langword="true"/> if the specified enum value is defined else <see langword="false"/></returns>
        [Pure]
        public static Boolean IsDefined<TEnum>(TEnum enumValue) where TEnum : Enum => 
            __EnumChache<TEnum>.DefinedValues.Contains(enumValue);
    }
}
