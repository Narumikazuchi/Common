using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Narumikazuchi
{
    /// <summary>
    /// Extensions class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if the specified enum value is valid/defined in the enum.
        /// </summary>
        /// <param name="enumValue">The value to check.</param>
        /// <returns><see langword="true"/> if the specified enum value is defined else <see langword="false"/></returns>
        [Pure]
        public static Boolean IsDefined<TEnum>(this TEnum enumValue)
            where TEnum : Enum =>
                __EnumChache<TEnum>.DefinedValues.Contains(enumValue);

        /// <summary>
        /// Enumerates the flags, which are set in the input value.
        /// </summary>
        /// <param name="enumValue">The value to enumerate the flags of.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all flags that are set in the input value.</returns>
        [Pure]
        [return: NotNull]
        public static IEnumerable<TEnum> EnumerateFlags<TEnum>(this TEnum enumValue)
            where TEnum : Enum
        {
            if (!AttributeResolver.HasAttribute<FlagsAttribute>(typeof(TEnum)))
            {
                yield break;
            }
#pragma warning disable
            foreach (TEnum value in Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>())
            {
                if (enumValue.HasFlag(value))
                {
                    yield return value;
                }
            }
#pragma warning restore
        }
    }
}
