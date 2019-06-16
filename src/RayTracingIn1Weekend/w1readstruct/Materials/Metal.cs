using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend.w1readstruct.Math;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Materials
{
    public class Metal : MaterialBase
    {
        public Vec3f Albedo;
        public float Fuzz;

        public Metal(in Vec3f a, float fuzz)
        {
            this.Albedo = a;
            this.Fuzz = fuzz < 1 ? fuzz : 1.0f;
        }

        public override bool Scatter(in Rayf r, HitRecord rec, out Vec3f attenuation, out Rayf Scattered, Random drand)
        {
            Vec3f reflected = Reflect(GetNormal(r.Direction), rec.Normal);
            Scattered = new Rayf(rec.Point, reflected + Fuzz * RayTrace1.RandomUnitSphere(drand));

            attenuation = Albedo;

            return Dot(Scattered.Direction, rec.Normal) > 0;            
        }

    }
}
