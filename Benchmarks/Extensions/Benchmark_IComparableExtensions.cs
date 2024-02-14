#nullable disable
using BenchmarkDotNet.Attributes;
using Narumikazuchi;

namespace Benchmarks;

[MemoryDiagnoser(false)]
public class Benchmark_IComparableExtensions
{
    [Benchmark]
    public Int32 ClampInteger()
    {
        return 42.Clamp(0, 69);
    }

    [Benchmark]
    public String ClampString()
    {
        return "foobar".Clamp("foo", "bar");
    }

    [Benchmark]
    public void ThrowIfLesserThan()
    {
        42.ThrowIfLesserThan(0);
    }

    [Benchmark]
    public void ThrowIfBiggerThan()
    {
        42.ThrowIfBiggerThan(69);
    }

    [Benchmark]
    public void ThrowIfOutOfRange()
    {
        42.ThrowIfOutOfRange(0, 69);
    }
}