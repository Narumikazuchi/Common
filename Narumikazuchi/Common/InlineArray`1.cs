namespace Narumikazuchi;

/// <summary>
/// Provides helper method for inline arrays.
/// </summary>
static public class InlineArray<TElement>
{
    /// <summary>
    /// Returns a <see cref="Span{T}"/> representing the <paramref name="inlineArray"/>.
    /// </summary>
    /// <param name="inlineArray">The array to represent as a span.</param>
    /// <param name="arraySize">The size of the span. -1 will default to the length of the array.</param>
    /// <returns>A <see cref="Span{T}"/> that represents the <paramref name="inlineArray"/></returns>
    static public Span<TElement> AsSpan<TInlineArray>(ref TInlineArray inlineArray,
                                                      Int32 arraySize = -1)
        where TInlineArray : struct, IInlineArray<TInlineArray, TElement>
    {
        if (arraySize is -1)
        {
            arraySize = inlineArray.FixedLength;
        }

        return MemoryMarshal.CreateSpan(reference: ref Unsafe.As<TInlineArray, TElement>(source: ref inlineArray),
                                        length: arraySize);
    }

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{T}"/> representing the <paramref name="inlineArray"/>.
    /// </summary>
    /// <param name="inlineArray">The array to represent as a span.</param>
    /// <param name="arraySize">The size of the span. -1 will default to the length of the array.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> that represents the <paramref name="inlineArray"/></returns>
    static public ReadOnlySpan<TElement> AsReadOnlySpan<TInlineArray>(in TInlineArray inlineArray,
                                                                      Int32 arraySize = -1)
        where TInlineArray : struct, IInlineArray<TInlineArray, TElement>
    {
        if (arraySize is -1)
        {
            arraySize = inlineArray.FixedLength;
        }

        return MemoryMarshal.CreateReadOnlySpan(reference: ref Unsafe.As<TInlineArray, TElement>(source: ref Unsafe.AsRef(source: in inlineArray)),
                                                length: arraySize);
    }

    /// <summary>
    /// Returns a <see cref="Span{T}"/> representing the <paramref name="inlineArray"/>.
    /// </summary>
    /// <remarks>
    /// This method does not enforce the use of the <see cref="InlineArrayAttribute"/>. This might therefore fail if used on a struct that is not an inline array.
    /// Worst case would be the resulting span reading into unchecked memory.
    /// </remarks>
    /// <param name="inlineArray">The array to represent as a span.</param>
    /// <param name="arraySize">The size of the span. -1 will default to the length of the array.</param>
    /// <returns>A <see cref="Span{T}"/> that represents the <paramref name="inlineArray"/></returns>
    static public Span<TElement> AsSpanUnsafe<TInlineArray>(ref TInlineArray inlineArray,
                                                            Int32 arraySize = -1)
        where TInlineArray : struct
    {
        if (arraySize is -1)
        {
            if (AttributeResolver.HasAttribute<InlineArrayAttribute>(info: typeof(TInlineArray)) is true)
            {
                InlineArrayAttribute attribute = AttributeResolver.FetchSingleAttribute<InlineArrayAttribute>(info: typeof(TInlineArray));
                arraySize = attribute.Length;
            }
            else
            {
                arraySize = 0;
            }
        }

        return MemoryMarshal.CreateSpan(reference: ref Unsafe.As<TInlineArray, TElement>(source: ref inlineArray),
                                        length: arraySize);
    }

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{T}"/> representing the <paramref name="inlineArray"/>.
    /// </summary>
    /// <remarks>
    /// This method does not enforce the use of the <see cref="InlineArrayAttribute"/>. This might therefore fail if used on a struct that is not an inline array.
    /// Worst case would be the resulting span reading into unchecked memory.
    /// </remarks>
    /// <param name="inlineArray">The array to represent as a span.</param>
    /// <param name="arraySize">The size of the span. -1 will default to the length of the array.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> that represents the <paramref name="inlineArray"/></returns>
    static public ReadOnlySpan<TElement> AsReadOnlySpanUnsafe<TInlineArray>(in TInlineArray inlineArray,
                                                                            Int32 arraySize = -1)
        where TInlineArray : struct
    {
        if (arraySize is -1)
        {
            if (AttributeResolver.HasAttribute<InlineArrayAttribute>(info: typeof(TInlineArray)) is true)
            {
                InlineArrayAttribute attribute = AttributeResolver.FetchSingleAttribute<InlineArrayAttribute>(info: typeof(TInlineArray));
                arraySize = attribute.Length;
            }
            else
            {
                arraySize = 0;
            }
        }

        return MemoryMarshal.CreateReadOnlySpan(reference: ref Unsafe.As<TInlineArray, TElement>(source: ref Unsafe.AsRef(source: in inlineArray)),
                                                length: arraySize);
    }
}