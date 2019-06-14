using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RayTracingIn1Weekend.w1readstruct.Math
{
    /// <summary>
    /// Contains Vector math functions.
    /// use "using static" in order to bypass class prefix all the time.
    /// </summary>
    public static class VecMath
    {
        private const MethodImplOptions METHODIMPL = MethodImplOptions.AggressiveInlining;

        #region Vec3f

        [MethodImpl(METHODIMPL)]
        public static Vec3f Vec3(float x, float y, float z)
        {
            return new Vec3f(x, y, z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Negate(in Vec3f v)
        {
            return new Vec3f(-v.X, -v.Y, -v.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Add(in Vec3f l, in Vec3f r)
        {
            return new Vec3f(
                l.X + r.X,
                l.Y + r.Y,
                l.Z + r.Z
            );
        }

        [MethodImpl(METHODIMPL)]
        public static void Add(in Vec3f l, in Vec3f r, out Vec3f v)
        {
            v = Add(l, r);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Subtract(in Vec3f l, in Vec3f r)
        {
            return new Vec3f(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Multiply(in Vec3f l, in Vec3f r)
        {
            return new Vec3f(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Divide(in Vec3f l, in Vec3f r)
        {
            return new Vec3f(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Add(in Vec3f v, float f)
        {
            return new Vec3f(v.X + f, v.Y + f, v.Z + f);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Subtract(in Vec3f v, float f)
        {
            return new Vec3f(v.X - f, v.Y - f, v.Z - f);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Multiply(in Vec3f v, float f)
        {
            return new Vec3f(v.X * f, v.Y * f, v.Z * f);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Divide(in Vec3f v, float f)
        {
            float k = 1.0f / f;
            return Multiply(v, k);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Subtract(float f, in Vec3f v)
        {
            return new Vec3f(f - v.X, f - v.Y, f - v.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Divide(float f, in Vec3f v)
        {
            return new Vec3f(f / v.X, f / v.Y, f / v.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static bool IsEqual(in Vec3f l, in Vec3f r)
        {
            return l.X == r.X && l.Y == r.Y && l.Z == r.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static bool IsEqual(in Vec3f l, in Vec3f r, float e)
        {
            return l.X - r.X >= e &&
                l.Y - r.Y >= e &&
                l.Z - r.Z >= e;
        }

        [MethodImpl(METHODIMPL)]
        public static bool NotEqual(in Vec3f l, in Vec3f r)
        {
            return l.X != r.X || l.Y != r.Y || l.Z != r.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static bool NotEqual(in Vec3f l, in Vec3f r, float e)
        {
            return l.X - r.X <= e ||
                l.Y - r.Y <= e ||
                l.Z - r.Z <= e;
        }

        [MethodImpl(METHODIMPL)]
        public static float GetLengthSquared(in Vec3f v)
        {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static float GetLength(in Vec3f v)
        {
            return MathF.Sqrt(GetLengthSquared(v));
            //return MathF.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f GetNormal(in Vec3f v)
        {
            var len = GetLength(v);

            return Divide(v, len);
        }

        /// <summary>
        /// Calculates the dot product of two (pre-)normalized vectors.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(METHODIMPL)]
        public static float Dot(in Vec3f l, in Vec3f r)
        {
            return l.X * r.X + l.Y * r.Y + l.Z * r.Z;
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Cross(in Vec3f l, in Vec3f r)
        {
            return new Vec3f(
                l.Y * r.Z - l.Z * r.Y,
                -(l.X * r.Z - l.Z * r.X),
                l.X * r.Y - l.Y * r.X
                );
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3f Reflect(in Vec3f v, in Vec3f n)
        {
            return v - 2 * Dot(v, n) * n;
        }

        public static bool Refract(in Vec3f v, in Vec3f n, float ni_over_nt, out Vec3f refracted)
        {
            Vec3f uv = GetNormal(v);
            float dt = Dot(uv, n);

            float disciminant = 1f - ni_over_nt * ni_over_nt * (1f - dt * dt);

            if(disciminant > 0)
            {
                refracted = ni_over_nt * (uv - n * dt) - n * MathF.Sqrt(disciminant);
                return true;
            }
            else
            {
                refracted = Vec3f.Zero;
                return false;
            }

        }

        #endregion

        #region Vec3d

        [MethodImpl(METHODIMPL)]
        public static Vec3d Vec3(double x, double y, double z)
        {
            return new Vec3d(x, y, z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3d Negate(in Vec3d v)
        {
            return new Vec3d(-v.X, -v.Y, -v.Z);
        }

        [MethodImpl(METHODIMPL)]
        public static Vec3d Add(in Vec3d l, in Vec3d r)
        {
            return new Vec3d(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        }

        #endregion
    }
}
