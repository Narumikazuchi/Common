﻿namespace Narumikazuchi;

public partial struct AlphanumericVersion
{
    static private Int32 CompareComponent(String? me,
                                          String? other)
    {
        if (me is null)
        {
            if (other is null)
            {
                return 0;
            }
            return -1;
        }
        if (other is null)
        {
            return 1;
        }

        if (me.Length > other.Length)
        {
            return 1;
        }
        if (me.Length < other.Length)
        {
            return -1;
        }

        for (Int32 i = 0;
             i < me.Length;
             i++)
        {
            if (me[i] > other[i])
            {
                return 1;
            }
            if (me[i] < other[i])
            {
                return -1;
            }
        }
        return 0;
    }

    private AlphanumericVersion(AlphanumericVersion original)
    {
        m_Major = original.m_Major;
        m_Minor = original.m_Minor;
        m_Build = original.m_Build;
        m_Revision = original.m_Revision;
    }

    private static readonly Regex s_Regex = new(@"^[a-zA-Z0-9\-]*$");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String m_Major;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? m_Minor;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? m_Build;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly String? m_Revision;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SPECIFIER_NEEDS_TO_BE_ALPHANUMERIC = "The specifier for the version component needs to be alphanumeric.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String SEGMENT_COUNT_OUT_OF_BOUNDS = "The number of version components is out of the allowed bounds (min: 1, max: 4).";
}