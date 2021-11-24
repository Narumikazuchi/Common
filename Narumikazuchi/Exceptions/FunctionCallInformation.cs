namespace Narumikazuchi;

/// <summary>
/// Contains the information for a function call on the call stack.
/// </summary>
public readonly partial struct FunctionCallInformation
{
    /// <inheritdoc/>
    public override String ToString()
    {
        StringBuilder builder = new();
        builder.Append(value: this.Target);
        builder.Append(value: '.');
        builder.Append(value: this.Name);
        builder.Append(value: '(');
        builder.Append(value: String.Join(separator: ',',
                                          values: this._parameters));
        builder.Append(value: ')');
        if (this.File is null ||
            this.Line == 0)
        {
            return builder.ToString();
        }
        builder.Append(value: '\n');
        builder.Append(value: "at ");
        builder.Append(value: this.File);
        builder.Append(value: " line ");
        builder.Append(value: this.Line);
        builder.Append(value: ':');
        builder.Append(value: this.Column);
        return builder.ToString();
    }

    /// <summary>
    /// Gets the path to the file, where the function is defined.
    /// </summary>
    public String? File { get; init; }
    /// <summary>
    /// Gets the name of the declaring type.
    /// </summary>
    public String? Target { get; init; }
    /// <summary>
    /// Gets the name of the function.
    /// </summary>
    public String? Name { get; init; }
    /// <summary>
    /// Gets the line number in the file.
    /// </summary>
    public Int32 Line { get; init; }
    /// <summary>
    /// Gets the column in the line of the file.
    /// </summary>
    public Int32 Column { get; init; }
}

partial struct FunctionCallInformation
{
    internal FunctionCallInformation(StackFrame frame)
    {
        MethodBase? method = frame.GetMethod();
        this.File = frame.GetFileName();
        this.Target = method?.DeclaringType?.FullName;
        this.Name = method?.Name;
        this.Line = frame.GetFileLineNumber();
        this.Column = frame.GetFileColumnNumber();
        if (method is null)
        {
            return;
        }
        foreach (ParameterInfo parameter in method.GetParameters())
        {
            this._parameters.Add(item: parameter.ParameterType.FullName);
        }
    }

    private readonly List<String?> _parameters = new();
}