namespace Narumikazuchi;

/// <summary>
/// Represents a result where the functioncall can fail and actually return an error.
/// </summary>
public readonly struct Result<TResult, TError>
{
#pragma warning disable CS1591
    static public implicit operator Result<TResult, TError>(TResult value)
    {
        return new(value: value);
    }
    static public implicit operator Result<TResult, TError>(TError error)
    {
        return new(error: error);
    }
#pragma warning restore CS1591

    /// <summary>
    /// Instantiates a new instance of the <see cref="Result{TResult, TError}"/> struct.
    /// </summary>
    /// <param name="value">The value to wrap in this instance.</param>
    public Result(TResult value)
    {
        this.IsOk = true;
        m_Error = default;
        m_Value = value;
    }
    /// <summary>
    /// Instantiates a new instance of the <see cref="Result{TResult, TError}"/> struct.
    /// </summary>
    /// <param name="error">The error to wrap in this instance.</param>
    public Result(TError error)
    {
        this.IsError = true;
        m_Error = error;
        m_Value = default;
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
        else if (m_Error is Exception exception)
        {
            return exception.Message;
        }
        else if (m_Error is not null)
        {
            String? value = m_Error.ToString();
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
    /// If this wraps a value then the <paramref name="transformer"/> will map it to <typeparamref name="TOther"/>.
    /// Otherwise the wrapped <typeparamref name="TError"/> will just get passed on.
    /// </summary>
    /// <param name="transformer">The transformation to apply to the wrapped value.</param>
    /// <returns>A <see cref="Result{TResult}"/> wrapping the transformed value or wrapping the stored <typeparamref name="TError"/></returns>
    public readonly Result<TOther, TError> AndThen<TOther>(Func<TResult, TOther> transformer)
    {
        if (this.IsOk is true)
        {
            return transformer.Invoke(arg: this.Value);
        }
        else if (this.IsError is true)
        {
            return new(error: this.Error);
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// If this wraps a <typeparamref name="TError"/> then the <paramref name="transformer"/> will map it to <typeparamref name="TOther"/>.
    /// Otherwise the wrapped value will just get passed on.
    /// </summary>
    /// <param name="transformer">The transformation to apply to the wrapped <typeparamref name="TError"/>.</param>
    /// <returns>A <see cref="Result{TResult}"/> wrapping the transformed <typeparamref name="TError"/> or wrapping the stored value</returns>
    public readonly Result<TResult, TOther> OrElse<TOther>(Func<TError, TOther> transformer)
        where TOther : Exception
    {
        if (this.IsOk is true)
        {
            return new(value: this.Value);
        }
        else if (this.IsError is true)
        {
            return transformer.Invoke(arg: this.Error);
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// If this instance wraps a value it gets returned, otherwise the <paramref name="defaultValue"/> will get returned.
    /// </summary>
    /// <param name="defaultValue">The value to return if this instance doesn't wrap a value.</param>
    /// <returns>The value wrapped in this instance or <paramref name="defaultValue"/> if this instance does not wrap a value</returns>
    public readonly TResult UnwrapOr(TResult defaultValue)
    {
        if (this.IsOk is true)
        {
            return this.Value;
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Gets whether or not this instance wraps a value.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Value))]
    public readonly Boolean IsOk
    {
        get;
    }

    /// <summary>
    /// Gets whether or not this instance wraps a <typeparamref name="TError"/>.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public readonly Boolean IsError
    {
        get;
    }

    /// <summary>
    /// Gets whether or not this instance actually wraps anything.
    /// </summary>
    public readonly Boolean IsEmpty
    {
        get
        {
            return this.IsOk is false &&
                   this.IsError is false;
        }
    }

    /// <summary>
    /// Gets the <typeparamref name="TError"/> that this instance wraps, if it wraps a <typeparamref name="TError"/>.
    /// </summary>
    public readonly TError? Error
    {
        get
        {
            return m_Error;
        }
    }

    /// <summary>
    /// Gets the value that this instance wraps, if it wraps a value.
    /// </summary>
    public readonly TResult? Value
    {
        get
        {
            return m_Value;
        }
    }

    private readonly TError? m_Error;
    private readonly TResult? m_Value;

    private const String NULL = "null";
}