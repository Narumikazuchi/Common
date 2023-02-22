#if NET5_0_OR_GREATER
namespace Narumikazuchi;

/// <summary>
/// Contains the information for a function call on the call stack.
/// </summary>
public readonly partial struct FunctionCallInformation
{
    /// <inheritdoc/>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public readonly override String ToString()
    {
        StringBuilder builder = new();
        builder.Append(value: this.Target);
        builder.Append(value: '.');
        builder.Append(value: this.Name);
        builder.Append(value: '(');
        builder.Append(value: String.Join(separator: ',',
                                          values: m_Parameters));
        builder.Append(value: ')');
        if (this.File.IsNull ||
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
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [MaybeNull]
#endif
    public MaybeNull<String> File
    {
        get
        {
            return m_Filename;
        }
        init
        {
            m_Filename = value;
        }
    }

    /// <summary>
    /// Gets the name of the declaring type.
    /// </summary>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [MaybeNull]
#endif
    public MaybeNull<String> Target
    {
        get
        {
            return m_Target;
        }
        init
        {
            m_Target = value;
        }
    }

    /// <summary>
    /// Gets the name of the function.
    /// </summary>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [MaybeNull]
#endif
    public MaybeNull<String> Name
    {
        get
        {
            return m_Name;
        }
        init
        {
            m_Name = value;
        }
    }

    /// <summary>
    /// Gets the line number in the file.
    /// </summary>
    public Int32 Line
    {
        get
        {
            return m_Line;
        }
        init
        {
            m_Line = value;
        }
    }

    /// <summary>
    /// Gets the column in the line of the file.
    /// </summary>
    public Int32 Column
    {
        get
        {
            return m_Column;
        }
        init
        {
            m_Column = value;
        }
    }

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

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int32 m_Line;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int32 m_Column;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly MaybeNull<String> m_Filename;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly MaybeNull<String> m_Target;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly MaybeNull<String> m_Name;
    private readonly List<String?> m_Parameters = new();
}
#endif