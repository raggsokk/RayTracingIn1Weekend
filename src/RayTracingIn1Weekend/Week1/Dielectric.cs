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

        public override bool Scatter(Ray r, HitRecord rec, out Vec3f attenuation, out Ray Scattered, Random drand)
        {
            Vec3f outward_normal;
            Vec3f reflected = Vec3f.Reflect(r.Direction, rec.Normal);
            float ni_over_nt;

            // try 1,1,0 to see color shift, then change to 1,1,1 and see differance.
            attenuation = new Vec3f(1.0f, 1.0f, 1.0f);
            Vec3f refracted;

            if(Vec3f.Dot(r.Direction, rec.Normal) > 0)
            {
                outward_normal = -rec.Normal;
                ni_over_nt = RefIndex;
            }
            else
            {
                outward_normal = rec.Normal;
                ni_over_nt = 1.0f / RefIndex;
            }

            if(Vec3f.Refract(r.Direction, outward_normal, ni_over_nt, out refracted))
            {
                Scattered = new Ray(rec.Point, refracted);
            }
            else
            {
                Scattered = new Ray(rec.Point, reflected);
                return false;
            }

            return true;            
        }
    }
}
