using System;

namespace RayTracingConsole
{
    public class Options
    {
        public int Width {get;set;}
        public int Height {get;set;}
        public int Samples {get;set;}
        public bool Parallel {get;set;} = true; // change this to specifiy thread count?
        public string Output {get;set;}
    }
}