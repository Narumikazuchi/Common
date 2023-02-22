namespace Narumikazuchi;

/// <summary>
/// Represents the method that will handle an event without event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
public delegate void EventHandler<TSender>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    MaybeNull<TSender> sender);
/// <summary>
/// Represents the method that will handle an event with certain event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void EventHandler<TSender, TEventArgs>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    MaybeNull<TSender> sender,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    MaybeNull<TEventArgs> eventArgs) 
        where TEventArgs : EventArgs;

/// <summary>
/// Represents the method to compare two instances of the same type for equality.
/// </summary>
public delegate Boolean EqualityComparison<TComparable>(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
# endif
    MaybeNull<TComparable> first,
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [AllowNull]
#endif
    MaybeNull<TComparable> second);