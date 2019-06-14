using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RayTracingIn1Weekend.w1readstruct.Math
{
    public static class MathExtensions
    {
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random drand)
        {
            return (float)drand.NextDouble();
        }
    }
}
