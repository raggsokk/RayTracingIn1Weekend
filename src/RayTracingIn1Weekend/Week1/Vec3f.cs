using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RayTracingIn1Weekend.Week1
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("[{X}, {Y}, {Z}]")]
    public struct Vec3f
    {
        private const MethodImplOptions METHODIMPL = MethodImplOptions.AggressiveInlining;

        public float X;
        public float Y;
        public float Z;

        public static readonly Vec3f Zero = new Vec3f(0.0f, 0.0f, 0.0f);
        public static readonly Vec3f One = new Vec3f(1.0f, 1.0f, 1.0f);
        public static readonly Vec3f UnitX = new Vec3f(1.0f, 0.0f, 0.0f);
        public static readonly Vec3f UnitY = new Vec3f(0.0f, 1.0f, 0.0f);
        public static readonly Vec3f UnitZ = new Vec3f(0.0f, 0.0f, 1.0f);

        [MethodImpl(METHODIMPL)]
        public Vec3f(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        [MethodImpl(METHODIMPL)]
        public float GetLength()
        {
            return MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
        }

        [MethodImpl(METHODIMPL)]
        public float GetLengthSquared()
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
        }

        [MethodImpl(METHODIMPL)]
        public Vec3f GetNormal()
        {
            var len = GetLength();
            return this / len;
        }

        [MethodImpl(METHODIMPL)]
        public void Normalize()
        {
            float k = 1.0f / MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            this.X = this.X * k;
            this.Y = this.Y * k;
            this.Z = this.Z * k;
        }

        [Obsolete("Use Normalize() instead")]
        public void MakeUnitVector()
        {
            Normalize();
        }

        [MethodImpl(METHODIMPL)]
        public float Dot(Vec3f v)
        {
            return Dot(this, v);
        }

        [MethodImpl(METHODIMPL)]
        public Vec3f Cross(Vec3f v)
        {
            return Cross(this, v);
        }

        [MethodImpl(METHODIMPL)]
        public static float Dot(Vec3f l, Vec3f r)
        {
            return l.X * r.X + l.Y * r.Y + l.Z * r.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Cross(Vec3f l, Vec3f r)
        {
            return new Vec3f(
                l.Y * r.Z - l.Z * r.Y,
                -(l.X * r.Z - l.Z * r.X),
                l.X * r.Y - l.Y * r.X
            );
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Reflect(Vec3f v, Vec3f n)
        {
            return v - 2 * Dot(v,n) * n;
        }

        [MethodImpl(METHODIMPL)]
        public static bool Refract(Vec3f v, Vec3f n, float ni_over_nt, out Vec3f refracted)
        {
            Vec3f uv = v.GetNormal();
            float dt = Dot(uv, n);
            float discriminant = 1f - ni_over_nt * ni_over_nt * (1f - dt * dt);
            if (discriminant > 0)
            {
                refracted = ni_over_nt * (uv - n * dt) - n * MathF.Sqrt(discriminant);
                return true;
            }
            else
            {
                refracted = Vec3f.Zero;
                return false;
            }
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(Vec3f v)
        {
            return new Vec3f(
                -v.X,
                -v.Y,
                -v.Z
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator +(Vec3f l, Vec3f r)
        {
            return new Vec3f(            
                l.X + r.X,
                l.Y + r.Y,
                l.Z + r.Z
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(Vec3f l, Vec3f r)
        {
            return new Vec3f(
                l.X - r.X,
                l.Y - r.Y,
                l.Z - r.Z
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator *(Vec3f l, Vec3f r)
        {
            return new Vec3f(
                l.X * r.X,
                l.Y * r.Y,
                l.Z * r.Z
            );
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f operator /(Vec3f l, Vec3f r)
        {
            return new Vec3f(
                l.X / r.X,
                l.Y / r.Y,
                l.Z / r.Z
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator +(Vec3f v, float f)
        {
            return new Vec3f(
                v.X + f,
                v.Y + f,
                v.Z + f
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(Vec3f v, float f)
        {
            return new Vec3f(
                v.X - f,
                v.Y - f,
                v.Z - f
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator *(Vec3f v, float f)
        {
            return new Vec3f(
                v.X * f,
                v.Y * f,
                v.Z * f
            );
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f operator /(Vec3f v, float f)
        {
            float k = 1.0f / f;

            return new Vec3f(
                v.X * k,
                v.Y * k,
                v.Z * k
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator +(float f, Vec3f v)
        {
            return new Vec3f(
                v.X + f,
                v.Y + f,
                v.Z + f
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(float f, Vec3f v)
        {
            return new Vec3f(
                f - v.X,
                f - v.Y,
                f - v.Z
            );
        }

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator *(float f, Vec3f v)
        {
            return new Vec3f(
                v.X * f,
                v.Y * f,
                v.Z * f
            );
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f operator /(float f, Vec3f v)
        {
            return new Vec3f(
                f / v.X,
                f / v.Y,
                f / v.Z
            );
        }

        [MethodImpl(METHODIMPL)]
        public static bool operator == (Vec3f l, Vec3f r)
        {
            return l.X == r.X && l.Y == r.Y && l.Z == r.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static bool operator !=(Vec3f l, Vec3f r)
        {
            return l.X != r.X || l.Y != r.Y || l.Z != r.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static explicit operator Rgb3f (Vec3f v)
        {
            return new Rgb3f(
                v.X,
                v.Y,
                v.Z
            );
            //return MemoryMarshal.Cast<Vec3f, Rgb3f>()
        }


        public override bool Equals(object obj)
        {            
            if (obj is Vec3f v)
                return this == v;

            return false;
        }

        public override int GetHashCode()
        {
            return (X, Y, Z).GetHashCode();
            //unchecked
            //{
            //    int hash = 17;
            //    hash = hash * 23 + X.GetHashCode();
            //    hash = hash * 23 + Y.GetHashCode();
            //    hash = hash * 23 + Z.GetHashCode();

            //    return hash;
            //}
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }
    }
}
