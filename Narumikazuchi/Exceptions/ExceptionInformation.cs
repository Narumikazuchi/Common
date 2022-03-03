namespace Narumikazuchi;

/// <summary>
/// Contains detailed information of an <see cref="Exception"/>.
/// </summary>
public readonly partial struct ExceptionInformation
{
    /// <summary>
    /// Gets the environment data from when the <see cref="Exception"/> has been thrown.
    /// </summary>
    public IDictionary Data => 
        m_Source.Data;
    /// <summary>
    /// Gets the inner <see cref="Exception"/>.
    /// </summary>
    public Exception? InnerException => 
        m_Source.InnerException;
    /// <summary>
    /// Gets the callstack from when the <see cref="Exception"/> occured.
    /// </summary>
    public IReadOnlyList<FunctionCallInformation> CallStack
    {
        get
        {
            if (m_CallStack.Count == 0)
            {
                this.ParseStackTrace();
            }
            return m_CallStack;
        }
    }
    /// <summary>
    /// Gets the type in which the <see cref="Exception"/> occured.
    /// </summary>
    public Type? SourceType => 
        m_Source.TargetSite?
                .DeclaringType;
    /// <summary>
    /// Gets the library where the <see cref="SourceType"/> is declared.
    /// </summary>
    public String? SourceLibrary => 
        m_Source.TargetSite?
                .Module
                .FullyQualifiedName;
    /// <summary>
    /// Gets the member who caused the <see cref="Exception"/>.
    /// </summary>
    public String? SourceMember => 
        m_Source.TargetSite?
                .ToString();
    /// <summary>
    /// Gets the member type of the <see cref="SourceMember"/>.
    /// </summary>
    public String? SourceMemberType => 
        m_Source.TargetSite?
                .MemberType
                .ToString();
}

partial struct ExceptionInformation
{
    internal ExceptionInformation(Exception source)
    {
        m_Source = source;
    }

    private void ParseStackTrace()
    {
        StackTrace st = new(e: m_Source,
                            fNeedFileInfo: true);
        foreach (StackFrame frame in st.GetFrames())
        {
            m_CallStack.Add(new(frame: frame));
        }
    }

    private readonly List<FunctionCallInformation> m_CallStack = new();
    private readonly Exception m_Source;
}