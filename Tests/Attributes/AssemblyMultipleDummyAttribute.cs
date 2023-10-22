namespace Attributes;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class AssemblyMultipleDummyAttribute : Attribute
{
    public AssemblyMultipleDummyAttribute(Int32 id)
    {
        this.Id = id;
    }

    public Int32 Id { get; init; }
}