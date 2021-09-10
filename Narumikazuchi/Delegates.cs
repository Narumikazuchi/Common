using System;
using System.Diagnostics.CodeAnalysis;

namespace Narumikazuchi
{
    /// <summary>
    /// Represents the method that will handle an event without event data.
    /// </summary>
    /// <param name="sender">The object which raised the event.</param>
    /// <param name="e">The event data for the raised event.</param>
    public delegate void EventHandler<TSender>([DisallowNull] TSender sender, 
                                               [MaybeNull] EventArgs? e);
    /// <summary>
    /// Represents the method that will handle an event with certain event data.
    /// </summary>
    /// <param name="sender">The object which raised the event.</param>
    /// <param name="e">The event data for the raised event.</param>
    public delegate void EventHandler<TSender, TEventArgs>([DisallowNull] TSender sender,
                                                           [MaybeNull] TEventArgs? e) 
        where TEventArgs : EventArgs;

    /// <summary>
    /// Represents the method to compare two instances of the same type for equality.
    /// </summary>
    public delegate Boolean EqualityComparison<T>(T first, 
                                                  T second);
}
