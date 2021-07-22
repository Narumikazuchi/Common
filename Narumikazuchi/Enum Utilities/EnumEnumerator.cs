using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Narumikazuchi
{
    /// <summary>
    /// Enumerates through enum values.
    /// </summary>
    public static class EnumEnumerator
    {
        /// <summary>
        /// Enumerates through all values defined for the <typeparamref name="TEnum"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all values defined for the <typeparamref name="TEnum"/>.</returns>
        [Pure]
        public static IEnumerable<TEnum> EnumerateValues<TEnum>() where TEnum : Enum =>
            __EnumChache<TEnum>.DefinedValues;

        /// <summary>
        /// Enumerates the flags, which are set in the input value.
        /// </summary>
        /// <param name="enumValue">The value to enumerate the flags of.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all flags that are set in the input value.</returns>
        [Pure]
        public static IEnumerable<TEnum> EnumerateFlags<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            if (!AttributeResolver.TypeHasAttribute<FlagsAttribute>(typeof(TEnum)))
            {
                yield break;
            }
#pragma warning disable
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
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
