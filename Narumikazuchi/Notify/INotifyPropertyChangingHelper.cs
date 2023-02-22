namespace Narumikazuchi;

/// <summary>
/// Implements a way for extensions methods to raise the <see cref="INotifyPropertyChanging.PropertyChanging"/> event.
/// </summary>
public interface INotifyPropertyChangingHelper :
    INotifyPropertyChanging
{
    /// <summary>
    /// Raises the <see cref="INotifyPropertyChanging.PropertyChanging"/> event for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property that is changing.</param>
    public void OnPropertyChanging(
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        [DisallowNull]
#endif
        NotNull<String> propertyName);
}