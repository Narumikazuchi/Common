#nullable disable
using BenchmarkDotNet.Attributes;
using Narumikazuchi;

namespace Benchmarks;

[MemoryDiagnoser(false)]
public class Benchmark_StringExtensions
{
    [Params("NoInvalidCharacters", "HasInvalid\v\nPathCharacters", "Has:nvalidF:lenameCharacters")]
    public String Value
    {
        get;
        set;
    }

    [Benchmark]
    public String Sanitize()
    {
        return this.Value.SanitizeForFilename();
    }

    [Benchmark]
    public void ThrowArgumentNullExceptionIfNull()
    {
        this.Value.ThrowIfNullOrEmpty(asArgumentException: true);
    }

    [Benchmark]
    public void ThrowNullReferenceExceptionIfNull()
    {
        this.Value.ThrowIfNullOrEmpty();
    }

    [Benchmark]
    public void ThrowArgumentNullExceptionIfEmpty()
    {
        this.Value.ThrowIfNullOrEmpty(asArgumentException: true);
    }

    [Benchmark]
    public void ThrowNullReferenceExceptionIfEmpty()
    {
        this.Value.ThrowIfNullOrEmpty();
    }
}