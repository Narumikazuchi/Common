namespace Narumikazuchi;

public partial struct AlphanumericVersion
{
#if NET7_0_OR_GREATER
    [GeneratedRegex("^[a-zA-Z0-9\\-]*$")]
    static private partial Regex VersionRegex();

    static private readonly Regex s_Regex = VersionRegex();
#else
    static private readonly Regex s_Regex = new(@"^[a-zA-Z0-9\-]*$");
#endif

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC = "The specifier for the version component needs to be alphanumeric.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SEGMENT_COUNT_OUT_OF_BOUNDS = "The number of version components is out of the allowed bounds (min: 1, max: 4).";

    private AlphanumericVersion(AlphanumericVersion original)
    {
        m_Value = original.m_Value;
        m_Minor = original.m_Minor;
        m_Build = original.m_Build;
        m_Revision = original.m_Revision;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String m_Value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int32 m_Minor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int32 m_Build;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int32 m_Revision;
}