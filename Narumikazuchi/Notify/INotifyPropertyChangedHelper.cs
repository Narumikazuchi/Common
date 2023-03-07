using System.Diagnostics.CodeAnalysis;

namespace Narumikazuchi;

/// <summary>
/// Implements a way for extensions methods to raise the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
/// </summary>
public interface INotifyPropertyChangedHelper :
    INotifyPropertyChanged
{
    /// <summary>
    /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    public void OnPropertyChanged([DisallowNull] String propertyName);
}