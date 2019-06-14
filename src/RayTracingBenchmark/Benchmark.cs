using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

using RayTracingIn1Weekend;
using RayTracingIn1Weekend.Week1;

namespace RayTracingBenchmark
{
    public class Benchmark
    {
        public int Width = 200;
        public int Height = 100;

        [Benchmark(Description = "Week1")]
        public void RenderWeek1()
        {
            var img = RayTrace1.Render(Width, Height, 10);
        }
    }
}
