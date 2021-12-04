// See https://aka.ms/new-console-template for more information
#if DEBUG
using V.Benchmarks;
#else
using BenchmarkDotNet.Running;
#endif

#if DEBUG
Console.WriteLine("Running in debug mode.");
var vectorCross = new VectorCrossBenchmark();
vectorCross.Dimensionality = 100;
vectorCross.IterationSetup();
vectorCross.Cross();
#else
BenchmarkRunner.Run(typeof(Program).Assembly);
#endif
