namespace Narumikazuchi;

/// <summary>
/// Constraints a type parameter for an <see langword="interface"/> to only allow the implementing type.
/// </summary>
/// <remarks>
/// Since there is no <see langword="self"/> or anything equivalent in C#, we usually do something like this:
/// <code>public interface IExample&lt;TSelf&gt; where TSelf : IExample&lt;TSelf&gt;</code> While this is 
/// already good enough for most people, you can still assign a class as type argument, that comes from 
/// anywhere else in the inheritance chain or even from a different inheritance chain. Using this attribute
/// fully constraints this behavior and guarantees, that the implementing type will always be assigned to
/// the <i>TSelf</i> type parameter. Basically enforcing the following behavior.
/// <code>public interface IExample&lt;[ConstrainToSelf] TSelf&gt; ...
/// public class Example : IExample&lt;Example&gt; &lt;--- Type Argument can only be Example and nothing else</code>
/// </remarks>
[AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = true)]
public sealed class ConstrainToImplementationAttribute : Attribute
{ }