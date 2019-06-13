using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.w1readstruct
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("[{X}, {Y}, {Z}]")]
    public readonly struct Vec3f
    {
        public float X;
        public float Y;
        public float Z;
        
        public Vec3f(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
                
        public float GetLengthSquared()
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
        }
        
        public float GetLength()
        {
            return MathF.Sqrt(GetLengthSquared());
        }
        
        public Vec3f GetNormal()
        {
            throw new NotImplementedException();
            return this / GetLength();
        }
        
        public static Vec3f operator -(Vec3f v)
        {
            return new Vec3f(
                -v.X,
                -v.Y,
                -v.Z
            );
        }
        
        public static Vec3f operator + (Vec3f l, Vec3f r)
        {
            return Add(l, r);
        }
        
        public static Vec3f Add(in Vec3f l, in Vec3f r)
        {
            return new Vec3f(
                l.X + r.X,
                l.Y + r.Y,
                l.Z + r.Z
            );
        }
        
        public static void Add(in Vec3f l, in Vec3f r, out Vec3f v)
        {
             v = Add(l, r);
        }
    }
}