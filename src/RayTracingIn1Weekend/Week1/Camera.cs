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

        public Camera(Vec3f lookFrom, Vec3f lookAt, Vec3f vup, float vfov, float aspect)
        {
            Vec3f u, v, w;
            float theta = vfov * MathF.PI / 180;
            float half_height = MathF.Tan(theta / 2);
            float half_width = aspect * half_height;

            Origin = lookFrom;
            w = (lookFrom - lookAt).GetNormal();
            u = Vec3f.Cross(vup, w).GetNormal();
            v = Vec3f.Cross(w, u);

            //LowerLeftCorner = new Vec3f(-half_width, -half_height, -1.0f);
            LowerLeftCorner = Origin - half_width * u - half_height * v - w;

            Horizontal = 2 * half_width * u;
            Vertical = 2 * half_height * v;




            //LowerLeftCorner = new Vec3f(-half_width, -half_height, -1.0f);
            //Horizontal = new Vec3f(2 * half_width, 0f, 0f);
            //Vertical = new Vec3f(0f, 2 * half_height, 0f);
            //Origin = Vec3f.Zero;

            //LowerLeftCorner = new Vec3f(-2.0f, -1.0f, -1.0f);
            //Horizontal = new Vec3f(4.0f, 0.0f, 0.0f);
            //Vertical = new Vec3f(0.0f, 2.0f, 0.0f);
            //Origin = Vec3f.Zero;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
        }
    }
}
