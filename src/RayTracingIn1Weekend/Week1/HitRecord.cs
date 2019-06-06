using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{
    [DebuggerDisplay("P={Point}, N={Normal}, t={t}")]
    public struct HitRecord
    {
        public float t;
        public Vec3f Point;
        public Vec3f Normal;
        public MaterialBase Material;
    }
}
