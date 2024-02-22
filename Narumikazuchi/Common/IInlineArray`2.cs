#if NET8_0_OR_GREATER
namespace Narumikazuchi;

/// <summary>
/// Identifies a struct as an inline array for use with generics.
/// </summary>
public interface IInlineArray<[ConstrainToImplementation] TSelf, TElement>
    where TSelf : struct, IInlineArray<TSelf, TElement>
{
    internal Int32 FixedLength
    {
        get
        {
            return s_Length.Value;
        }
    }

    static private readonly Lazy<Int32> s_Length = new(valueFactory: () => AttributeResolver.FetchSingleAttribute<InlineArrayAttribute>(info: typeof(TSelf)).Length,
                                                       mode: LazyThreadSafetyMode.ExecutionAndPublication);
}
#endif