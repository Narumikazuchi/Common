namespace Narumikazuchi;

/// <summary>
/// Represents the method to compare two instances of the same type for equality.
/// </summary>
public delegate Boolean EqualityComparison<TComparable>([AllowNull] TComparable? first,
                                                        [AllowNull] TComparable? second);