﻿namespace Narumikazuchi;

/// <summary>
/// Represents the method that will handle an event without event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void EventHandler<TSender>([DisallowNull] TSender sender, 
                                           [AllowNull] EventArgs? eventArgs);
/// <summary>
/// Represents the method that will handle an event with certain event data.
/// </summary>
/// <param name="sender">The object which raised the event.</param>
/// <param name="eventArgs">The event data for the raised event.</param>
public delegate void EventHandler<TSender, TEventArgs>([DisallowNull] TSender sender,
                                                       [AllowNull] TEventArgs? eventArgs) 
    where TEventArgs : EventArgs;

/// <summary>
/// Represents the method to compare two instances of the same type for equality.
/// </summary>
public delegate Boolean EqualityComparison<T>(T first, 
                                              T second);