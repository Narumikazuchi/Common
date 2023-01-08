namespace Narumikazuchi;

/// <summary>
/// Represents the method that will handle an event without event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void EventHandler<TSender>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    TSender? sender,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    EventArgs? eventArgs);
/// <summary>
/// Represents the method that will handle an event with certain event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void EventHandler<TSender, TEventArgs>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    TSender? sender,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    TEventArgs? eventArgs) 
        where TEventArgs : EventArgs;

/// <summary>
/// Represents the method to compare two instances of the same type for equality.
/// </summary>
public delegate Boolean EqualityComparison<TComparable>(TComparable first,
                                                        TComparable second);

/// <summary>
/// Maps an object of type <typeparamref name="TOrigin"/> to an object of type <typeparamref name="TResult"/>
/// asynchronosly.
/// </summary>
/// <param name="origin">The object to map from.</param>
/// <param name="cancellationToken"></param>
/// <returns>The object that has been mapped to.</returns>
public delegate Task<TResult> AsyncOptionMapping<TOrigin, TResult>(TOrigin origin,
                                                                   CancellationToken cancellationToken = default);

/// <summary>
/// Maps an object of type <typeparamref name="TOrigin"/> to an <see cref="Option{T}"/> of type <typeparamref name="TResult"/>
/// asynchronosly.
/// </summary>
/// <param name="origin">The object to map from.</param>
/// <param name="cancellationToken"></param>
/// <returns>The <see cref="Option{T}"/> that has been mapped to.</returns>
public delegate Task<Option<TResult>> AsyncOptionDirectMapping<TOrigin, TResult>(TOrigin origin,
                                                                                 CancellationToken cancellationToken = default);

/// <summary>
/// Asynchronosly performs an action on the object passed to the delegate.
/// </summary>
/// <param name="origin">The object to perform the action on.</param>
/// <param name="cancellationToken"></param>
public delegate Task AsyncOptionInteraction<T>(T origin,
                                               CancellationToken cancellationToken = default);