using System;
using System.Collections.Generic;
using System.Text;

using RayTracingIn1Weekend.w1readstruct.Math;

namespace RayTracingIn1Weekend.w1readstruct.Materials
{
    // right now this is class based.
    // later we should try this with struct based.
    public abstract class MaterialBase
    {
        public abstract bool Scatter(in Rayf r, HitRecord rec, out Vec3f attenuation, out Rayf Scattered, Random drand);
    }
}
