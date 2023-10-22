namespace Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ClassMultipleDummyAttribute : Attribute
{
    public ClassMultipleDummyAttribute(Int32 id)
    {
        this.Id = id;
    }

    public Int32 Id { get; init; }
}