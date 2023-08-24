using Narumikazuchi.Generated;
using System.Diagnostics.CodeAnalysis;

namespace Narumikazuchi;

/// <summary>
/// Represents an immutable alphanumeric version number.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly partial struct AlphanumericVersion
{
    /// <summary>
    /// Implicit conversion from <see cref="Version"/> class.
    /// </summary>
    static public implicit operator AlphanumericVersion([AllowNull] Version? source)
    {
        if (source is null)
        {
            return new();
        }

        if (source.Revision > -1)
        {
            return new(major: source.Major.ToString(),
                       minor: source.Minor.ToString(),
                       build: source.Build.ToString(),
                       revision: source.Revision.ToString());
        }
        else if (source.Build > -1)
        {
            return new(major: source.Major.ToString(),
                       minor: source.Minor.ToString(),
                       build: source.Build.ToString());
        }

        return new(major: source.Major.ToString(),
                   minor: source.Minor.ToString());
    }

    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion([DisallowNull] StringOrUnsignedInt major,
                               [AllowNull] StringOrUnsignedInt minor = default,
                               [AllowNull] StringOrUnsignedInt build = default,
                               [AllowNull] StringOrUnsignedInt revision = default)
    {
        if (!major.HasValue)
        {
            throw new ArgumentNullException(nameof(major));
        }

        String majorString = major.ToString();
        if (!s_Regex.IsMatch(majorString))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data.Add(key: "Value",
                               value: majorString);
            throw exception;
        }

        if (minor.HasValue)
        {
            String minorString = minor.ToString();
            if (!s_Regex.IsMatch(minorString))
            {
                ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                                  paramName: nameof(minor));
                exception.Data.Add(key: "Value",
                                   value: minorString);
                throw exception;
            }
        }

        if (build.HasValue)
        {
            String buildString = build.ToString();
            if (!s_Regex.IsMatch(buildString))
            {
                ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                                  paramName: nameof(build));
                exception.Data.Add(key: "Value",
                                   value: buildString);
                throw exception;
            }
        }

        if (revision.HasValue)
        {
            String revisionString = revision.ToString();
            if (!s_Regex.IsMatch(revisionString))
            {
                ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                                  paramName: nameof(revision));
                exception.Data.Add(key: "Value",
                                   value: revisionString);
                throw exception;
            }
        }

        m_Major = majorString;
        m_Minor = minor.HasValue ? minor.ToString() : null;
        m_Build = build.HasValue ? build.ToString() : null;
        m_Revision = revision.HasValue ? revision.ToString() : null;
    }

    /// <inheritdoc/>
    public override Boolean Equals([NotNullWhen(true)] Object? obj)
    {
        return obj is AlphanumericVersion other &&
               this.CompareTo(other) == 0;
    }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    {
        if (m_Major is null)
        {
            return Int32.MaxValue;
        }

        Int32 result = m_Major.GetHashCode();

        if (m_Minor is null)
        {
            return result;
        }

        result ^= m_Minor.GetHashCode();

        if (m_Build is null)
        {
            return result;
        }

        result ^= m_Build.GetHashCode();

        if (m_Revision is null)
        {
            return result;
        }

        result ^= m_Revision.GetHashCode();

        return result;
    }

    /// <summary>
    /// Transforms the <see cref="AlphanumericVersion"/> into a <see cref="String"/> according to the default format '#.#.#.#'.
    /// </summary>
    [return: NotNull]
    public override String ToString()
    {
        return this.ToString(null);
    }

    /// <summary>
    /// Formats the output to be in a user specified format.
    /// </summary>
    /// <param name="format">The format in which to display the version. See the 'remarks'-section for more information.</param>
    /// <remarks>
    /// Remarks: When defining the format of the output use the following rules:
    /// <list type="bullet">
    ///     <item>
    ///         Use '#' as placeholder for any of the segments. The method will replace the first '#' with the major version,
    ///         the second with the minor version, the third with the build version and the fourth with the revision.
    ///         Every instance of that symbol afterwards will be ignored.
    ///     </item>
    ///     <item>
    ///         Every other character that is present in front, in between or after the '#' characters will be used as separators
    ///         in the resulting output.
    ///     </item>
    /// </list>
    /// Example: A format of '#.#-#-#' will result in the output 'MAJOR.MINOR-BUILD-REVISION', while a format of 'a#bc#bc#d#e'
    /// will result in the output 'aMAJORbcMINORbcBUILDdREVISIONe', replacing each of the version segments with their respective values.
    /// </remarks>
    /// <returns>This instance formatted as a <see cref="String"/></returns>
    [return: NotNull]
    public String ToString([AllowNull] String? format)
    {
        if (String.IsNullOrWhiteSpace(format))
        {
            format = "#.#.#.#";
        }

        Int32 count = format.Count(x => x == '#')
                            .Clamp(1, 4);
        ReadOnlySpan<Char> working = format;
        StringBuilder builder = new();

        Int32 index;

        for (Int32 counter = 0;
             counter < count;
             counter++)
        {
            String current = counter switch
            {
                0 => this.Major,
                1 => this.Minor,
                2 => this.Build,
                3 => this.Revision,
                _ => "0"
            };
            if (current == "-1")
            {
                working = "";
                break;
            }

            index = working.IndexOf('#');
            if (index > 0)
            {
                builder.Append(working[0..index]);
            }

            if (String.IsNullOrWhiteSpace(current))
            {
                builder.Append('0');
            }
            else
            {
                builder.Append(current);
            }

            working = working[++index..];
        }

        if (working.Length > 0)
        {
            builder.Append(working);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the major version component of this <see cref="AlphanumericVersion"/>.
    /// </summary>
    [NotNull]
    public String Major
    {
        get
        {
            return m_Major;
        }
    }

    /// <summary>
    /// Gets the minor version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no minor version component is specified.
    /// </summary>
    [NotNull]
    public String Minor
    {
        get
        {
            if (m_Minor is null)
            {
                return "-1";
            }

            return m_Minor;
        }
    }

    /// <summary>
    /// Gets the build version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no build version component is specified.
    /// </summary>
    [NotNull]
    public String Build
    {
        get
        {
            if (m_Build is null)
            {
                return "-1";
            }

            return m_Build;
        }
    }

    /// <summary>
    /// Gets the revision version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no revision  version component is specified.
    /// </summary>
    [NotNull]
    public String Revision
    {
        get
        {
            if (m_Revision is null)
            {
                return "-1";
            }

            return m_Revision;
        }
    }
}