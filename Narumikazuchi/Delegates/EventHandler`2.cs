namespace Narumikazuchi;

/// <summary>
/// Represents the method that will handle an event with certain event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void EventHandler<TSender, TEventArgs>([DisallowNull] TSender sender,
                                                       TEventArgs eventArgs)
    where TEventArgs : EventArgs;