using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Buffers;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend;
using RayTracingIn1Weekend.Week1;

namespace RayTracingConsole
{
    internal static class FormatUtils
    {
        public static void ConvertBGR8(RayImage img, ref Span<byte> output)
        {
            if (output.Length < (img.Pixels.Length * 3))
                throw new ArgumentException("output cant hold all of the pixel data...");

            int cur = 0;

            foreach (var p in img.Pixels)
            {
                output[cur++] = (byte)(255.99 * p.B);
                output[cur++] = (byte)(255.99 * p.G);
                output[cur++] = (byte)(255.99 * p.R);
            }
        }
    }
}
