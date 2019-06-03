using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("")]
    public struct Ray
    {
        public Vec3f Origin;
        public Vec3f Direction;

        public Ray(Vec3f o, Vec3f d)
        {
            this.Origin = o;
            this.Direction = d;
        }

        public Vec3f PointAtParameter(float f) => this.Origin + f * this.Direction;
    }
}
