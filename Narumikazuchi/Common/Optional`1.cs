namespace Narumikazuchi;

/// <summary>
/// Represents a value that might or might not be set.
/// </summary>
public readonly struct Optional<TAny>
{
#pragma warning disable CS1591
    [return: NotNullIfNotNull(nameof(value))]
    static public implicit operator Optional<TAny>(TAny? value)
    {
        return new(value: value);
    }

    static public implicit operator Result<TAny, NullReferenceException>(Optional<TAny> value)
    {
        if (value.HasValue is true)
        {
            return value.Value;
        }
        else
        {
            return new NullReferenceException();
        }
    }
#pragma warning restore CS1591

    /// <summary>
    /// Instantiates a new instance of the <see cref="Optional{TAny}"/> struct.
    /// </summary>
    /// <param name="value">The value to with this instance.</param>
    /// <param name="overrideHasValue">Whether or not the <see cref="HasValue"/> will be set to <see langword="true"/></param>
    public Optional(TAny? value = default,
                    Boolean? overrideHasValue = default)
    {
        if (overrideHasValue is null)
        {
            this.HasValue = value is not null;
        }
        else
        {
            this.HasValue = overrideHasValue.Value is true;
        }

        m_Value = value;
    }

    /// <inheritdoc/>
    public readonly override String ToString()
    {
        if (m_Value is not null)
        {
            String? value = m_Value.ToString();
            if (value is null)
            {
                return String.Empty;
            }
            else
            {
                return value;
            }
        }
        else
        {
            return NULL;
        }
    }

    /// <summary>
    /// Transforms this instance into an instance of another type. The <paramref name="transformer"/> will only be called
    /// if this instance actually holds a value.
    /// </summary>
    /// <param name="transformer">The transformation function to map from <typeparamref name="TAny"/> to <typeparamref name="TOther"/>.</param>
    /// <returns>An <see cref="Optional{TAny}"/> of type <typeparamref name="TOther"/> depending on whether this instance has a value or not</returns>
    public readonly Optional<TOther> Map<TOther>(Func<TAny, TOther> transformer)
    {
        if (this.HasValue is true)
        {
            return new(value: transformer.Invoke(arg: this.Value));
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Executes the <paramref name="action"/> with the value wrapped in this instance as parameter, but only if this
    /// instance actually holds a value.
    /// </summary>
    /// <param name="action">The action to perform on the wrapped value.</param>
    public readonly void UseFor(Action<TAny> action)
    {
        if (this.HasValue is true)
        {
            action.Invoke(obj: this.Value);
        }
    }

    /// <summary>
    /// If this instance holds a value it gets returned, otherwise the <paramref name="defaultValue"/> will get returned.
    /// </summary>
    /// <param name="defaultValue">The value to return if this instance doesn't hold a value.</param>
    /// <returns>The value wrapped in this instance or <paramref name="defaultValue"/> if this instance does not hold a value</returns>
    public readonly TAny UnwrapOr(TAny defaultValue)
    {
        if (this.HasValue is true)
        {
            return this.Value;
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Gets whether or not this instance holds a value.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Value))]
    public readonly Boolean HasValue
    {
        get;
    }
    
    /// <summary>
    /// Gets the value wrapped in this instance.
    /// </summary>
    public readonly TAny? Value
    {
        get
        {
            return m_Value;
        }
    }

    private readonly TAny? m_Value;

    private const String NULL = "null";
}