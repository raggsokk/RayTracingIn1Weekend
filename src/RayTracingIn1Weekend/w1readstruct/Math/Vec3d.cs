using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Math
{
    /// <summary>
    /// This class is just to test operator overloading in VecMath class.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("[{X}, {Y}, {Z}]")]
    public readonly struct Vec3d
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public static readonly Vec3d Zero = Vec3(0.0, 0.0, 0.0);
        public static readonly Vec3d One = Vec3(1.0, 1.0, 1.0);
        public static readonly Vec3d UnitX = Vec3(1.0, 0.0, 0.0);
        public static readonly Vec3d UnitY = Vec3(0.0, 1.0, 0.0);
        public static readonly Vec3d UnitZ = Vec3(0.0, 0.0, 1.0);

        public Vec3d(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
