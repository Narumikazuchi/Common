using System;
using System.Runtime.CompilerServices;

namespace Narumikazuchi
{
    /// <summary>
    /// Contains helpers for throwing exceptions.
    /// </summary>
    public static class ExceptionHelpers
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the source parameter is null.
        /// </summary>
        /// <param name="source">The source to check against <see langword="null"/>.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException" />
        public static void ThrowIfNull(Object? source, [CallerArgumentExpression("source")] String? paramName = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the source parameter is null or completely made of whitespace.
        /// </summary>
        /// <param name="source">The source to check against <see langword="null"/>.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException" />
        public static void ThrowIfNullOrEmpty(String? source, [CallerArgumentExpression("source")] String? paramName = null)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
