using System;
using System.Collections.Generic;
using System.Text;
using RayTracingIn1Weekend.w1readstruct.Math;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Materials
{
    public class Lambertian : MaterialBase
    {
        public Vec3f Albedo;

        public Lambertian(in Vec3f a)
        {
            this.Albedo = a;
        }

        public override bool Scatter(in Rayf r, HitRecord rec, out Vec3f attenuation, out Rayf Scattered, Random drand)
        {
            Vec3f target = rec.Point + rec.Normal + RayTrace1.RandomUnitSphere(drand);
            //Scattered = new Rayf(rec.Point, target - rec.Point);
            Scattered = new Rayf(rec.Point, Subtract(target, rec.Point));

            attenuation = Albedo;

            return true;            
        }
    }
}
