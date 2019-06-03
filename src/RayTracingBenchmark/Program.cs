using System;

using BenchmarkDotNet;
using BenchmarkDotNet.Running;

namespace RayTracingBenchmark
{
    // Class handles benchmarking my raytrace implementation.
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();

            //Console.WriteLine("Hello World!");
        }
    }
}
