namespace Narumikazuchi;

/// <summary>
/// Represents the method that will handle an event with certain event data.
/// </summary>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void StaticEventHandler<TEventArgs>(TEventArgs eventArgs)
    where TEventArgs : EventArgs;