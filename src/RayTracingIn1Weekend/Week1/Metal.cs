using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{
    public class Metal : MaterialBase
    {
        public Vec3f Albedo;

        public Metal(Vec3f a)
        {
            this.Albedo = a;
        }

        public override bool Scatter(Ray r, HitRecord rec, out Vec3f attenuation, out Ray Scattered, Random drand)
        {
            Vec3f reflected = Vec3f.Reflect(r.Direction.GetNormal(), rec.Normal);
            Scattered = new Ray(rec.Point, reflected);

            attenuation = Albedo;

            return (Vec3f.Dot(Scattered.Direction, rec.Normal) > 0);
        }
    }
}