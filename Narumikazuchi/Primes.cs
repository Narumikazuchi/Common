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
    public static Boolean IsPrime(Int32 candidate)
    {
        if (candidate < 0)
        {
            return false;
        }

        if (candidate <= _known[^1])
        {
            Int32 index = _known.BinarySearch(candidate);
            if (index > -1)
            {
                return true;
            }
        }

        if ((candidate & 1) != 0)
        {
            Int32 max = (Int32)Math.Sqrt(candidate);
            for (Int32 i = 3; i <= max; i += 2)
            {
                if (candidate % i == 0)
                {
                    return false;
                }
            }
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
    public static Int32 GetPrevious(Int32 origin)
    {
        if (origin < 0)
        {
            ArgumentException ex = new(message: NEGATIVE_NUMBERS_NOT_SUPPORTED);
            ex.Data.Add(key: "origin",
                        value: origin);
            throw ex;
        }

        Int32 index;
        if (origin <= _known[^1])
        {
            index = ApproachInRange(origin,
                                    0..(_known.Count - 1),
                                    true);
            return _known[index];
        }

        FillMissingPrimes(origin);

        index = ApproachInRange(origin,
                                0..(_known.Count - 1),
                                true);
        return _known[index];
    }

    /// <summary>
    /// Returns the nearest prime that is greater than or equal to the specified parameter.
    /// </summary>
    /// <param name="origin">The origin from where to start.</param>
    /// <returns>The nearest prime that is greater than or equal to the specified parameter</returns>
    /// <exception cref="ArgumentException"/>
    public static Int32 GetNext(Int32 origin)
    {
        if (origin < 0)
        {
            ArgumentException ex = new(message: NEGATIVE_NUMBERS_NOT_SUPPORTED);
            ex.Data.Add(key: "origin",
                        value: origin);
            throw ex;
        }

        Int32 index;
        if (origin <= _known[^1])
        {
            index = ApproachInRange(origin,
                                    0..(_known.Count - 1),
                                    false);
            return _known[index];
        }


        for (Int32 i = (origin | 1); i < Int32.MaxValue; i += 2)
        {
            if (IsPrime(i))
            {
                FillMissingPrimes(i);
            }
        }

        index = ApproachInRange(origin,
                                0..(_known.Count - 1),
                                false);
        return _known[index];
    }

    /// <summary>
    /// Enumerates all primes in the specified range.
    /// </summary>
    /// <returns>The list of primes contained in the specified range</returns>
    /// <exception cref="ArgumentException"/>
    public static IEnumerable<Int32> ListUntil(Range range) =>
        ListUntil(range.Start.Value, 
                  range.End.Value);
    /// <summary>
    /// Enumerates all primes in the range specified by the specified starting point to the specified end point.
    /// </summary>
    /// <returns>The list of primes contained in the specified range</returns>
    /// <exception cref="ArgumentException"/>
    public static IEnumerable<Int32> ListUntil(Int32 startPoint,
                                               Int32 endPoint)
    {
        if (startPoint > endPoint)
        {
            (startPoint, endPoint) = (endPoint, startPoint);
        }

        if (endPoint > _known[^1])
        {
            FillMissingPrimes(endPoint | 1);
        }

        for (Int32 i = 0; i < _known.Count; i++)
        {
            if (_known[i] < startPoint)
            {
                continue;
            }
            if (endPoint < _known[i])
            {
                yield break;
            }
            yield return _known[i];
        }
        yield break;
    }
}

// Non-Public
partial class Primes
{
    private static void FillMissingPrimes(Int32 upperBound)
    {
        for (Int32 i = upperBound; i < Int32.MaxValue; i += 2)
        {
            if (IsPrime(i))
            {
                _known.Add(i);
                break;
            }
        }
        for (Int32 i = upperBound; i > _known[^1]; i -= 2)
        {
            if (IsPrime(i))
            {
                _known.Add(i);
            }
        }
        _known.Sort();
    }

    private static Int32 ApproachInRange(Int32 value,
                                         Range range,
                                         Boolean reverse)
    {
        Int32 length = range.End.Value - range.Start.Value;
        if (length == 1)
        {
            return reverse 
                ? range.Start.Value
                : range.End.Value;
        }
        length = (length + 1) / 2;
        if (value > _known[range.Start] &&
            value <= _known[range.Start.Value + length])
        {
            return ApproachInRange(value,
                                   range.Start.Value..(range.Start.Value + length),
                                   reverse);
        }
        if (value > _known[range.Start.Value + length] &&
            value <= _known[range.End])
        {
            return ApproachInRange(value,
                                   (range.Start.Value + length)..range.End,
                                   reverse);
        }
        return reverse
            ? range.End.Value
            : range.Start.Value;
    }

    private static readonly List<Int32> _known = new()
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

#pragma warning disable
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NEGATIVE_NUMBERS_NOT_SUPPORTED = "The prime finding for negative numbers is not supported.";
#pragma warning restore
}