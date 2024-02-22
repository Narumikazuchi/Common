#if NET8_0_OR_GREATER
#endif

using Narumikazuchi.Generators.TaggedUnions;

[assembly: UnionOf(typeof(String), typeof(Byte), typeof(UInt16), typeof(UInt32), typeof(UInt64), Typename = "StringOrUnsignedInt")]