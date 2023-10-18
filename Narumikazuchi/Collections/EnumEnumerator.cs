namespace Narumikazuchi.Collections;

/// <summary>
/// Represents an <see cref="IStrongEnumerable{TElement, TEnumerator}"/> and <see cref="IStrongEnumerator{TElement}"/> of type
/// <typeparamref name="TEnum"/>.
/// </summary>
[DebuggerDisplay("{Typename}[]")]
[Obsolete($"This type has been superceded by the '{nameof(FlagEnumerator<TEnum>)}' struct.")]
public partial struct EnumEnumerator<TEnum>
    where TEnum : struct, Enum
{
    /// <inheritdoc/>
    [return: NotNull]
    public override readonly String ToString()
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