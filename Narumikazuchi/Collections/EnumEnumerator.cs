namespace Narumikazuchi.Collections;

/// <summary>
/// Represents an <see cref="IStrongEnumerable{TElement, TEnumerator}"/> and <see cref="IStrongEnumerator{TElement}"/> of type
/// <typeparamref name="TEnum"/>.
/// </summary>
[DebuggerDisplay("{Typename}[]")]
public partial struct EnumEnumerator<TEnum>
    where TEnum : struct, Enum
{
    /// <inheritdoc/>
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [return: NotNull]
#endif
    public override String ToString()
    {
        if (m_Mode is 0)
        {
            return "- empty -";
        }
        else if (m_Mode is 1)
        {
            return String.Join(" | ", s_Values.Value);
        }
        else if (m_Mode is 2)
        {
            return m_Value!.ToString()!;
        }
        else
        {
            throw new ImpossibleState();
        }
    }
}