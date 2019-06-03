using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using RayTracingIn1Weekend;
using RayTracingIn1Weekend.Week1;

namespace RayTracingConsole
{
    //public class PpmFormat
    //{

    //}

    public static class PpmFormatExtension
    {
        public static bool WritePpm(this RayImage img, Stream output)
        {
            try
            {
                using (var writer = new StreamWriter(output))
                {
                    //var writer = new StreamWriter(output);

                    writer.WriteLine("P3");
                    writer.WriteLine($"{img.Width} {img.Height}");
                    writer.WriteLine("255");

                    foreach (var p in img.Pixels)
                    {
                        int ir = (int)(255.99 * p.R);
                        int ig = (int)(255.99 * p.G);
                        int ib = (int)(255.99 * p.B);

                        writer.WriteLine($"{ir} {ig} {ib} ");
                    }

                    writer.Flush();
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
