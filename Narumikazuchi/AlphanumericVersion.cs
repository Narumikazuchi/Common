﻿namespace Narumikazuchi;

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
    public AlphanumericVersion([DisallowNull] String major)
    {
        ExceptionHelpers.ThrowIfArgumentNull(major);

        if (!_regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }

        this._major = major;
        this._minor = null;
        this._build = null;
        this._revision = null;
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
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor)
    {
        ExceptionHelpers.ThrowIfArgumentNull(major);
        ExceptionHelpers.ThrowIfArgumentNull(minor);

        if (!_regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }
        if (!_regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data
                     .Add(key: "Value",
                          value: minor);
            throw exception;
        }

        this._major = major;
        this._minor = minor;
        this._build = null;
        this._revision = null;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion([DisallowNull] String major,
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
                   [DisallowNull] String minor) :
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
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor,
                   [DisallowNull] String build)
    {
        ExceptionHelpers.ThrowIfArgumentNull(major);
        ExceptionHelpers.ThrowIfArgumentNull(minor);
        ExceptionHelpers.ThrowIfArgumentNull(build);

        if (!_regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }
        if (!_regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data
                     .Add(key: "Value",
                          value: minor);
            throw exception;
        }
        if (!_regex.IsMatch(input: build))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(build));
            exception.Data
                     .Add(key: "Value",
                          value: build);
            throw exception;
        }

        this._major = major;
        this._minor = minor;
        this._build = build;
        this._revision = null;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor,
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
    public AlphanumericVersion([DisallowNull] String major,
                   in Int64 minor,
                   [DisallowNull] String build) :
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
                   [DisallowNull] String minor,
                   [DisallowNull] String build) :
        this(major: major.ToString(),
             minor: minor,
             build: build)
    { }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion([DisallowNull] String major,
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
                   [DisallowNull] String minor,
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
                   [DisallowNull] String build) :
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
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor,
                   [DisallowNull] String build,
                   [DisallowNull] String revision)
    {
        ExceptionHelpers.ThrowIfArgumentNull(major);
        ExceptionHelpers.ThrowIfArgumentNull(minor);
        ExceptionHelpers.ThrowIfArgumentNull(build);
        ExceptionHelpers.ThrowIfArgumentNull(revision);

        if (!_regex.IsMatch(input: major))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(major));
            exception.Data
                     .Add(key: "Value",
                          value: major);
            throw exception;
        }
        if (!_regex.IsMatch(input: minor))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(minor));
            exception.Data
                     .Add(key: "Value",
                          value: minor);
            throw exception;
        }
        if (!_regex.IsMatch(input: build))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(build));
            exception.Data
                     .Add(key: "Value",
                          value: build);
            throw exception;
        }
        if (!_regex.IsMatch(input: revision))
        {
            ArgumentException exception = new(message: SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC,
                                              paramName: nameof(revision));
            exception.Data
                     .Add(key: "Value",
                          value: revision);
            throw exception;
        }

        this._major = major;
        this._minor = minor;
        this._build = build;
        this._revision = revision;
    }
    /// <summary>
    /// Creates a new instance of the <see cref="AlphanumericVersion"/> struct.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor,
                   [DisallowNull] String build,
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
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor,
                   in Int64 build,
                   [DisallowNull] String revision) :
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
    public AlphanumericVersion([DisallowNull] String major,
                   in Int64 minor,
                   [DisallowNull] String build,
                   [DisallowNull] String revision) :
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
                   [DisallowNull] String minor,
                   [DisallowNull] String build,
                   [DisallowNull] String revision) :
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
    public AlphanumericVersion([DisallowNull] String major,
                   [DisallowNull] String minor,
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
    public AlphanumericVersion([DisallowNull] String major,
                   in Int64 minor,
                   [DisallowNull] String build,
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
                   [DisallowNull] String minor,
                   [DisallowNull] String build,
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
    public AlphanumericVersion([DisallowNull] String major,
                   in Int64 minor,
                   in Int64 build,
                   [DisallowNull] String revision) :
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
                   [DisallowNull] String minor,
                   in Int64 build,
                   [DisallowNull] String revision) :
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
                   [DisallowNull] String build,
                   [DisallowNull] String revision) :
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
    public AlphanumericVersion([DisallowNull] String major,
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
                   [DisallowNull] String minor,
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
                   [DisallowNull] String build,
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
                   [DisallowNull] String revision) :
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
    public static AlphanumericVersion Parse([DisallowNull] String source) =>
        Parse(source: source, 
              provider: null);

    /// <summary>
    /// Tries to parse the specified string into a new <see cref="AlphanumericVersion"/> object.
    /// </summary>
    /// <returns><see langword="true"/> if the parsing succeeded; otherwise, <see langword="false"/></returns>
    public static Boolean TryParse([DisallowNull] String source,
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
        if (this._major is null)
        {
            return Int32.MaxValue;
        }

        Int32 result = this._major
                           .GetHashCode();

        if (this._minor is null)
        {
            return result;
        }

        result ^= this._minor
                      .GetHashCode();

        if (this._build is null)
        {
            return result;
        }

        result ^= this._build
                      .GetHashCode();

        if (this._revision is null)
        {
            return result;
        }

        result ^= this._revision
                      .GetHashCode();

        return result;
    }

    /// <inheritdoc/>
    public override String ToString()
    {
        if (this._major is null)
        {
            return "0.0.0.0";
        }

        StringBuilder builder = new();
        builder.Append(value: this._major);

        if (this._minor is null)
        {
            return builder.ToString();
        }

        if (Char.IsDigit(this._minor[0]))
        {
            builder.Append(value: '.');
        }
        else
        {
            builder.Append(value: '-');
        }
        builder.Append(value: this._minor);

        if (this._build is null)
        {
            return builder.ToString();
        }

        if (Char.IsDigit(this._build[0]))
        {
            builder.Append(value: '.');
        }
        else
        {
            builder.Append(value: '-');
        }
        builder.Append(value: this._build);

        if (this._revision is null)
        {
            return builder.ToString();
        }


        if (Char.IsDigit(this._revision[0]))
        {
            builder.Append(value: '.');
        }
        else
        {
            builder.Append(value: '-');
        }
        builder.Append(value: this._revision);
        return builder.ToString();
    }

    /// <summary>
    /// Gets the major version component of this <see cref="AlphanumericVersion"/>.
    /// </summary>
    public String Major => 
        this._major;
    /// <summary>
    /// Gets the minor version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no minor version component is specified.
    /// </summary>
    public String Minor
    {
        get
        {
            if (this._minor is null)
            {
                return "-1";
            }
            return this._minor;
        }
    }
    /// <summary>
    /// Gets the build version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no build version component is specified.
    /// </summary>
    public String Build
    {
        get
        {
            if (this._build is null)
            {
                return "-1";
            }
            return this._build;
        }
    }
    /// <summary>
    /// Gets the revision version component of this <see cref="AlphanumericVersion"/>. Returns -1 if no revision  version component is specified.
    /// </summary>
    public String Revision
    {
        get
        {
            if (this._revision is null)
            {
                return "-1";
            }
            return this._revision;
        }
    }

    /// <summary>
    /// Implicit conversion from <see cref="Version"/> class.
    /// </summary>
    public static implicit operator AlphanumericVersion(Version source)
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
        this._major = original._major;
        this._minor = original._minor;
        this._build = original._build;
        this._revision = original._revision;
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

    private static readonly Regex _regex = new(@"^[a-zA-Z0-9\-]*$");
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String _major;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? _minor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? _build;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? _revision;

#pragma warning disable IDE1006
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC = "The specifier for the version component needs to be alphanumeric.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SEGMENT_COUNT_OUT_OF_BOUNDS = "The number of version components is out of the allowed bounds (min: 1, max: 4).";
#pragma warning restore
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
        Int32 result = CompareComponent(this._major, 
                                        other._major);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(this._minor,
                                  other._minor);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(this._build,
                                  other._build);

        if (result != 0)
        {
            return result;
        }

        result = CompareComponent(this._revision,
                                  other._revision);

        return result;
    }
}

// IEqualityOperators<T, U>
partial struct AlphanumericVersion : IEqualityOperators<AlphanumericVersion, AlphanumericVersion>
{
    /// <inheritdoc/>
    public static Boolean operator ==(AlphanumericVersion left, 
                                      AlphanumericVersion right) =>
        left.CompareTo(right) == 0;
    /// <inheritdoc/>
    public static Boolean operator !=(AlphanumericVersion left, 
                                      AlphanumericVersion right) =>
        left.CompareTo(right) != 0;
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
    public static AlphanumericVersion Parse([DisallowNull] String source, 
                                [AllowNull] IFormatProvider? provider)
    {
        ExceptionHelpers.ThrowIfArgumentNull(source);

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
                !_regex.IsMatch(input: segments[i]))
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
            if (!_regex.IsMatch(input: segments[i]))
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
                                          [DisallowNull] IComparer comparer)
    {
        ExceptionHelpers.ThrowIfArgumentNull(comparer);

        return comparer.Compare(x: this,
                                y: other);
    }
}

// IStructuralEquatable
partial struct AlphanumericVersion : IStructuralEquatable
{
    Boolean IStructuralEquatable.Equals([AllowNull] Object? other, 
                                        [DisallowNull] IEqualityComparer comparer)
    {
        ExceptionHelpers.ThrowIfArgumentNull(comparer);

        return comparer.Equals(x: this,
                               y: other);
    }
    Int32 IStructuralEquatable.GetHashCode([DisallowNull] IEqualityComparer comparer)
    {
        ExceptionHelpers.ThrowIfArgumentNull(comparer);

        return comparer.GetHashCode(this);
    }
}