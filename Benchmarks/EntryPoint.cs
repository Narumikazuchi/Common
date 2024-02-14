#pragma warning disable IDE1006
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                 .Run(args);