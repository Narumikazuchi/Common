namespace Narumikazuchi;

/// <summary>
/// Represents the method that will handle an event without event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
public delegate void EventHandler<TSender>([DisallowNull] TSender sender);