using Narumikazuchi.Collections;

namespace Narumikazuchi;

/// <summary>
/// Contains helpers for the parameter clarifiers (<see cref="MaybeNull{T}"/>, <see cref="MaybeNullOrEmpty{T}"/>, <see cref="NotNull{T}"/>, <see cref="NotNullOrEmpty{T}"/>).
/// </summary>
static public class ParameterExtensions
{
    /// <summary>
    /// Gets an awaiter used to await this <see cref="Task"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    static public TaskAwaiter GetAwaiter(this MaybeNull<Task> value)
    {
        if (!value.IsNull)
        {
            return ((Task)value!).GetAwaiter();
        }
        else
        {
            return default;
        }
    }
    /// <summary>
    /// Gets an awaiter used to await this <see cref="Task"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    static public TaskAwaiter GetAwaiter(this NotNull<Task> value)
    {
        return ((Task)value).GetAwaiter();
    }

    /// <summary>
    /// Gets an awaiter used to await this <see cref="Task{T}"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    static public TaskAwaiter<T> GetAwaiter<T>(this MaybeNull<Task<T>> value)
    {
        if (!value.IsNull)
        {
            return ((Task<T>)value!).GetAwaiter();
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
    static public TaskAwaiter<T> GetAwaiter<T>(this NotNull<Task<T>> value)
    {
        return ((Task<T>)value).GetAwaiter();
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Gets an awaiter used to await this <see cref="ValueTask"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    static public ValueTaskAwaiter GetAwaiter(this MaybeNull<ValueTask> value)
    {
        if (value.IsNull)
        {
            return ((ValueTask)value!).GetAwaiter();
        }
        else
        {
            return default;
        }
    }
    /// <summary>
    /// Gets an awaiter used to await this <see cref="ValueTask"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    static public ValueTaskAwaiter GetAwaiter(this NotNull<ValueTask> value)
    {
        return ((ValueTask)value).GetAwaiter();
    }

    /// <summary>
    /// Gets an awaiter used to await this <see cref="ValueTask{T}"/>.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    static public ValueTaskAwaiter<T> GetAwaiter<T>(this MaybeNull<ValueTask<T>> value)
    {
        if (!value.IsNull)
        {
            return ((ValueTask<T>)value!).GetAwaiter();
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
    static public ValueTaskAwaiter<T> GetAwaiter<T>(this NotNull<ValueTask<T>> value)
    {
        return ((ValueTask<T>)value).GetAwaiter();
    }
#endif

    static public IEnumerator GetEnumerator<TEnumerable>(this MaybeNullOrEmpty<TEnumerable> value)
        where TEnumerable : IEnumerable
    {
        if (!value.IsNull)
        {
            return ((TEnumerable)value!).GetEnumerator();
        }
        else
        {
            return new EmptyEnumerator<Object>();
        }
    }

    static public IEnumerator GetEnumerator<TEnumerable>(this NotNullOrEmpty<TEnumerable> value)
        where TEnumerable : IEnumerable
    {
        return ((TEnumerable)value!).GetEnumerator();
    }

    static public IEnumerator<TElement> GetEnumerator<TEnumerable, TElement>(this MaybeNullOrEmpty<TEnumerable> value)
        where TEnumerable : IEnumerable<TElement>
    {
        if (!value.IsNull)
        {
            return ((TEnumerable)value!).GetEnumerator();
        }
        else
        {
            return new EmptyEnumerator<TElement>();
        }
    }

    static public IEnumerator<TElement> GetEnumerator<TEnumerable, TElement>(this NotNullOrEmpty<TEnumerable> value)
        where TEnumerable : IEnumerable<TElement>
    {
        return ((TEnumerable)value!).GetEnumerator();
    }

    static public TEnumerator GetEnumerator<TEnumerable, TElement, TEnumerator>(this MaybeNullOrEmpty<TEnumerable> value)
        where TEnumerable : IStrongEnumerable<TElement, TEnumerator>
        where TEnumerator : struct, IStrongEnumerator<TElement>
    {
        if (!value.IsNull)
        {
            return ((TEnumerable)value!).GetEnumerator();
        }
        else
        {
            return default;
        }
    }

    static public TEnumerator GetEnumerator<TEnumerable, TElement, TEnumerator>(this NotNullOrEmpty<TEnumerable> value)
        where TEnumerable : IStrongEnumerable<TElement, TEnumerator>
        where TEnumerator : struct, IStrongEnumerator<TElement>
    {
        return ((TEnumerable)value!).GetEnumerator();
    }
}