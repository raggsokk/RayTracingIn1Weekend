using System;
using System.IO;
using System.Diagnostics;

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

            // parameters to given by arguments later.
            var width = 200;
            var height = 100;
            var output = "image.bmp";
            Func<RayImage, Stream, bool> encoder = (r, s) => r.WriteBmp(s);


            // code.
            var watch = new Stopwatch();
            watch.Start();

            // render.
            var img = RayTrace1.Render(width, height);

            watch.Stop();

            using(var f = File.OpenWrite(output))
            {
                //img.WriteBmp(f);
                encoder(img, f);
            }

            // done message.
            Console.WriteLine($"Image '{output}' rendered in {watch.ElapsedMilliseconds}ms.");
        }
    }
}
