﻿namespace Narumikazuchi;

#if NET5_0_OR_GREATER
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
    public Option<Exception> InnerException => 
        m_Source.InnerException;

    /// <summary>
    /// Gets the callstack from when the <see cref="Exception"/> occured.
    /// </summary>
    public ImmutableArray<FunctionCallInformation> CallStack =>
        m_CallStack;

    /// <summary>
    /// Gets the type in which the <see cref="Exception"/> occured.
    /// </summary>
    public Option<Type> SourceType => 
        m_Source.TargetSite?.DeclaringType;

    /// <summary>
    /// Gets the library where the <see cref="SourceType"/> is declared.
    /// </summary>
    public Option<String> SourceLibrary => 
        m_Source.TargetSite?.Module.FullyQualifiedName;

    /// <summary>
    /// Gets the member who caused the <see cref="Exception"/>.
    /// </summary>
    public Option<String> SourceMember => 
        m_Source.TargetSite?.ToString();

    /// <summary>
    /// Gets the member type of the <see cref="SourceMember"/>.
    /// </summary>
    public Option<String> SourceMemberType => 
        m_Source.TargetSite?.MemberType.ToString();
}

partial struct ExceptionInformation
{
    internal ExceptionInformation(Exception source)
    {
        m_Source = source;

        List<FunctionCallInformation> infos = new();
        StackTrace st = new(e: m_Source,
                            fNeedFileInfo: true);
        foreach (StackFrame frame in st.GetFrames())
        {
            infos.Add(new(frame: frame));
        }

        m_CallStack = infos.ToImmutableArray();
    }

    private readonly ImmutableArray<FunctionCallInformation> m_CallStack;
    private readonly Exception m_Source;
}
#endif