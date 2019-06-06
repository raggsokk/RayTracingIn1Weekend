using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracingIn1Weekend.Week1
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
            float theta = vfov * MathF.PI / 180;
            float half_height = MathF.Tan(theta / 2);
            float half_width = aspect * half_height;
            Origin = lookFrom;
            this.w = (lookFrom - lookAt).GetNormal();
            this.u = Vec3f.Cross(vup, w).GetNormal();
            this.v = Vec3f.Cross(w, u);

            LowerLeftCorner = Origin - half_width * focus_distance * u - half_height * focus_distance * v - focus_distance * w;
            Horizontal = 2 * half_width * focus_distance * u;
            Vertical = 2 * half_height * focus_distance * v;


            //Vec3f u, v, w;
            //LowerLeftCorner = new Vec3f(-half_width, -half_height, -1.0f);
            //LowerLeftCorner = Origin - half_width * u - half_height * v - w;

            //Horizontal = 2 * half_width * u;
            //Vertical = 2 * half_height * v;
        }

        public Ray GetRay(float s, float t, Random drand)
        {
            Vec3f rd = LensRadius * Random_in_unit_disk(drand);
            Vec3f offset = u * rd.X + v * rd.Y;
            return new Ray(Origin + offset,
                LowerLeftCorner + s * Horizontal + t * Vertical - Origin - offset);
            //return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
        }

        public Vec3f Random_in_unit_disk(Random drand)
        {
            Vec3f p;
            do
            {
                p = 2.0f * new Vec3f((float)drand.NextDouble(), (float)drand.NextDouble(), 0) - new Vec3f(1f, 1f, 0);
            } while (Vec3f.Dot(p, p) >= 1f);

            return p;
        }
    }
}
