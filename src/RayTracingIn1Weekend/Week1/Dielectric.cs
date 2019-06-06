using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
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
            return r0 + (1 - r0) * MathF.Pow((1 - cosine), 5);
        }

        public override bool Scatter(Ray r, HitRecord rec, out Vec3f attenuation, out Ray Scattered, Random drand)
        {
            Vec3f outward_normal;
            Vec3f reflected = Vec3f.Reflect(r.Direction, rec.Normal);
            float ni_over_nt;

            // try 1,1,0 to see color shift, then change to 1,1,1 and see differance.
            attenuation = new Vec3f(1.0f, 1.0f, 1.0f);
            Vec3f refracted;
            float reflect_prob;
            float cosine;

            if(Vec3f.Dot(r.Direction, rec.Normal) > 0)
            {
                outward_normal = -rec.Normal;
                ni_over_nt = RefIndex;
                cosine = RefIndex * Vec3f.Dot(r.Direction, rec.Normal) / r.Direction.GetLength();
            }
            else
            {
                outward_normal = rec.Normal;
                ni_over_nt = 1.0f / RefIndex;
                cosine = -Vec3f.Dot(r.Direction, rec.Normal) / r.Direction.GetLength();
            }

            if(Vec3f.Refract(r.Direction, outward_normal, ni_over_nt, out refracted))
            {
                reflect_prob = Schlick(cosine, RefIndex);
                //Scattered = new Ray(rec.Point, refracted);
            }
            else
            {
                Scattered = new Ray(rec.Point, reflected); // comment out this line?
                reflect_prob = 1.0f;
                //return false;
            }

            if(drand.NextDouble() < reflect_prob)
            {
                Scattered = new Ray(rec.Point, reflected);
            }
            else
            {
                Scattered = new Ray(rec.Point, refracted);
            }

            return true;            
        }
    }
}
