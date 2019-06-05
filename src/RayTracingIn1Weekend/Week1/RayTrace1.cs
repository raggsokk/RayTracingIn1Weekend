﻿using System;

namespace RayTracingIn1Weekend.Week1
{
    public class RayTrace1
    {

        private static Vec3f Color(Ray r, HitableList world)
        {
            HitRecord record = new HitRecord();

            if(world.Hit(r, 0.0f, float.MaxValue, ref record))
            {
                Vec3f N = record.Normal;
                return 0.5f * new Vec3f(N.X + 1, N.Y + 1, N.Z + 1);
            }
            else
            {
                Vec3f unit_direction = r.Direction.GetNormal();
                float t = 0.5f * (unit_direction.Y + 1.0f);

                return (1.0f - t) * Vec3f.One + t * (new Vec3f(0.5f, 0.7f, 1.0f));

            }
        }

        public static RayImage Render(int width, int height)
        {
            // As simple as possible code for now.
            var img = new RayImage();
            img.Width = width;
            img.Height = height;
            img.Pixels = new Rgb3f[width * height];

            Vec3f lower_left_corner = new Vec3f(-2.0f, -1.0f, -1.0f);
            Vec3f horizontal = new Vec3f(4.0f, 0.0f, 0.0f);
            Vec3f vertical = new Vec3f(0.0f, 2.0f, 0.0f);
            Vec3f origin = new Vec3f(0.0f, 0.0f, 0.0f);

            var world = new HitableList(
                new Sphere(new Vec3f(0f, 0f, -1f), 0.5f),
                new Sphere(new Vec3f(0, -100.5f, -1f), 100)
                ) ;

            int cur = 0;

            for(int j = height-1; j>= 0; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    float u = (float)i / (float)width;
                    float v = (float)j / (float)height;

                    Ray r = new Ray(origin, lower_left_corner + u * horizontal + v * vertical);

                    //Vec3f p = r.PointAtParameter(2.0f);

                    Vec3f col = Color(r, world);

                    img.Pixels[cur++] = (Rgb3f)col;

                    //img.Pixels[cur++] = new Rgb3f() { R = r, G = g, B = b };
                }
            }

            return img;
        }
    }
}
