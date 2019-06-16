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
    public struct HitRecord
    {
        public float t;
        public Vec3f Point;
        public Vec3f Normal;
        public MaterialBase Material;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HitRecord(float t, in Vec3f p, in Vec3f n, MaterialBase mat)
        {
            this.t = t;
            Point = p;
            Normal = n;
            Material = mat;
        }
    }
}
