using System;

using BenchmarkDotNet;
using BenchmarkDotNet.Running;

namespace RayTracingBenchmark
{
    // Class handles benchmarking my raytrace implementations.
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}
