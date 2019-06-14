using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend.w1readstruct.Math;
using RayTracingIn1Weekend.w1readstruct.Materials;

namespace RayTracingIn1Weekend.w1readstruct.Materials
{

    [DebuggerDisplay("P={Point}, N={Normal}, t={t}")]
    public readonly struct HitRecord
    {
        public readonly float t;
        public readonly Vec3f Point;
        public readonly Vec3f Normal;
        public readonly MaterialBase Material;

        public HitRecord(float t, in Vec3f p, in Vec3f n, MaterialBase mat)
        {
            this.t = t;
            Point = p;
            Normal = n;
            Material = mat;
        }
    }
}
