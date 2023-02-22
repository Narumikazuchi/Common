using Narumikazuchi.Collections;

namespace Narumikazuchi;

/// <summary>
/// Provides methods to generate primes or check for them.
/// </summary>
static public partial class Primes
{
    /// <summary>
    /// Checks whether the specified candidate is a prime number.
    /// </summary>
    /// <param name="candidate">The candidate to check.</param>
    /// <returns><see langword="true"/> if the specified candidate is a prime; otherwise <see langword="false"/></returns>
    static public Boolean IsPrime(Int64 candidate)
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
            Int32 index = Known.BinarySearch(candidate);
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
    static public Int64 GetPrevious(Int64 origin)
    {
        if (origin < 0)
        {
            ArgumentException exception = new(NEGATIVE_NUMBERS_NOT_SUPPORTED);
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
    static public Int64 GetNext(Int64 origin)
    {
        if (origin < 0)
        {
            ArgumentException exception = new(NEGATIVE_NUMBERS_NOT_SUPPORTED);
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
    static public PrimeEnumerator ListUntil(Range range)
    {
        return ListUntil(startPoint: range.Start.Value,
                         endPoint: range.End.Value);
    }
#endif
    /// <summary>
    /// Enumerates all primes in the range specified by the specified starting point to the specified end point.
    /// </summary>
    /// <returns>The list of primes contained in the specified range</returns>
    /// <exception cref="ArgumentException"/>
    static public PrimeEnumerator ListUntil(Int64 startPoint,
                                            Int64 endPoint)
    {
        if (startPoint < 0)
        {
            ArgumentException exception = new(NEGATIVE_NUMBERS_NOT_SUPPORTED);
            exception.Data.Add(key: "startPoint",
                               value: startPoint);
            throw exception;
        }
        if (endPoint < 0)
        {
            ArgumentException exception = new(NEGATIVE_NUMBERS_NOT_SUPPORTED);
            exception.Data.Add(key: "endPoint",
                               value: endPoint);
            throw exception;
        }

        if (startPoint > endPoint)
        {
#if NET48_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NETCOREAPP2_0_OR_GREATER
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