namespace Narumikazuchi;

/// <summary>
/// Provides methods to generate primes or check for them.
/// </summary>
public static partial class Primes
{
    /// <summary>
    /// Checks whether the specified candidate is a prime number.
    /// </summary>
    /// <param name="candidate">The candidate to check.</param>
    /// <returns><see langword="true"/> if the specified candidate is a prime; otherwise <see langword="false"/></returns>
    public static Boolean IsPrime(in Int64 candidate)
    {
        if (candidate < 0)
        {
            return false;
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        if (candidate <= Known[^1])
#else
        if (candidate <= Known.Last())
#endif
        {
            Int32 index = Known.BinarySearch(item: candidate);
            if (index > -1)
            {
                return true;
            }
        }

        if ((candidate & 1) != 0)
        {
            Int64 max = (Int64)Math.Sqrt(candidate);
            for (Int64 i = 3; 
                 i <= max; 
                 i += 2)
            {
                if (candidate % i == 0)
                {
                    return false;
                }
            }
            Known.Add(candidate);
            Known.Sort();
            return true;
        }
        return candidate == 2;
    }

    /// <summary>
    /// Returns the nearest prime that is smaller than or equal to the specified parameter.
    /// </summary>
    /// <param name="origin">The origin from where to start.</param>
    /// <returns>The nearest prime that is smaller than or equal to the specified parameter</returns>
    /// <exception cref="ArgumentException"/>
    public static Int64 GetPrevious(in Int64 origin)
    {
        if (origin < 0)
        {
            ArgumentException exception = new(message: NEGATIVE_NUMBERS_NOT_SUPPORTED);
            exception.Data.Add(key: "origin",
                               value: origin);
            throw exception;
        }

        Int32 index;

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        if (origin <= Known[^1])
        {
            index = ApproachInRange(value: origin,
                                    range: 0..(Known.Count - 1),
                                    reverse: true);
            return Known[index];
        }

        Int64 next = Known[^1];
        while (next < origin)
        {
            next = FindNextPrime();
        }

        index = ApproachInRange(value: origin,
                                range: 0..(Known.Count - 1),
                                reverse: true);
#else
        if (origin <= Known.Last())
        {
            index = ApproachInRange(value: origin,
                                    start: 0,
                                    end: Known.Count - 1,
                                    reverse: true);
            return Known[index];
        }

        Int64 next = Known.Last();
        while (next < origin)
        {
            next = FindNextPrime();
        }

        index = ApproachInRange(value: origin,
                                start: 0,
                                end: Known.Count - 1,
                                reverse: true);
#endif

        return Known[index];
    }

    /// <summary>
    /// Returns the nearest prime that is greater than or equal to the specified parameter.
    /// </summary>
    /// <param name="origin">The origin from where to start.</param>
    /// <returns>The nearest prime that is greater than or equal to the specified parameter</returns>
    /// <exception cref="ArgumentException"/>
    public static Int64 GetNext(in Int64 origin)
    {
        if (origin < 0)
        {
            ArgumentException exception = new(message: NEGATIVE_NUMBERS_NOT_SUPPORTED);
            exception.Data.Add(key: "origin",
                               value: origin);
            throw exception;
        }

        Int32 index;

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        if (origin <= Known[^1])
        {
            index = ApproachInRange(value: origin,
                                    range: 0..(Known.Count - 1),
                                    reverse: false);
            return Known[index];
        }

        Int64 next = Known[^1];
        while (next < origin)
        {
            next = FindNextPrime();
        }

        index = ApproachInRange(value: origin,
                                range: 0..(Known.Count - 1),
                                reverse: false);
#else
        if (origin <= Known.Last())
        {
            index = ApproachInRange(value: origin,
                                    start: 0,
                                    end: Known.Count - 1,
                                    reverse: false);
            return Known[index];
        }

        Int64 next = Known.Last();
        while (next < origin)
        {
            next = FindNextPrime();
        }

        index = ApproachInRange(value: origin,
                                start: 0,
                                end: Known.Count - 1,
                                reverse: false);
#endif

        return Known[index];
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// Enumerates all primes in the specified range.
    /// </summary>
    /// <returns>The list of primes contained in the specified range</returns>
    /// <exception cref="ArgumentException"/>
    public static PrimeEnumerator ListUntil(in Range range) =>
        ListUntil(startPoint: range.Start.Value, 
                  endPoint: range.End.Value);
#endif
    /// <summary>
    /// Enumerates all primes in the range specified by the specified starting point to the specified end point.
    /// </summary>
    /// <returns>The list of primes contained in the specified range</returns>
    /// <exception cref="ArgumentException"/>
    public static PrimeEnumerator ListUntil(Int64 startPoint,
                                            Int64 endPoint)
    {
        if (startPoint < 0)
        {
            ArgumentException exception = new(message: NEGATIVE_NUMBERS_NOT_SUPPORTED);
            exception.Data.Add(key: "startPoint",
                               value: startPoint);
            throw exception;
        }
        if (endPoint < 0)
        {
            ArgumentException exception = new(message: NEGATIVE_NUMBERS_NOT_SUPPORTED);
            exception.Data.Add(key: "endPoint",
                               value: endPoint);
            throw exception;
        }

        if (startPoint > endPoint)
        {
#if NET47_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NETCOREAPP2_0_OR_GREATER
            (startPoint, endPoint) = (endPoint, startPoint);
#else
            Int64 temp = startPoint;
            startPoint = endPoint;
            endPoint = temp;
#endif
        }

        return new(startPoint: startPoint,
                   endPoint: endPoint);
    }
}

// Non-Public
partial class Primes
{
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    private static Int32 ApproachInRange(in Int64 value,
                                         in Range range,
                                         in Boolean reverse)
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
    private static Int32 ApproachInRange(in Int64 value,
                                         in Int32 start,
                                         in Int32 end,
                                         in Boolean reverse)
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

    internal static Int64 FindNextPrime()
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