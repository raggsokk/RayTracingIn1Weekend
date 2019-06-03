using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("{R} {G} {B}")]
    public struct Rgb3f
    {
        public float R;
        public float G;
        public float B;

        public Rgb3f(float r = 0, float g = 0, float b = 0)
        {
            R = r; G = g; B = b;
        }
    }
}
