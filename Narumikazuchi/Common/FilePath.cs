
namespace Narumikazuchi;

/// <summary>
/// Represents a directory or file path.
/// </summary>
public readonly partial struct FilePath : IEnumerable<Char>, IEquatable<FilePath>
{
#pragma warning disable CS1591
    static public implicit operator FilePath(String path)
    {
        return new(path: path);
    }

    static public implicit operator FilePath(ReadOnlySpan<Char> path)
    {
        String spanAsString = path.ToString();
        return new(path: spanAsString);
    }

    static public implicit operator FilePath(FileSystemInfo file)
    {
        return new(path: file.FullName);
    }

    static public implicit operator String(FilePath path)
    {
        return path.ToString();
    }

    static public FilePath operator /(FilePath left,
                                      FilePath right)
    {
        return left.Append(other: right);
    }

    static public FilePath operator /(String left,
                                      FilePath right)
    {
        return new FilePath(path: left).Append(other: right);
    }

    static public FilePath operator /(FilePath left,
                                      String right)
    {
        return left.Append(other: right);
    }

    static public FilePath operator /(FileSystemInfo left,
                                      FilePath right)
    {
        return new FilePath(path: left.FullName).Append(other: right);
    }

    static public FilePath operator /(FilePath left,
                                      FileSystemInfo right)
    {
        return left.Append(other: right.FullName);
    }

    static public Boolean operator ==(FilePath left,
                                      FilePath right)
    {
        return left.Equals(other: right) is true;
    }

    static public Boolean operator !=(FilePath left,
                                      FilePath right)
    {
        return left.Equals(other: right) is false;
    }
#pragma warning restore CS1591

    /// <summary>
    /// Instantiates a new instance of the <see cref="FilePath"/> struct.
    /// </summary>
    /// <param name="path">The path that the instance should represent.</param>
    public FilePath(Optional<String> path)
    {
        if (path.HasValue)
        {
            m_Value = RemoveInvalidCharactersFrom(value: path.Value);
        }
        else
        {
            m_Value = String.Empty;
        }
    }

    /// <summary>
    /// Appends the given <paramref name="other"/> string as path to this instance value and returns a new <see cref="FilePath"/> where the paths are concatenated.
    /// </summary>
    /// <param name="other">The path to append to this instance.</param>
    /// <returns>A new <see cref="FilePath"/> where the paths are concatenated</returns>
    public readonly FilePath Append(String other)
    {
        String result = RemoveInvalidCharactersFrom(value: other);
        if (result.Length > 0 &&
            result[0] is '/')
        {
            result = RemoveInvalidCharactersFrom(value: $"{m_Value}/{result.AsSpan()[1..]}");
        }
        else if (String.IsNullOrWhiteSpace(value: result) is true)
        {
            return this;
        }
        else
        {
            result = $"{m_Value}/{result}";
        }

        return new(path: result);
    }
    /// <summary>
    /// Appends the given <paramref name="other"/> string as path to this instance value and returns a new <see cref="FilePath"/> where the paths are concatenated.
    /// </summary>
    /// <param name="other">The path to append to this instance.</param>
    /// <returns>A new <see cref="FilePath"/> where the paths are concatenated</returns>
    public readonly FilePath Append(FilePath other)
    {
        String combined;
        if (other.m_Value.Length > 0 &&
            other.m_Value[0] is '/')
        {
            combined = RemoveInvalidCharactersFrom(value: $"{m_Value}/{other.m_Value.AsSpan()[1..]}");
        }
        else if (other.IsEmpty is true)
        {
            return this;
        }
        else
        {
            combined = $"{m_Value}/{other.m_Value}";
        }

        return new(path: combined);
    }

    /// <inheritdoc/>
    public readonly Boolean Equals(FilePath other)
    {
        if (m_Value is null)
        {
            return other.m_Value is null;
        }

        Optional<String> left = String.IsInterned(str: m_Value);
        Optional<String> right = String.IsInterned(str: other.m_Value);

        if (left.HasValue is true &&
            right.HasValue is true)
        {
            return ReferenceEquals(objA: left.Value,
                                   objB: right.Value);
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public readonly override Boolean Equals([NotNullWhen(true)] Object? obj)
    {
        return obj is FilePath other &&
               this.Equals(other: other) is true;
    }

    /// <summary>
    /// Returns an enumerator that can iterate through the characters in this path.
    /// </summary>
    /// <returns>An enumerator</returns>
    public readonly CharEnumerator GetEnumerator()
    {
        if (m_Value is null)
        {
            return String.Empty.GetEnumerator();
        }
        else
        {
            return m_Value.GetEnumerator();
        }
    }

    /// <inheritdoc/>
    public readonly override Int32 GetHashCode()
    {
        if (m_Value is null)
        {
            return String.Empty.GetHashCode();
        }
        else
        {
            return m_Value.GetHashCode();
        }
    }

    /// <summary>
    /// Replaces all occurences of <paramref name="oldValue"/> with the value of <paramref name="newValue"/>.
    /// </summary>
    /// <param name="oldValue">The string to replace.</param>
    /// <param name="newValue">The new string to replace <paramref name="oldValue"/> with.</param>
    /// <param name="comparison">The comparision style to use when looking for <paramref name="oldValue"/>.</param>
    /// <returns>A new <see cref="FilePath"/> where all occurences of <paramref name="oldValue"/> were replaced by <paramref name="newValue"/></returns>
    public readonly FilePath Replace(String oldValue,
                                     String newValue,
                                     StringComparison comparison = StringComparison.InvariantCulture)
    {
        if (m_Value is null)
        {
            return this;
        }

        String result = m_Value.Replace(oldValue: oldValue,
                                        newValue: newValue,
                                        comparisonType: comparison);
        return new(path: result);
    }

    /// <summary>
    /// Determines whether the specified path is a part of this path.
    /// </summary>
    /// <param name="path">The path to check for in the current instance.</param>
    /// <returns><see langword="true"/> if the <paramref name="path"/> is part of this instance; otherwise, <see langword="false"/></returns>
    public readonly Boolean SharesCommonPathWith(FilePath path)
    {
        if (m_Value is null)
        {
            return path.m_Value is null;
        }
        else if (path.m_Value is null)
        {
            return true;
        }
        else
        {
            return m_Value.StartsWith(value: path.m_Value);
        }
    }

    /// <summary>
    /// Returns the <see cref="FilePath"/> to the parent directory, if it exists.
    /// </summary>
    /// <returns>The path to the parent directory.</returns>
    public readonly FilePath GetParent()
    {
        if (m_Value is null)
        {
            return new(path: String.Empty);
        }
        else
        {
            Int32 index = m_Value.LastIndexOf(value: '/');
            if (index is -1)
            {
                return this;
            }
            else
            {
                return new(path: m_Value[..index]);
            }
        }
    }

    /// <summary>
    /// Returns this path in string form.
    /// </summary>
    /// <returns>This path as string.</returns>
    public readonly override String ToString()
    {
        if (m_Value is null)
        {
            return String.Empty;
        }
        else
        {
            return m_Value;
        }
    }

    /// <summary>
    /// Gets the extension of the file represented by this path. If the path does not represent a file or the file has no extension this returns an empty string.
    /// </summary>
    public String Extension
    {
        get
        {
            if (m_Value is null)
            {
                return String.Empty;
            }
            else
            {
                Int32 index = m_Value.LastIndexOf(value: '.');
                if (index is -1)
                {
                    return String.Empty;
                }
                else
                {
                    return m_Value[index..];
                }
            }
        }
    }

    /// <summary>
    /// Gets the extension of the file or directory represented by this path.Returns an empty string when the path is empty.
    /// </summary>
    public String Name
    {
        get
        {
            if (m_Value is null)
            {
                return String.Empty;
            }
            else
            {
                Int32 index = m_Value.LastIndexOf(value: '/');
                if (index is -1)
                {
                    return String.Empty;
                }
                else
                {
                    return m_Value[index..];
                }
            }
        }
    }

    /// <summary>
    /// Gets whether or not this instance has a value.
    /// </summary>
    public Boolean IsEmpty
    {
        get
        {
            return String.IsNullOrWhiteSpace(value: m_Value) is true;
        }
    }

    /// <summary>
    /// Gets the path to the root for this instance. Returns an empty string if this path is empty.
    /// </summary>
    public FilePath Root
    {
        get
        {
            if (m_Value is null)
            {
                return new(path: String.Empty);
            }
            else
            {
                Int32 index = m_Value.IndexOf(value: '/');
                if (index is -1)
                {
                    return new(path: String.Empty);
                }
                else
                {
                    return new(path: m_Value[..(index + 1)]);
                }
            }
        }
    }
}