namespace Narumikazuchi;

public partial class Primes
{
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    static private Int32 ApproachInRange(Int64 value,
                                         Range range,
                                         Boolean reverse)
    {
        Int32 length = range.End.Value - range.Start.Value;
        if (length == 1)
        {
            if (reverse)
            {
                return range.Start.Value;
            }
            return range.End.Value;
        }
        length = (length + 1) / 2;
        if (value > Known[range.Start] &&
            value <= Known[range.Start.Value + length])
        {
            return ApproachInRange(value: value,
                                   range: range.Start.Value..(range.Start.Value + length),
                                   reverse: reverse);
        }
        if (value > Known[range.Start.Value + length] &&
            value <= Known[range.End])
        {
            return ApproachInRange(value: value,
                                   range: (range.Start.Value + length)..range.End,
                                   reverse: reverse);
        }
        if (reverse)
        {
            return range.End.Value;
        }
        return range.Start.Value;
    }
#else
    static private Int32 ApproachInRange(Int64 value,
                                         Int32 start,
                                         Int32 end,
                                         Boolean reverse)
    {
        Int32 length = start - end;
        if (length == 1)
        {
            if (reverse)
            {
                return start;
            }
            return end;
        }
        length = (length + 1) / 2;
        if (value > Known[start] &&
            value <= Known[start + length])
        {
            return ApproachInRange(value: value,
                                   start: start,
                                   end: start + length,
                                   reverse: reverse);
        }
        if (value > Known[start + length] &&
            value <= Known[end])
        {
            return ApproachInRange(value: value,
                                   start: start + length,
                                   end: end,
                                   reverse: reverse);
        }
        if (reverse)
        {
            return end;
        }
        return start;
    }
#endif

    static internal Int64 FindNextPrime()
    {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        Int64 last = Known[^1];
#else
        Int64 last = Known.Last();
#endif
        do
        {
            last += 2;
        } while (!IsPrime(last));

        return last;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal static List<Int64> Known { get; } = new()
    {
        2,
        3,
        5,
        7,
        11,
        13,
        17,
        19,
        23,
        27,
        29,
        31,
        37,
        41,
        43,
        47,
        53,
        59,
        61,
        67,
        71,
        73,
        79,
        83,
        89,
        97,
        101,
        103,
        107,
        109,
        113,
        127,
        131,
        137,
        139,
        149,
        151,
        157,
        163,
        167,
        173,
        179,
        181,
        191,
        193,
        197,
        199,
        211,
        223,
        227,
        229,
        233,
        239,
        241,
        251,
        257,
        263,
        269,
        271,
        277,
        281,
        283,
        293,
        307,
        311,
        313,
        317,
        331,
        337,
        347,
        349,
        353,
        359,
        367,
        373,
        379,
        383,
        389,
        397,
        401,
        409,
        419,
        421,
        431,
        433,
        439,
        443,
        449,
        457,
        461,
        463,
        467,
        479,
        487,
        491,
        499,
        503,
        509,
        521,
        523,
        541,
        547,
        557,
        563,
        569,
        571,
        577,
        587,
        593,
        599,
        601,
        607,
        613,
        617,
        619,
        631,
        641,
        643,
        647,
        653,
        659,
        661,
        673,
        677,
        683,
        691,
        701,
        709,
        719,
        727,
        733,
        739,
        743,
        751,
        757,
        761,
        769,
        773,
        787,
        797,
        809,
        811,
        821,
        823,
        827,
        829,
        839,
        853,
        857,
        859,
        863,
        877,
        881,
        883,
        887,
        907,
        911,
        919,
        929,
        937,
        941,
        947,
        953,
        967,
        971,
        977,
        983,
        991,
        997
    };

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NEGATIVE_NUMBERS_NOT_SUPPORTED = "The prime finding for negative numbers is not supported.";
}