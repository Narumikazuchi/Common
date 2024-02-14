namespace Narumikazuchi;

/// <summary>
/// Represents a result where the functioncall can fail and actually return an <see cref="Exception"/>.
/// </summary>
public readonly struct Result<TResult>
{
#pragma warning disable CS1591
    static public implicit operator Result<TResult>(TResult value)
    {
        return new(value: value);
    }
    static public implicit operator Result<TResult>(Exception error)
    {
        return new(error: error);
    }
#pragma warning restore CS1591

    /// <summary>
    /// Instantiates a new instance of the <see cref="Result{TResult}"/> struct.
    /// </summary>
    /// <param name="value">The value to wrap in this instance.</param>
    public Result(TResult value)
    {
        this.IsOk = true;
        m_Error = default;
        m_Value = value;
    }
    /// <summary>
    /// Instantiates a new instance of the <see cref="Result{TResult}"/> struct.
    /// </summary>
    /// <param name="error">The error to wrap in this instance.</param>
    public Result(Exception error)
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
    /// Otherwise the wrapped <see cref="Exception"/> will just get passed on.
    /// </summary>
    /// <param name="transformer">The transformation to apply to the wrapped value.</param>
    /// <returns>A <see cref="Result{TResult}"/> wrapping the transformed value or wrapping the stored <see cref="Exception"/></returns>
    public readonly Result<TOther> AndThen<TOther>(Func<TResult, TOther> transformer)
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
    /// If this wraps an <see cref="Exception"/> then the <paramref name="transformer"/> will map it to <typeparamref name="TOther"/>.
    /// Otherwise the wrapped value will just get passed on.
    /// </summary>
    /// <param name="transformer">The transformation to apply to the wrapped <see cref="Exception"/>.</param>
    /// <returns>A <see cref="Result{TResult}"/> wrapping the transformed <see cref="Exception"/> or wrapping the stored value</returns>
    public readonly Result<TResult> OrElse<TOther>(Func<Exception, TOther> transformer)
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
    /// Gets whether or not this instance wraps an <see cref="Exception"/>.
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
    /// Gets the <see cref="Exception"/> that this instance wraps, if it wraps an <see cref="Exception"/>.
    /// </summary>
    public readonly Exception? Error
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

    private readonly Exception? m_Error;
    private readonly TResult? m_Value;

    private const String NULL = "null";
}