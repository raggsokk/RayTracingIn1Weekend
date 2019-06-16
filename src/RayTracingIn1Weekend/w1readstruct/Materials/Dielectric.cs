using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using RayTracingIn1Weekend.w1readstruct.Math;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Materials
{
    public class Dielectric : MaterialBase
    {
        public float RefIndex;

        public Dielectric(float refIndex)
        {
            this.RefIndex = refIndex;
        }

        private static float Schlick(float cosine, float refIndex)
        {
            float r0 = (1f - refIndex) / (1 + refIndex);
            r0 = r0 * r0;
            return r0 + (1f - r0) * MathF.Pow((1 - cosine), 5);
        }

        public override bool Scatter(in Rayf r, HitRecord rec, out Vec3f attenuation, out Rayf Scattered, Random drand)
        {
            Vec3f outward_normal;
            Vec3f reflected = Reflect(r.Direction, rec.Normal);
            float ni_over_nt;

            attenuation = Vec3f.One;
            Vec3f refracted;
            float reflect_prob;
            float cosine;

            if(Dot(r.Direction, rec.Normal) > 0)
            {
                outward_normal = -rec.Normal;
                ni_over_nt = RefIndex;
                cosine = RefIndex * Dot(r.Direction, rec.Normal) / GetLength(r.Direction);
            }
            else
            {
                outward_normal = rec.Normal;
                ni_over_nt = 1.0f / RefIndex;
                cosine = -Dot(r.Direction, rec.Normal) / GetLength(r.Direction);
            }

            if(Refract(r.Direction, outward_normal, ni_over_nt, out refracted))
            {
                reflect_prob = Schlick(cosine, RefIndex);
            }
            else
            {
                Scattered = new Rayf(rec.Point, reflected);
                reflect_prob = 1.0f;
            }

            if(drand.NextFloat() < reflect_prob)
            {
                Scattered = new Rayf(rec.Point, reflected);
            }
            else
            {
                Scattered = new Rayf(rec.Point, refracted);
            }

            return true;            
        }
    }
}
