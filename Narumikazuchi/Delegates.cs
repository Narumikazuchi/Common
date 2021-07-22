using System;

namespace Narumikazuchi
{
    /// <summary>
    /// Represents the method that will handle an event without event data.
    /// </summary>
    /// <param name="sender">The object which raised the event.</param>
    /// <param name="e">The event data for the raised event.</param>
    public delegate void EventHandler<TSender>(TSender sender, EventArgs e);
    /// <summary>
    /// Represents the method that will handle an event with certain event data.
    /// </summary>
    /// <param name="sender">The object which raised the event.</param>
    /// <param name="e">The event data for the raised event.</param>
    public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e) where TEventArgs : EventArgs;
}
