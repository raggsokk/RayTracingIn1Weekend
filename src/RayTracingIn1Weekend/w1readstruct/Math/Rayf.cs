using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Math
{
    public readonly struct Rayf
    {
        public readonly Vec3f Origin;
        public readonly Vec3f Direction;

        public Rayf(in Vec3f o, in Vec3f d)
        {
            this.Origin = o;
            this.Direction = d;
        }

        //public Vec3f PointAtParameter(float f) => this.Origin + f * this.Direction;
        public Vec3f PointAtParameter(float f) => Add(this.Origin, Multiply(this.Direction, f));
    }
}
