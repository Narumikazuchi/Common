#if NETCOREAPP3_0_OR_GREATER
global using System.Collections.Immutable;
#endif
#if NET7_0_OR_GREATER
global using System.Numerics;
#endif

using Narumikazuchi.TypeExtensions;

[assembly: UnionOf(typeof(String), typeof(SByte), typeof(UInt16), typeof(UInt32), typeof(UInt64), Typename = "StringOrUnsignedInt")]