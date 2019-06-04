using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{
    public abstract class HitableBase
    {
        public abstract bool Hit(Ray r, float tMin, float tMax, ref HitRecord record);
    }
}
