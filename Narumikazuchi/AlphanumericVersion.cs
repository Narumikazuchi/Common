namespace Narumikazuchi;

/// <summary>
/// Represents an immutable alphanumeric version number.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly partial struct AlphanumericVersion
{
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major)
    {
#if NETCOREAPP3_0_OR_GREATER
        major.ThrowIfNullOrEmpty(asArgumentException: true);
#else
        if (String.IsNullOrWhiteSpace(major))
        {
            throw new ArgumentNullException(nameof(major));
        }
#endif

        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data.Add(key: "Value",
                               value: major);
            throw exception;
        }

        m_Major = major;
        m_Minor = null;
        m_Build = null;
        m_Revision = null;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major) :
        this(major.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor)
    {
#if NETCOREAPP3_0_OR_GREATER
        major.ThrowIfNullOrEmpty(asArgumentException: true);
        minor.ThrowIfNullOrEmpty(asArgumentException: true);
#else
        if (String.IsNullOrWhiteSpace(major))
        {
            throw new ArgumentNullException(nameof(major));
        }
        if (String.IsNullOrWhiteSpace(minor))
        {
            throw new ArgumentNullException(nameof(minor));
        }
#endif

        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data.Add(key: "Value",
                               value: major);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data.Add(key: "Value",
                               value: minor);
            throw exception;
        }

        m_Major = major;
        m_Minor = minor;
        m_Build = null;
        m_Revision = null;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor) :
            this(major: major,
                 minor: minor.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor) :
            this(major: major.ToString(),
                 minor: minor.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
                               in Int64 minor) :
            this(major: major.ToString(),
                 minor: minor.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build)
    {
#if NETCOREAPP3_0_OR_GREATER
        major.ThrowIfNullOrEmpty(asArgumentException: true);
        minor.ThrowIfNullOrEmpty(asArgumentException: true);
        build.ThrowIfNullOrEmpty(asArgumentException: true);
#else
        if (String.IsNullOrWhiteSpace(major))
        {
            throw new ArgumentNullException(nameof(major));
        }
        if (String.IsNullOrWhiteSpace(minor))
        {
            throw new ArgumentNullException(nameof(minor));
        }
        if (String.IsNullOrWhiteSpace(build))
        {
            throw new ArgumentNullException(nameof(build));
        }
#endif

        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data.Add(key: "Value",
                               value: major);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data.Add(key: "Value",
                               value: minor);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: build))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(build));
            exception.Data.Add(key: "Value",
                               value: build);
            throw exception;
        }

        m_Major = major;
        m_Minor = minor;
        m_Build = build;
        m_Revision = null;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
        in Int64 build) :
            this(major: major,
                 minor: minor,
                 build: build.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build) :
            this(major: major,
                 minor: minor.ToString(),
                 build: build)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build) :
            this(major: major.ToString(),
                 minor: minor,
                 build: build)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor,
        in Int64 build) :
            this(major: major,
                 minor: minor.ToString(),
                 build: build.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
        in Int64 build) :
            this(major: major.ToString(),
                 minor: minor,
                 build: build.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
                               in Int64 minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build) :
            this(major: major.ToString(),
                 minor: minor.ToString(),
                 build: build)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
                               in Int64 minor,
                               in Int64 build) :
        this(major: major.ToString(),
             minor: minor.ToString(),
             build: build.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision)
    {
#if NETCOREAPP3_0_OR_GREATER
        major.ThrowIfNullOrEmpty(asArgumentException: true);
        minor.ThrowIfNullOrEmpty(asArgumentException: true);
        build.ThrowIfNullOrEmpty(asArgumentException: true);
        revision.ThrowIfNullOrEmpty(asArgumentException: true);
#else
        if (String.IsNullOrWhiteSpace(major))
        {
            throw new ArgumentNullException(nameof(major));
        }
        if (String.IsNullOrWhiteSpace(minor))
        {
            throw new ArgumentNullException(nameof(minor));
        }
        if (String.IsNullOrWhiteSpace(build))
        {
            throw new ArgumentNullException(nameof(build));
        }
        if (String.IsNullOrWhiteSpace(revision))
        {
            throw new ArgumentNullException(nameof(revision));
        }
#endif

        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data.Add(key: "Value",
                               value: major);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data.Add(key: "Value",
                               value: minor);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: build))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(build));
            exception.Data.Add(key: "Value",
                               value: build);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: revision))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(revision));
            exception.Data.Add(key: "Value",
                               value: revision);
            throw exception;
        }

        m_Major = major;
        m_Minor = minor;
        m_Build = build;
        m_Revision = revision;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
        in Int64 revision) :
            this(major: major,
                 minor: minor,
                 build: build,
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
        in Int64 build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
         String revision) :
            this(major: major,
                 minor: minor,
                 build: build.ToString(),
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision) :
            this(major: major,
                 minor: minor.ToString(),
                 build: build,
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision) :
            this(major: major.ToString(),
                 minor: minor,
                 build: build,
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
        in Int64 build,
        in Int64 revision) :
            this(major: major,
                 minor: minor,
                 build: build.ToString(),
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
        in Int64 revision) :
            this(major: major,
                 minor: minor.ToString(),
                 build: build,
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
        in Int64 revision) :
            this(major: major.ToString(),
                 minor: minor,
                 build: build,
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor,
        in Int64 build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision) :
            this(major: major,
                 minor: minor.ToString(),
                 build: build.ToString(),
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
        in Int64 build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision) :
            this(major: major.ToString(),
                 minor: minor,
                 build: build.ToString(),
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
        in Int64 minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision) :
            this(major: major.ToString(),
                 minor: minor.ToString(),
                 build: build,
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String major,
        in Int64 minor,
        in Int64 build,
        in Int64 revision) :
            this(major: major,
                 minor: minor.ToString(),
                 build: build.ToString(),
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String minor,
        in Int64 build,
        in Int64 revision) :
            this(major: major.ToString(),
                 minor: minor,
                 build: build.ToString(),
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
        in Int64 minor,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String build,
        in Int64 revision) :
            this(major: major.ToString(),
                 minor: minor.ToString(),
                 build: build,
                 revision: revision.ToString())
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
        in Int64 minor,
        in Int64 build,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        String revision) :
            this(major: major.ToString(),
                 minor: minor.ToString(),
                 build: build.ToString(),
                 revision: revision)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion(in Int64 major,
                               in Int64 minor,
                               in Int64 build,
                               in Int64 revision) :
        this(major: major.ToString(),
             minor: minor.ToString(),
             build: build.ToString(),
             revision: revision.ToString())
    { }

#if NET7_0_OR_GREATER
    /// <summary>
    /// Parses the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    public static AlphanumericVersion Parse([DisallowNull] String source) =>
        Parse(source: source);

    /// <summary>
    /// Tries to parse the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    /// <returns><see langword="true"/> if the parsing succeeded; otherwise, <see langword="false"/></returns>
    public static Boolean TryParse([DisallowNull] String source,
                                   out AlphanumericVersion result) =>
        TryParse(source: source,
                 result: out result);
#endif

    /// <inheritdoc/>
    public override Boolean Equals(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [NotNullWhen(true)]
#endif
        Object? obj) =>
            obj is AlphanumericVersion other &&
            this.CompareTo(other) == 0;

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
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public override String ToString() =>
        this.ToString(null);

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
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public String ToString(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        String? format)
    {
        format ??= "#.#.#.#";

        Int32 count = format.Count(x => x == '#')
                            .Clamp(1, 4);
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        ReadOnlySpan<Char> working = format;
#else
        String working = format;
#endif
        StringBuilder builder = new();

        Int32 index;

        for (Int32 i = 0;
             i < count;
             i++)
        {
            String current = i switch
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
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
                builder.Append(working[0..index]);
#else
                builder.Append(working.Substring(0, index));
#endif
            }

            if (String.IsNullOrWhiteSpace(current))
            {
                builder.Append('0');
            }
            else
            {
                builder.Append(current);
            }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
            working = working[++index..];
#else
            working = working.Substring(index + 1);
#endif
        }

        if (working.Length > 0)
        {
            builder.Append(working);
        }

        return builder.ToString();
    }

#if (NET5_0 || NET6_0) && !NET7_0_OR_GREATER
    /// <inheritdoc/>
    public static AlphanumericVersion Parse([DisallowNull] String source)
    {
        source.ThrowIfNullOrEmpty(asArgumentException: true);

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

        for (Int32 i = 0;
             i < segments.Length;
             i++)
        {
            if (String.IsNullOrWhiteSpace(segments[i]) ||
                !s_Regex.IsMatch(input: segments[i]))
            {
                FormatException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC);
                exception.Data.Add(key: "Value",
                                   value: segments[i]);
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
    public static Boolean TryParse([NotNullWhen(true)] String? source,
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

        for (Int32 i = 0;
             i < segments.Length;
             i++)
        {
            if (!s_Regex.IsMatch(input: segments[i]))
            {
                result = default;
                return false;
            }
            if (String.IsNullOrWhiteSpace(segments[i]))
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
#endif

    /// <summary>
    /// Gets the major version component of this <see cref="AlphanumericVersion"/>.
    /// </summary>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
    public String Major => 
        m_Major;

    /// <summary>
    /// Gets the minor version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no minor version component is specified.
    /// </summary>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
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
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
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
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
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

    /// <summary>
    /// Implicit conversion from <see cref="Version"/> class.
    /// </summary>
    public static implicit operator AlphanumericVersion(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        Version? source)
    {
        if (source is null)
        {
            return new();
        }

        if (source.Revision > -1)
        {
            return new(major: source.Major,
                       minor: source.Minor,
                       build: source.Build,
                       revision: source.Revision);
        }
        if (source.Build > -1)
        {
            return new(major: source.Major,
                       minor: source.Minor,
                       build: source.Build);
        }
        return new(major: source.Major,
                   minor: source.Minor);
    }

#if (NET5_0 || NET6_0) && !NET7_0_OR_GREATER
    /// <inheritdoc/>
    public static Boolean operator ==(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) == 0;
    }

    /// <inheritdoc/>
    public static Boolean operator !=(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) != 0;
    }
#endif
}

// Non-Public
partial struct AlphanumericVersion
{
    private AlphanumericVersion(in AlphanumericVersion original)
    {
        m_Major = original.m_Major;
        m_Minor = original.m_Minor;
        m_Build = original.m_Build;
        m_Revision = original.m_Revision;
    }

    private static Int32 CompareComponent(String? me,
                                          String? other)
    {
        if (me is null)
        {
            if (other is null)
            {
                return 0;
            }
            return -1;
        }
        if (other is null)
        {
            return 1;
        }

        if (me.Length > other.Length)
        {
            return 1;
        }
        if (me.Length < other.Length)
        {
            return -1;
        }

        for (Int32 i = 0;
             i < me.Length; 
             i++)
        {
            if (me[i] > other[i])
            {
                return 1;
            }
            if (me[i] < other[i])
            {
                return -1;
            }
        }
        return 0;
    }

    private static readonly Regex s_Regex = new(@"^[a-zA-Z0-9\-]*$");
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String m_Major;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? m_Minor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? m_Build;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? m_Revision;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC = "The specifier for the version component needs to be alphanumeric.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SEGMENT_COUNT_OUT_OF_BOUNDS = "The number of version components is out of the allowed bounds (min: 1, max: 4).";
}

// ICloneable
partial struct AlphanumericVersion : ICloneable
{
    /// <summary>
    /// Creates a new object that is an exact copy of this instance.
    /// </summary>
    /// <returns>A new object that is an exact copy of this instance.</returns>
    public AlphanumericVersion Clone() =>
        new(original: this);

    /// <inheritdoc/>
    Object ICloneable.Clone() =>
        new AlphanumericVersion(original: this);
}

// IComparable
partial struct AlphanumericVersion : IComparable
{
    Int32 IComparable.CompareTo(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [AllowNull]
#endif
        Object? obj)
    {
        if (obj is AlphanumericVersion other)
        {
            return this.CompareTo(other);
        }
        return 1;
    }
}

// IComparable<T>
partial struct AlphanumericVersion : IComparable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Int32 CompareTo(AlphanumericVersion other)
    {
        Int32 result = CompareComponent(me: m_Major, 
                                        other: other.m_Major);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(me: m_Minor,
                                  other: other.m_Minor);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(me: m_Build,
                                  other: other.m_Build);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(me: m_Revision,
                                  other: other.m_Revision);

        return result;
    }
}

// IEquatable<T>
partial struct AlphanumericVersion : IEquatable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Boolean Equals(AlphanumericVersion other) =>
        this.CompareTo(other) == 0;
}

#if NET7_0_OR_GREATER
// IEqualityOperators<T, U>
partial struct AlphanumericVersion : IEqualityOperators<AlphanumericVersion, AlphanumericVersion, Boolean>
{
    /// <inheritdoc/>
    public static Boolean operator ==(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) == 0;
    }

    /// <inheritdoc/>
    public static Boolean operator !=(AlphanumericVersion left,
                                      AlphanumericVersion right)
    {
        return left.CompareTo(right) != 0;
    }
}

// IParsable<T>
partial struct AlphanumericVersion : IParsable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public static AlphanumericVersion Parse([DisallowNull] String source, 
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

        for (Int32 i = 0;
             i < segments.Length; 
             i++)
        {
            if (String.IsNullOrWhiteSpace(segments[i]) ||
                !s_Regex.IsMatch(input: segments[i]))
            {
                FormatException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC);
                exception.Data.Add(key: "Value",
                                   value: segments[i]);
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
    public static Boolean TryParse([NotNullWhen(true)] String? source, 
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

        for (Int32 i = 0;
             i < segments.Length;
             i++)
        {
            if (!s_Regex.IsMatch(input: segments[i]))
            {
                result = default;
                return false;
            }
            if (String.IsNullOrWhiteSpace(segments[i]))
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

// IStructuralComparable
partial struct AlphanumericVersion : IStructuralComparable
{
    Int32 IStructuralComparable.CompareTo(Object? other,
                                          IComparer comparer)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(comparer);
#else
        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
#endif

        return comparer.Compare(x: this,
                                y: other);
    }
}

// IStructuralEquatable
partial struct AlphanumericVersion : IStructuralEquatable
{
    Boolean IStructuralEquatable.Equals(Object? other,
                                        IEqualityComparer comparer)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(comparer);
#else
        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
#endif

        return comparer.Equals(x: this,
                               y: other);
    }

    Int32 IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(comparer);
#else
        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
#endif

        return comparer.GetHashCode(this);
    }
}