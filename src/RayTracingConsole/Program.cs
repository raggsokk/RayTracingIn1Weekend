using System;
using System.IO;

using RayTracingIn1Weekend;
using RayTracingIn1Weekend.Week1;



namespace RayTracingConsole
{
    //Class handles rendering options, output format, etc.
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Handle argument parsing before calling render etc.            

            var img = RayTrace1.Render(200, 100);

            using(var f = File.OpenWrite("image.ppm"))
            {
                img.WritePpm(f);
            }

        }
    }
}
