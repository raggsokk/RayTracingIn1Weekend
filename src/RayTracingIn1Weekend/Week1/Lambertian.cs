using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{
    public class Lambertian : MaterialBase
    {
        public Vec3f Albedo;

        public Lambertian(Vec3f a)
        {
            this.Albedo = a;
        }

        public override bool Scatter(Ray r, HitRecord rec, out Vec3f attenuation, out Ray Scattered, Random drand)
        {
            Vec3f target = rec.Point + rec.Normal + RayTrace1.RandomInUnitSphere(drand);
            Scattered = new Ray(rec.Point, target - rec.Point);

            attenuation = Albedo;

            return true;
        }
    }
}
