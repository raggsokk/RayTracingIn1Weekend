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

            }
            else
            {
                Vec3f unit_direction = r.Direction.GetNormal();
                float t = 0.5f * (unit_direction.Y + 1.0f);

                return (1.0f - t) * Vec3f.One + t * (new Vec3f(0.5f, 0.7f, 1.0f));

            }
        }

        public static HitableList RandomScene(Random drand)
        {
            int n = 500;

            var world = new HitableList();

            world.Add(new Sphere(new Vec3f(0, -1000f, 0), 1000, new Lambertian(new Vec3f(0.5f, 0.5f, 0.5f))));

            int i = 1;
            for(int a = -11; a < 11;a++)
            {
                for(int b = -11; b < 11;b++)
                {
                    float choose_mat = (float)drand.NextDouble();
                    var center = new Vec3f(a + 0.9f * (float)drand.NextDouble(), 0.2f, b + 0.9f * (float)drand.NextDouble());

                    if((center - new Vec3f(4f, 0.2f, 0)).GetLength() > 0.9f)
                    {
                        if(choose_mat < 0.8f) // diffuse
                        {
                            world.Add(new Sphere(center, 0.2f, new Lambertian(new Vec3f((float)drand.NextDouble() * (float)drand.NextDouble(),
                                (float)drand.NextDouble() * (float)drand.NextDouble(), (float)drand.NextDouble() * (float)drand.NextDouble()))));
                        }
                        else if(choose_mat < 0.95f) // metal
                        {
                            var o = new Vec3f(
                                0.5f * (1f + (float)drand.NextDouble()),
                                0.5f * (1f + (float)drand.NextDouble()),
                                0.5f * (1f + (float)drand.NextDouble())
                                );
                            world.Add(new Sphere(center, 0.2f, new Metal(o, 0.5f * (float)drand.NextDouble())));
                        }
                        else // glass
                        {
                            world.Add(new Sphere(center, 0.2f, new Dielectric(1.5f)));
                        }
                    }
                }
            }

            world.Add(new Sphere(Vec3f.UnitY, 1.0f, new Dielectric(1.5f)));
            world.Add(new Sphere(new Vec3f(-4f, 1f, 0), 1.0f, new Lambertian(new Vec3f(0.4f, 0.2f, 0.1f))));
            world.Add(new Sphere(new Vec3f(4f, 1f, 0), 1.0f, new Metal(new Vec3f(0.7f, 0.6f, 0.5f), 0.0f)));

            return world;
        }

        public static RayImage Render(int width, int height, int samples)
        {
            // As simple as possible code for now.
            var img = new RayImage();
            img.Width = width;
            img.Height = height;
            img.Pixels = new Rgb3f[width * height];
            //int samples = 50;

            var lookFrom = new Vec3f(5f, 5f, 5f);
            var lookAt = new Vec3f(0, 0, -1f);
            float dist_to_focus = (lookFrom - lookAt).GetLength();
            float aperture = 2.0f;

            var cam = new Camera(lookFrom, lookAt, Vec3f.UnitY,
                40, (float)width / (float)height, aperture, dist_to_focus);

            var drand = new Random();

            var world = RandomScene(drand);

            //var world = new HitableList(
            //    new Sphere(new Vec3f(0f, 0f, -1f), 0.5f, new Lambertian(new Vec3f(0.1f, 0.2f, 0.5f))),
            //    new Sphere(new Vec3f(0, -100.5f, -1f), 100, new Lambertian(new Vec3f(0.8f, 0.8f, 0.0f))),
            //    new Sphere(new Vec3f(1f, 0f, -1f), 0.5f, new Metal(new Vec3f(0.8f, 0.6f, 0.2f), 0.0f)),
            //    new Sphere(new Vec3f(-1f, 0f, -1f), 0.5f, new Dielectric(1.5f)),
            //    new Sphere(new Vec3f(-1f, 0f, -1f), -0.45f, new Dielectric(1.5f))
            //    );

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

                        Ray r = cam.GetRay(u, v, drand);
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
