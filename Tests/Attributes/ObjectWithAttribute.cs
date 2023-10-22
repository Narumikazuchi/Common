namespace Attributes;

[ClassOnceDummy]
[ClassMultipleDummy(1)]
[ClassMultipleDummy(2)]
public sealed class ObjectWithAttribute
{
    public void MethodWithAttributedParameter(
        [ParameterOnceDummy][ParameterMultipleDummy(1)][ParameterMultipleDummy(2)] Int32 _)
    { }
}