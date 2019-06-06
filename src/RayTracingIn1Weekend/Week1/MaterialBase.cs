using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingIn1Weekend.Week1
{
    public abstract class MaterialBase
    {
        public abstract bool Scatter(Ray r, HitRecord rec, out Vec3f attenuation, out Ray Scattered, Random drand);
    }
}
