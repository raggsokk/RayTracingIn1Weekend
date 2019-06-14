using System;
using System.Collections.Generic;
using System.Text;

using RayTracingIn1Weekend.w1readstruct.Math;
using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Scene
{
    public class Camera
    {
        public Vec3f Origin;
        public Vec3f LowerLeftCorner;
        public Vec3f Horizontal;
        public Vec3f Vertical;
        public Vec3f u;
        public Vec3f v;
        public Vec3f w;
        public float LensRadius;

        public Camera(Vec3f lookFrom, Vec3f lookAt, Vec3f vup, float vfov, float aspect,
            float aperture, float focus_distance)
        {
            LensRadius = aperture / 2f;
            float theta = vfov * MathF.PI / 180f;
            float half_heigth = MathF.Tan(theta / 2f);
            float half_width = aspect * half_heigth;
            Origin = lookFrom;

            this.w = GetNormal(lookFrom - lookAt);
            this.u = GetNormal(Cross(vup, w));
            this.v = Cross(w, u);

            LowerLeftCorner = Origin - half_width * focus_distance * u - half_heigth * focus_distance * v - focus_distance * w;
            Horizontal = 2 * half_width * focus_distance * u;
            Vertical = 2 * half_heigth * focus_distance * v;
        }

        public Rayf GetRay(float s, float t, Random drand)
        {
            Vec3f rd = LensRadius * Random_In_Unit_Disk(drand);
            Vec3f offset = u * rd.X + v * rd.Y;
            return new Rayf(Origin + offset,
                LowerLeftCorner + s * Horizontal + t * Vertical - Origin - offset);
        }

        public Vec3f Random_In_Unit_Disk(Random drand)
        {
            Vec3f p;
            do
            {
                p = 2.0f * new Vec3f(drand.NextFloat(), drand.NextFloat(), 0) - new Vec3f(1f, 1f, 0);
            } while (Dot(p, p) >= 1f);

            return p;
        }
    }
}
