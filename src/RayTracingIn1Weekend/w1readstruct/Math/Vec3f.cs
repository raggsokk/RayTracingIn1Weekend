using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Math
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("[{X}, {Y}, {Z}]")]
    public readonly struct Vec3f
    {
        private const MethodImplOptions METHODIMPL = MethodImplOptions.AggressiveInlining;

        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public static readonly Vec3f Zero = Vec3(0.0f, 0.0f, 0.0f);
        public static readonly Vec3f One = Vec3(1.0f, 1.0f, 1.0f);
        public static readonly Vec3f UnitX = Vec3(1.0f, 0.0f, 0.0f);
        public static readonly Vec3f UnitY = Vec3(0.0f, 1.0f, 0.0f);
        public static readonly Vec3f UnitZ = Vec3(0.0f, 0.0f, 1.0f);

        [MethodImpl(METHODIMPL)]
        public Vec3f(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #region Functions

        /*
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
            return VecMath.GetNormal(this);
        }
        */

        #endregion

        #region Operators

        /*
        public static Vec3f operator -(Vec3f v)
        {
            return Negate(v);
        }
        
        public static Vec3f operator + (Vec3f l, Vec3f r)
        {
            return Add(l, r);
        }

        public static Vec3f operator - (Vec3f l, Vec3f r)
        {
            return Subtract(l, r);
        }
        */

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(Vec3f v) => Negate(v);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator +(Vec3f l, Vec3f r) => Add(l, r);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(Vec3f l, Vec3f r) => Subtract(l, r);

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator *(Vec3f l, Vec3f r) => Multiply(l, r);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator /(Vec3f l, Vec3f r) => Divide(l, r);


        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator +(Vec3f v, float f) => Add(v, f);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(Vec3f v, float f) => Subtract(v, f);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator *(Vec3f v, float f) => Multiply(v, f);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator /(Vec3f v, float f) => Divide(v, f);

        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator +(float f, Vec3f v) => Add(v, f);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator -(float f, Vec3f v) => Subtract(f, v);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator *(float f, Vec3f v) => Multiply(v, f);
        [DebuggerNonUserCode()]
        [MethodImpl(METHODIMPL)]
        public static Vec3f operator /(float f, Vec3f v) => Divide(f, v);



        public static bool operator ==(Vec3f l, Vec3f r) => IsEqual(l, r);
        public static bool operator !=(Vec3f l, Vec3f r) => NotEqual(l, r);


        #endregion

        #region Object Overloads

        public override bool Equals(object obj)
        {
            if (obj is Vec3f v)
                return IsEqual(this, v);

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

        #endregion
    }
}