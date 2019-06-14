using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend.w1readstruct.Math;
using RayTracingIn1Weekend.w1readstruct.Materials;

namespace RayTracingIn1Weekend.w1readstruct.Scene
{
    // right now this is class based.
    // later we should try this with struct based.
    public abstract class HitableBase
    {
        public abstract bool Hit(in Rayf r, float tMin, float tMax, ref HitRecord record);
    }
}
