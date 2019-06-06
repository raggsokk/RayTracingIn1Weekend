using System;

namespace RayTracingIn1Weekend.Week1
{
    public class RayTrace1
    {
        public static Vec3f RandomInUnitSphere(Random drand)
        {
            Vec3f p;// = Vec3f.Zero;

            do
            {
                p = 2.0f * new Vec3f((float)drand.NextDouble(), (float)drand.NextDouble(), (float)drand.NextDouble()) - Vec3f.One;
            } while (p.GetLengthSquared() >= 1.0f);

            return p;
        }

        private static Vec3f Color(Ray r, HitableList world, int depth, Random drand)
        {
            HitRecord record = new HitRecord();

            if(world.Hit(r, 0.0001f, float.MaxValue, ref record))
            {
                Ray scattered = new Ray();
                Vec3f attenuation = Vec3f.Zero;
                
                if(depth < 50 && record.Material.Scatter(r, record, out attenuation, out scattered, drand))
                {
                    return attenuation * Color(scattered, world, depth +1, drand);
                }
                else {
                    return Vec3f.Zero;
                }

                // last chapters color code.
                //Vec3f target = record.Point + record.Normal + RandomInUnitSphere(drand);
                //return 0.5f * Color(new Ray(record.Point, target - record.Point), world, drand);
                //Vec3f N = record.Normal;
                //return 0.5f * new Vec3f(N.X + 1, N.Y + 1, N.Z + 1);
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
            int samples = 100;

            //Vec3f lower_left_corner = new Vec3f(-2.0f, -1.0f, -1.0f);
            //Vec3f horizontal = new Vec3f(4.0f, 0.0f, 0.0f);
            //Vec3f vertical = new Vec3f(0.0f, 2.0f, 0.0f);
            //Vec3f origin = new Vec3f(0.0f, 0.0f, 0.0f);

            var world = new HitableList(
                new Sphere(new Vec3f(0f, 0f, -1f), 0.5f, new Lambertian(new Vec3f(0.1f, 0.2f, 0.5f))),
                new Sphere(new Vec3f(0, -100.5f, -1f), 100, new Lambertian(new Vec3f(0.8f, 0.8f, 0.0f))),
                new Sphere(new Vec3f(1f, 0f, -1f), 0.5f, new Metal(new Vec3f(0.8f, 0.6f, 0.2f), 0.0f)),
                new Sphere(new Vec3f(-1f, 0f, -1f), 0.5f, new Dielectric(1.5f)),
                new Sphere(new Vec3f(-1f, 0f, -1f), -0.45f, new Dielectric(1.5f))
                );
            float R = MathF.Cos(MathF.PI / 4);
            //var cam = new Camera(90, (float)width / (float)height);
            var cam = new Camera(new Vec3f(-2f, 2f, 1f), -Vec3f.UnitZ, 
                Vec3f.UnitY, 20, (float)width / (float)height);

            //var world = new HitableList(
            //        new Sphere(new Vec3f(0, -100.5f, -1f), 100, new Lambertian(new Vec3f(0.8f, 0.8f, 0.0f))),
            //        new Sphere(new Vec3f(-R, 0, -1), R, new Lambertian(Vec3f.UnitZ)),
            //        new Sphere(new Vec3f( R, 0,-1), R, new Lambertian(Vec3f.UnitX))
            //    );

            var drand = new Random();

            int cur = 0;

            for(int j = height-1; j>= 0; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    Vec3f col = Vec3f.Zero;

                    for(int s = 0; s < samples; s++)
                    {
                        float u = (float)(i + drand.NextDouble()) / (float)width;
                        float v = (float)(j + drand.NextDouble()) / (float)height;

                        Ray r = cam.GetRay(u, v);
                        //Vec3f p = r.PointAtParameter(2.0f); // still not used.

                        col += Color(r, world, 0, drand);
                    }

                    col /= (float)samples;
                    // gamma correction ??
                    col = new Vec3f(MathF.Sqrt(col.X), MathF.Sqrt(col.Y), MathF.Sqrt(col.Z));
                    img.Pixels[cur++] = (Rgb3f)col;

                    //img.Pixels[cur++] = new Rgb3f() { R = r, G = g, B = b };
                }
            }

            return img;
        }
    }
}
