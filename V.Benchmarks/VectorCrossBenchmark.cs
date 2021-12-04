using BenchmarkDotNet.Attributes;

namespace V.Benchmarks
{
    [MemoryDiagnoser]
    public class VectorCrossBenchmark
    {
        [Params(3, 10)]
        public int Dimensionality { get; set; }

        private Vector[]? Vectors { get; set; }

        private static readonly Random Random = new(9001);

        [IterationSetup]
        public void IterationSetup()
        {
            IEnumerable<double> GenerateRandom() => Enumerable.Range(0, Dimensionality).Select(_ => Random.NextDouble());
            Vectors = Enumerable.Range(0, Dimensionality -1).Select(_ => new Vector(GenerateRandom())).ToArray();
        }

        [Benchmark]
        public Vector Cross()
            => Vector.Cross(Vectors!);
    }
}
