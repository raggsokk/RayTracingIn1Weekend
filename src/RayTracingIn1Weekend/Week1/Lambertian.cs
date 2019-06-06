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

        public override bool Scatter(Ray r, HitRecord rec, Vec3f attenuation, ref Ray Scattered, Random drand)
        {
            Vec3f target = rec.Point + rec.Normal + RayTrace1.RandomInUnitSphere(drand);
            Scattered = new Ray(rec.Point, target - rec.Point);

            attenuation = Albedo;

            return true;

            //throw new NotImplementedException();
        }


    }
}
