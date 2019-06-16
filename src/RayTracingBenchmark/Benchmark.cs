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
    [MemoryDiagnoser]
    [ShortRunJob()]
    public class Benchmark
    {
        //public int Width = 200;
        //public int Height = 100;

        
        [Benchmark(Description = "Week1")]
        //[Arguments(100, 50, 10)]
        //[Arguments(100, 50, 50)]
        [Arguments(200, 100, 10)]
        //[Arguments(200, 100, 50)]
        public void RenderWeek1(int width, int height, int samples)
        {
            var img = RayTrace1.Render(width, height, samples);
        }

        //[Benchmark(Description = "Week1Parallel")]
        ////[Arguments(100, 50, 10)]
        ////[Arguments(100, 50, 50)]
        //[Arguments(200, 100, 10)]
        ////[Arguments(200, 100, 15)]
        //public void RenderWeek1Parallel(int width, int height, int samples)
        //{
        //    var img = RayTrace1.RenderParallelFor(width, height, samples);
        //}

        [Benchmark(Description = "readonlystruct")]
        //[Arguments(100, 50, 10)]
        //[Arguments(100, 50, 50)]
        [Arguments(200, 100, 10)]
        //[Arguments(200, 100, 50)]
        public void RenderWeek1readonlystruct(int width, int height, int samples)
        {
            var img = RayTracingIn1Weekend.w1readstruct.RayTrace1.Render(width, height, samples);
        }
    }
}
