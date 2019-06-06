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

        public Camera(float vfov, float aspect)
        {
            float theta = vfov * MathF.PI / 180;
            float half_height = MathF.Tan(theta / 2);
            float half_width = aspect * half_height;

            LowerLeftCorner = new Vec3f(-half_width, -half_height, -1.0f);
            Horizontal = new Vec3f(2 * half_width, 0f, 0f);
            Vertical = new Vec3f(0f, 2 * half_height, 0f);
            Origin = Vec3f.Zero;

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
