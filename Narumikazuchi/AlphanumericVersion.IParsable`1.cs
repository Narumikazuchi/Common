namespace Narumikazuchi;

#if NET7_0_OR_GREATER
public partial struct AlphanumericVersion : IParsable<AlphanumericVersion>
{
    /// <inheritdoc/>
    static public AlphanumericVersion Parse([DisallowNull] String source,
                                            [AllowNull] IFormatProvider? provider)
    {
        ArgumentNullException.ThrowIfNull(source);

        String[] segments = source.Split(separator: '.',
                                         options: StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length is < 1
                            or > 4)
        {
            ArgumentException exception = new(message: SEGMENT_COUNT_OUT_OF_BOUNDS,
                                              paramName: nameof(source));
            exception.Data.Add(key: "Number of Segments",
                               value: segments.Length);
            throw exception;
        }

        for (Int32 index = 0;
             index < segments.Length;
             index++)
        {
            if (String.IsNullOrWhiteSpace(segments[index]) ||
                !s_Regex.IsMatch(input: segments[index]))
            {
                FormatException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC);
                exception.Data.Add(key: "Value",
                                   value: segments[index]);
                throw exception;
            }
        }

        if (segments.Length == 1)
        {
            return new(major: segments[0]);
        }
        if (segments.Length == 2)
        {
            return new(major: segments[0],
                       minor: segments[1]);
        }
        if (segments.Length == 3)
        {
            return new(major: segments[0],
                       minor: segments[1],
                       build: segments[2]);
        }
        return new(major: segments[0],
                   minor: segments[1],
                   build: segments[2],
                   revision: segments[3]);
    }

    /// <inheritdoc/>
    static public Boolean TryParse([NotNullWhen(true)] String? source,
                                   [AllowNull] IFormatProvider? provider,
                                   out AlphanumericVersion result)
    {
        if (String.IsNullOrWhiteSpace(source))
        {
            result = default;
            return false;
        }

        String[] segments = source!.Split(separator: '.',
                                          options: StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length is < 1
                            or > 4)
        {
            result = default;
            return false;
        }

        for (Int32 index = 0;
             index < segments.Length;
             index++)
        {
            if (!s_Regex.IsMatch(input: segments[index]))
            {
                result = default;
                return false;
            }
            if (String.IsNullOrWhiteSpace(segments[index]))
            {
                result = default;
                return false;
            }
        }

        if (segments.Length == 1)
        {
            result = new(major: segments[0]);
            return true;
        }
        if (segments.Length == 2)
        {
            result = new(major: segments[0],
                         minor: segments[1]);
            return true;
        }
        if (segments.Length == 3)
        {
            result = new(major: segments[0],
                         minor: segments[1],
                         build: segments[2]);
            return true;
        }
        result = new(major: segments[0],
                     minor: segments[1],
                     build: segments[2],
                     revision: segments[3]);
        return true;
    }

    /// <summary>
    /// Parses the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    static public AlphanumericVersion Parse([DisallowNull] NotNull<String> source)
    {
        return Parse(source: source,
                     provider: null);
    }

    /// <summary>
    /// Tries to parse the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    /// <returns><see langword="true"/> if the parsing succeeded; otherwise, <see langword="false"/></returns>
    static public Boolean TryParse([DisallowNull] NotNullOrEmpty<String> source,
                                   out AlphanumericVersion result)
    {
        return TryParse(source: source,
                        provider: null,
                        result: out result);
    }
}
#else
public partial struct AlphanumericVersion
{
    /// <inheritdoc/>
    public static AlphanumericVersion Parse(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNullOrEmpty<String> source)
    {
        String toParse = source;
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        String[] segments = toParse.Split(separator: '.',
                                          options: StringSplitOptions.RemoveEmptyEntries);
#else
        String[] segments = toParse.Split(separator: new Char[] { '.' },
                                          options: StringSplitOptions.RemoveEmptyEntries);
#endif
        if (segments.Length is < 1
                            or > 4)
        {
            ArgumentException exception = new(message: SEGMENT_COUNT_OUT_OF_BOUNDS,
                                              paramName: nameof(source));
            exception.Data.Add(key: "Number of Segments",
                               value: segments.Length);
            throw exception;
        }

        for (Int32 index = 0;
             index < segments.Length;
             index++)
        {
            if (String.IsNullOrWhiteSpace(segments[index]) ||
                !s_Regex.IsMatch(input: segments[index]))
            {
                FormatException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC);
                exception.Data.Add(key: "Value",
                                   value: segments[index]);
                throw exception;
            }
        }

        if (segments.Length == 1)
        {
            return new(segments[0]);
        }
        if (segments.Length == 2)
        {
            return new(major: segments[0],
                       minor: segments[1]);
        }
        if (segments.Length == 3)
        {
            return new(major: segments[0],
                       minor: segments[1],
                       build: segments[2]);
        }
        return new(major: segments[0],
                   minor: segments[1],
                   build: segments[2],
                   revision: segments[3]);
    }

    /// <inheritdoc/>
    public static Boolean TryParse(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(true)]
#endif
        MaybeNullOrEmpty<String> source,
        out AlphanumericVersion result)
    {
        if (source.IsNull ||
            source.IsEmpty)
        {
            result = default;
            return false;
        }

        String toParse = source!;
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        String[] segments = toParse.Split(separator: '.',
                                          options: StringSplitOptions.RemoveEmptyEntries);
#else
        String[] segments = toParse.Split(separator: new Char[] { '.' },
                                          options: StringSplitOptions.RemoveEmptyEntries);
#endif

        if (segments.Length is < 1
                            or > 4)
        {
            result = default;
            return false;
        }

        for (Int32 index = 0;
             index < segments.Length;
             index++)
        {
            if (!s_Regex.IsMatch(input: segments[index]))
            {
                result = default;
                return false;
            }
            if (String.IsNullOrWhiteSpace(segments[index]))
            {
                result = default;
                return false;
            }
        }

        if (segments.Length == 1)
        {
            result = new(major: segments[0]);
            return true;
        }
        if (segments.Length == 2)
        {
            result = new(major: segments[0],
                         minor: segments[1]);
            return true;
        }
        if (segments.Length == 3)
        {
            result = new(major: segments[0],
                         minor: segments[1],
                         build: segments[2]);
            return true;
        }
        result = new(major: segments[0],
                     minor: segments[1],
                     build: segments[2],
                     revision: segments[3]);
        return true;
    }
}
#endif