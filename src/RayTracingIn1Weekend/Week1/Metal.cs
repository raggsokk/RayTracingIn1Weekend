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
        public float Fuzz;

        public Metal(Vec3f a, float fuzz)
        {
            this.Albedo = a;            
            this.Fuzz = fuzz < 1 ? fuzz : 1.0f;
        }

        public override bool Scatter(Ray r, HitRecord rec, out Vec3f attenuation, out Ray Scattered, Random drand)
        {
            Vec3f reflected = Vec3f.Reflect(r.Direction.GetNormal(), rec.Normal);
            Scattered = new Ray(rec.Point, reflected + Fuzz * RayTrace1.RandomInUnitSphere(drand));

            attenuation = Albedo;

            return (Vec3f.Dot(Scattered.Direction, rec.Normal) > 0);
        }
    }
}