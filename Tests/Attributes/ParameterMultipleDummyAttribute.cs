namespace Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public sealed class ParameterMultipleDummyAttribute : Attribute
{
    public ParameterMultipleDummyAttribute(Int32 id)
    {
        this.Id = id;
    }

    public Int32 Id { get; init; }
}