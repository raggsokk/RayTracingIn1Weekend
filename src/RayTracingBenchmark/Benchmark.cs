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
        //public int Width = 200;
        //public int Height = 100;

        [Benchmark(Description = "Week1")]
        [Arguments(100, 50, 10)]
        [Arguments(100, 50, 50)]
        [Arguments(200, 100, 10)]
        [Arguments(200, 100, 50)]
        public void RenderWeek1(int width, int height, int samples)
        {
            var img = RayTrace1.Render(width, height, samples);
        }
    }
}
