#if NETCOREAPP3_0_OR_GREATER
global using System.Collections.Immutable;
#endif
#if NETCOREAPP3_1_OR_GREATER
global using System.Runtime.CompilerServices;
#endif
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
global using System.Diagnostics.CodeAnalysis;
#endif
#if NET7_0_OR_GREATER
global using System.Numerics;
#endif

#if NETCOREAPP3_1_OR_GREATER
using Narumikazuchi.TypeExtensions;

[assembly: UnionOf(typeof(String), typeof(SByte), typeof(UInt16), typeof(UInt32), typeof(UInt64), Typename = "StringOrUnsignedInt")]
#endif