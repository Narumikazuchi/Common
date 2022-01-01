﻿namespace Narumikazuchi;

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
    public String? File
    {
        get => this._filename;
        init => this._filename = value;
    }
    /// <summary>
    /// Gets the name of the declaring type.
    /// </summary>
    public String? Target
    {
        get => this._target;
        init => this._target = value;
    }
    /// <summary>
    /// Gets the name of the function.
    /// </summary>
    public String? Name
    {
        get => this._name;
        init => this._name = value;
    }
    /// <summary>
    /// Gets the line number in the file.
    /// </summary>
    public Int32 Line
    {
        get => this._line;
        init => this._line = value;
    }
    /// <summary>
    /// Gets the column in the line of the file.
    /// </summary>
    public Int32 Column
    {
        get => this._column;
        init => this._column = value;
    }
}

partial struct FunctionCallInformation
{
    internal FunctionCallInformation(StackFrame frame)
    {
        MethodBase? method = frame.GetMethod();
        this._filename = frame.GetFileName();
        this._target = method?.DeclaringType?.FullName;
        this._name = method?.Name;
        this._line = frame.GetFileLineNumber();
        this._column = frame.GetFileColumnNumber();
        if (method is null)
        {
            return;
        }
        foreach (ParameterInfo parameter in method.GetParameters())
        {
            this._parameters.Add(parameter.ParameterType.FullName);
        }
    }

    private readonly Int32 _line;
    private readonly Int32 _column;
    private readonly String? _filename;
    private readonly String? _target;
    private readonly String? _name;
    private readonly List<String?> _parameters = new();
}