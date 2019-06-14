using System;
using System.Threading;
using System.Threading.Tasks;

using RayTracingIn1Weekend.w1readstruct.Math;
using RayTracingIn1Weekend.w1readstruct.Materials;
using RayTracingIn1Weekend.w1readstruct.Scene;


using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct
{
    public class RayTrace1
    {
        public static Vec3f RandomUnitSphere(Random drand)
        {
            Vec3f p;

            do
            {
                p = 2.0f * Vec3(drand.NextFloat(), drand.NextFloat(), drand.NextFloat()) - Vec3f.One;

            } while (GetLengthSquared(p) >= 1.0f);

            return p;
        }

        private static Vec3f Color(in Rayf r, in HitableList world, int depth, Random drand)
        {
            HitRecord record = new HitRecord();

            if(world.Hit(r, 0.0001f, float.MaxValue, ref record))
            {
                Rayf scattered = new Rayf();
                Vec3f attenuation = Vec3f.Zero;                

                if(depth < 50 && record.Material.Scatter(r, record, out attenuation, out scattered, drand))
                {
                    //return attenuation * Color()
                    return attenuation * Color(scattered, world, depth + 1, drand);
                }
                else
                {
                    return Vec3f.Zero;
                }
            }
            else
            {
                Vec3f unit_direction = GetNormal(r.Direction);
                float t = 0.5f * (unit_direction.Y + 1.0f);

                return (1.0f - t) * Vec3f.One + t * (new Vec3f(0.5f, 0.7f, 1.0f));
            }
        }

        public static HitableList RandomScene(Random drand)
        {
            int n = 500;

            var world = new HitableList();

            world.Add(new Sphere(new Vec3f(0, -1000f, 0), 1000, new Lambertian(new Vec3f(0.5f, 0.5f, 0.5f))));

            for(int a = -11; a < 11; a++)
            {
                for(int b = -11; b < 11;b++)
                {
                    float choose_mat = drand.NextFloat();
                    var center = new Vec3f(a + 0.9f * drand.NextFloat(), 0.2f, b + 0.9f * drand.NextFloat());

                    if(GetLength(center - new Vec3f(4f, 0.2f, 0)) > 0.9f)
                    {
                        if(choose_mat < 0.8f) // diffuse
                        {
                            world.Add(new Sphere(center, 0.2f, new Lambertian(
                                new Vec3f(drand.NextFloat() * drand.NextFloat(), drand.NextFloat() * drand.NextFloat(), drand.NextFloat() * drand.NextFloat()))));
                        }
                        else if(choose_mat < 0.95f) // metal
                        {
                            var o = new Vec3f(
                                0.5f * (1f + drand.NextFloat()),
                                0.5f * (1f + drand.NextFloat()),
                                0.5f * (1f + drand.NextFloat())
                                );

                            world.Add(new Sphere(center, 0.2f, new Metal(o, 0.5f * drand.NextFloat())));
                        }
                        else // glass
                        {
                            world.Add(new Sphere(center, 0.2f, new Dielectric(1.5f)));
                        }

                    }
                }
            }

            //world.Add(new Sphere(Vec3f.UnitY, 1.0f, new))
            world.Add(new Sphere(Vec3f.UnitY, 1.0f, new Dielectric(1.5f)));
            world.Add(new Sphere(new Vec3f(-4f, 1f, 0), 1.0f, new Lambertian(new Vec3f(0.4f, 0.2f, 0.1f))));
            world.Add(new Sphere(new Vec3f(4f, 1f, 0), 1.0f, new Metal(new Vec3f(0.7f, 0.6f, 0.5f), 0.0f)));

            return world;
        }

        public static RayImage Render(int width, int height, int samples)
        {
            var img = new RayImage();
            img.Width = width;
            img.Height = height;
            img.Pixels = new Rgb3f[width * height];

            var lookFrom = new Vec3f(5f, 5f, 5f);
            var lookAt = -Vec3f.UnitZ;
            float dist_to_focus = GetLength(lookFrom - lookAt);
            float aperture = 2.0f;

            var cam = new Camera(lookFrom, lookAt, Vec3f.UnitY,
                40, (float)width / (float)height, aperture, dist_to_focus);

            var drand = new Random(12345);
            var world = RandomScene(drand);

            int cur = 0;

            for(int j = height - 1; j >= 0; j--)
            {
                for(int i = 0; i < width; i++)
                {
                    Vec3f col = Vec3f.Zero;

                    for(int s = 0; s < samples; s++)
                    {
                        float u = (float)(i + drand.NextFloat()) / (float)width;
                        float v = (j + drand.NextFloat()) / (float)height;

                        Rayf r = cam.GetRay(u, v, drand);

                        col += Color(r, world, 0, drand);
                    }

                    col /= (float)samples;

                    col = new Vec3f(MathF.Sqrt(col.X), MathF.Sqrt(col.Y), MathF.Sqrt(col.Z));
                    img.Pixels[cur++] = new Rgb3f(col.X, col.Y, col.Z);
                }
            }

            return img;            
        }
    }
}
