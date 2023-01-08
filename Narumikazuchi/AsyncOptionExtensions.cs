namespace Narumikazuchi;

/// <summary>
/// Contains helpers for async operations involving <see cref="Option{T}"/>.
/// </summary>
public static class AsyncOptionExtensions
{
    /// <summary>
    /// Gets an awaiter used to await this <see cref="Task"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    public static TaskAwaiter GetAwaiter(this Option<Task> option)
    {
        if (option.HasValue)
        {
            return ((Task)option!).GetAwaiter();
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Gets an awaiter used to await this <see cref="Task{T}"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    public static TaskAwaiter<T> GetAwaiter<T>(this Option<Task<T>> option)
    {
        if (option.HasValue)
        {
            return ((Task<T>)option!).GetAwaiter();
        }
        else
        {
            return default;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets an awaiter used to await this <see cref="ValueTask"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    public static ValueTaskAwaiter GetAwaiter(this Option<ValueTask> option)
    {
        if (option.HasValue)
        {
            return ((ValueTask)option!).GetAwaiter();
        }
        else
        {
            return default;
        }
    }
    
    /// <summary>
    /// Gets an awaiter used to await this <see cref="ValueTask{T}"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    public static ValueTaskAwaiter<T> GetAwaiter<T>(this Option<ValueTask<T>> option)
    {
        if (option.HasValue)
        {
            return ((ValueTask<T>)option!).GetAwaiter();
        }
        else
        {
            return default;
        }
    }
#endif
}