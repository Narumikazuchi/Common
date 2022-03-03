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
    public AlphanumericVersion([DisallowNull] String major!!)
    {
        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!)
    {
        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data
                     .Add(key: "Value",
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
    public AlphanumericVersion([DisallowNull] String major!!,
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
                               [DisallowNull] String minor!!) :
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!,
                               [DisallowNull] String build!!)
    {
        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data
                     .Add(key: "Value",
                          value: minor);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: build))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(build));
            exception.Data
                     .Add(key: "Value",
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!,
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               in Int64 minor,
                               [DisallowNull] String build!!) :
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
                               [DisallowNull] String minor!!,
                               [DisallowNull] String build!!) :
        this(major: major.ToString(),
             minor: minor,
             build: build)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion([DisallowNull] String major!!,
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
                               [DisallowNull] String minor!!,
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
                               [DisallowNull] String build!!) :
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!,
                               [DisallowNull] String build!!,
                               [DisallowNull] String revision!!)
    {
        if (!s_Regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data
                     .Add(key: "Value",
                          value: minor);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: build))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(build));
            exception.Data
                     .Add(key: "Value",
                          value: build);
            throw exception;
        }
        if (!s_Regex.IsMatch(input: revision))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(revision));
            exception.Data
                     .Add(key: "Value",
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!,
                               [DisallowNull] String build!!,
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!,
                               in Int64 build,
                               [DisallowNull] String revision!!) :
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               in Int64 minor,
                               [DisallowNull] String build!!,
                               [DisallowNull] String revision!!) :
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
                               [DisallowNull] String minor!!,
                               [DisallowNull] String build!!,
                               [DisallowNull] String revision!!) :
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               [DisallowNull] String minor!!,
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               in Int64 minor,
                               [DisallowNull] String build!!,
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
                               [DisallowNull] String minor!!,
                               [DisallowNull] String build!!,
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
    public AlphanumericVersion([DisallowNull] String major!!,
                               in Int64 minor,
                               in Int64 build,
                               [DisallowNull] String revision!!) :
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
                               [DisallowNull] String minor!!,
                               in Int64 build,
                               [DisallowNull] String revision!!) :
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
                               [DisallowNull] String build!!,
                               [DisallowNull] String revision!!) :
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
    public AlphanumericVersion([DisallowNull] String major!!,
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
                               [DisallowNull] String minor!!,
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
                               [DisallowNull] String build!!,
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
                               [DisallowNull] String revision!!) :
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

    /// <summary>
    /// Parses the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    public static AlphanumericVersion Parse([DisallowNull] String source!!) =>
        Parse(source: source, 
              provider: null);

    /// <summary>
    /// Tries to parse the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    /// <returns><see langword="true"/> if the parsing succeeded; otherwise, <see langword="false"/></returns>
    public static Boolean TryParse([DisallowNull] String source!!,
                                   out AlphanumericVersion result) =>
        TryParse(source: source,
                 provider: null,
                 result: out result);

    /// <inheritdoc/>
    public override Boolean Equals([NotNullWhen(true)] Object? obj) =>
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

    /// <inheritdoc/>
    public override String ToString()
    {
        if (m_Major is null)
        {
            return "0.0.0.0";
        }

        StringBuilder builder = new();
        builder.Append(value: m_Major);

        if (m_Minor is null)
        {
            return builder.ToString();
        }

        if (Char.IsDigit(m_Minor[0]))
        {
            builder.Append(value: '.');
        }
        else
        {
            builder.Append(value: '-');
        }
        builder.Append(value: m_Minor);

        if (m_Build is null)
        {
            return builder.ToString();
        }

        if (Char.IsDigit(m_Build[0]))
        {
            builder.Append(value: '.');
        }
        else
        {
            builder.Append(value: '-');
        }
        builder.Append(value: m_Build);

        if (m_Revision is null)
        {
            return builder.ToString();
        }


        if (Char.IsDigit(m_Revision[0]))
        {
            builder.Append(value: '.');
        }
        else
        {
            builder.Append(value: '-');
        }
        builder.Append(value: m_Revision);
        return builder.ToString();
    }

    /// <summary>
    /// Gets the major version component of this <see cref="AlphanumericVersion"/>.
    /// </summary>
    public String Major => 
        m_Major;
    /// <summary>
    /// Gets the minor version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no minor version component is specified.
    /// </summary>
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
    public static implicit operator AlphanumericVersion(Version source!!)
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
    /// <inheritdoc/>
    public Object Clone() =>
        new AlphanumericVersion(original: this);
}

// IComparable
partial struct AlphanumericVersion : IComparable
{
    Int32 IComparable.CompareTo([AllowNull] Object? obj)
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

// IEqualityOperators<T, U>
partial struct AlphanumericVersion : IEqualityOperators<AlphanumericVersion, AlphanumericVersion>
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

// IEquatable<T>
partial struct AlphanumericVersion : IEquatable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public Boolean Equals(AlphanumericVersion other) =>
        this.CompareTo(other) == 0;
}

// IParseable<T>
partial struct AlphanumericVersion : IParseable<AlphanumericVersion>
{
    /// <inheritdoc/>
    public static AlphanumericVersion Parse([DisallowNull] String source!!, 
                                            [AllowNull] IFormatProvider? provider)
    {
        String[] segments = source.Split(separator: '.', 
                                         options: StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length is < 1
                            or > 4)
        {
            ArgumentException exception = new(message: SEGMENT_COUNT_OUT_OF_BOUNDS,
                                              paramName: nameof(source));
            exception.Data
                     .Add(key: "Number of Segments",
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
                exception.Data
                         .Add(key: "Value",
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

// IStructuralComparable
partial struct AlphanumericVersion : IStructuralComparable
{
    Int32 IStructuralComparable.CompareTo([AllowNull] Object? other,
                                          [DisallowNull] IComparer comparer!!) => 
        comparer.Compare(x: this,
                         y: other);
}

// IStructuralEquatable
partial struct AlphanumericVersion : IStructuralEquatable
{
    Boolean IStructuralEquatable.Equals([AllowNull] Object? other,
                                        [DisallowNull] IEqualityComparer comparer!!) => 
        comparer.Equals(x: this,
                        y: other);
    Int32 IStructuralEquatable.GetHashCode([DisallowNull] IEqualityComparer comparer!!) => 
        comparer.GetHashCode(this);
}