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
#if NET5_0_OR_GREATER && !NET7_0_OR_GREATER
global using System.Threading.Tasks;
#endif