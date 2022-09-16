namespace Narumikazuchi;

#if NET5_0_OR_GREATER
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
                                          values: m_Parameters));
        builder.Append(value: ')');
        if (!this.File.HasValue ||
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
    public Option<String> File
    {
        get => m_Filename;
        init => m_Filename = value;
    }

    /// <summary>
    /// Gets the name of the declaring type.
    /// </summary>
    public Option<String> Target
    {
        get => m_Target;
        init => m_Target = value;
    }

    /// <summary>
    /// Gets the name of the function.
    /// </summary>
    public Option<String> Name
    {
        get => m_Name;
        init => m_Name = value;
    }

    /// <summary>
    /// Gets the line number in the file.
    /// </summary>
    public Int32 Line
    {
        get => m_Line;
        init => m_Line = value;
    }

    /// <summary>
    /// Gets the column in the line of the file.
    /// </summary>
    public Int32 Column
    {
        get => m_Column;
        init => m_Column = value;
    }
}

partial struct FunctionCallInformation
{
    internal FunctionCallInformation(StackFrame frame)
    {
        MethodBase? method = frame.GetMethod();
        m_Filename = frame.GetFileName();
        m_Target = method?.DeclaringType?.FullName;
        m_Name = method?.Name;
        m_Line = frame.GetFileLineNumber();
        m_Column = frame.GetFileColumnNumber();
        if (method is null)
        {
            return;
        }
        foreach (ParameterInfo parameter in method.GetParameters())
        {
            m_Parameters.Add(parameter.ParameterType.FullName);
        }
    }

    private readonly Int32 m_Line;
    private readonly Int32 m_Column;
    private readonly Option<String> m_Filename;
    private readonly Option<String> m_Target;
    private readonly Option<String> m_Name;
    private readonly List<String?> m_Parameters = new();
}
#endif