namespace Narumikazuchi;

/// <summary>
/// Contains detailed information of an <see cref="Exception"/>.
/// </summary>
public readonly partial struct ExceptionInformation
{
    /// <summary>
    /// Gets the environment data from when the <see cref="Exception"/> has been thrown.
    /// </summary>
    public IDictionary Data => this._source.Data;
    /// <summary>
    /// Gets the inner <see cref="Exception"/>.
    /// </summary>
    public Exception? InnerException => this._source.InnerException;
    /// <summary>
    /// Gets the callstack from when the <see cref="Exception"/> occured.
    /// </summary>
    public IReadOnlyList<FunctionCallInformation> CallStack
    {
        get
        {
            if (this._callStack.Count == 0)
            {
                this.ParseStackTrace();
            }
            return this._callStack;
        }
    }
    /// <summary>
    /// Gets the type in which the <see cref="Exception"/> occured.
    /// </summary>
    public Type? SourceType => this._source.TargetSite?.DeclaringType;
    /// <summary>
    /// Gets the library where the <see cref="SourceType"/> is declared.
    /// </summary>
    public String? SourceLibrary => this._source.TargetSite?.Module.FullyQualifiedName;
    /// <summary>
    /// Gets the member who caused the <see cref="Exception"/>.
    /// </summary>
    public String? SourceMember => this._source.TargetSite?.ToString();
    /// <summary>
    /// Gets the member type of the <see cref="SourceMember"/>.
    /// </summary>
    public String? SourceMemberType => this._source.TargetSite?.MemberType.ToString();
}

partial struct ExceptionInformation
{
    internal ExceptionInformation(Exception source) =>
        this._source = source;

    private void ParseStackTrace()
    {
        StackTrace st = new(this._source,
                            true);
        foreach (StackFrame frame in st.GetFrames())
        {
            this._callStack.Add(new(frame));
        }
    }

    private readonly List<FunctionCallInformation> _callStack = new();
    private readonly Exception _source;
}