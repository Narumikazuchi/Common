#nullable disable
using BenchmarkDotNet.Attributes;
using Narumikazuchi;
using Narumikazuchi.Generators.TaggedUnions;

namespace Benchmarks;

[MemoryDiagnoser(false)]
public class Benchmark_AttributeResolver
{
    [Benchmark]
    public Boolean AssemblyHasAttribute()
    {
        return AttributeResolver.HasAttribute<UnionOfAttribute>(typeof(AttributeResolver).Assembly);
    }

    [Benchmark]
    public Boolean MemberInfoHasAttribute()
    {
        return AttributeResolver.HasAttribute<MemoryDiagnoserAttribute>(typeof(Benchmark_AttributeResolver));
    }

    [Benchmark]
    public Boolean ParameterHasAttribute()
    {
        MethodInfo method = typeof(AttributeResolver).GetMethod(nameof(AttributeResolver.HasAttribute));
        ParameterInfo parameter = method.GetParameters()[0];
        return AttributeResolver.HasAttribute<DisallowNullAttribute>(parameter);
    }

    [Benchmark]
    public ImmutableArray<UnionOfAttribute> AssemblyFetchAllAttributes()
    {
        return AttributeResolver.FetchAllAttributes<UnionOfAttribute>(typeof(AttributeResolver).Assembly);
    }

    [Benchmark]
    public ImmutableArray<MemoryDiagnoserAttribute> MemberInfoFetchAllAttributes()
    {
        return AttributeResolver.FetchAllAttributes<MemoryDiagnoserAttribute>(typeof(Benchmark_AttributeResolver));
    }

    [Benchmark]
    public ImmutableArray<DisallowNullAttribute> ParameterFetchAllAttributes()
    {
        MethodInfo method = typeof(AttributeResolver).GetMethod(nameof(AttributeResolver.HasAttribute));
        ParameterInfo parameter = method.GetParameters()[0];
        return AttributeResolver.FetchAllAttributes<DisallowNullAttribute>(parameter);
    }

    [Benchmark]
    public UnionOfAttribute AssemblyFetchSingleAttribute()
    {
        return AttributeResolver.FetchSingleAttribute<UnionOfAttribute>(typeof(AttributeResolver).Assembly);
    }

    [Benchmark]
    public MemoryDiagnoserAttribute MemberInfoFetchSingleAttribute()
    {
        return AttributeResolver.FetchSingleAttribute<MemoryDiagnoserAttribute>(typeof(Benchmark_AttributeResolver));
    }

    [Benchmark]
    public DisallowNullAttribute ParameterFetchSingleAttribute()
    {
        MethodInfo method = typeof(AttributeResolver).GetMethod(nameof(AttributeResolver.HasAttribute));
        ParameterInfo parameter = method.GetParameters()[0];
        return AttributeResolver.FetchSingleAttribute<DisallowNullAttribute>(parameter);
    }
}