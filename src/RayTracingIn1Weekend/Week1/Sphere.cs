using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{    
    [DebuggerDisplay("O={Center}, R={Radius}")]
    public class Sphere : HitableBase
    {
        public Vec3f Center;
        public float Radius;

        public Sphere(Vec3f center, float r)
        {
            this.Center = center;
            this.Radius = r;
        }

        public override bool Hit(Ray r, float tMin, float tMax, ref HitRecord record)
        {
            Vec3f oc = r.Origin - Center;

            float a = Vec3f.Dot(r.Direction, r.Direction);
            float b = Vec3f.Dot(oc, r.Direction);
            float c = Vec3f.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - a * c;

            if(discriminant > 0)
            {
                float tempSqrt = MathF.Sqrt(b * b - a * c);
                float temp = (-b - tempSqrt) / a;

                if (temp < tMax && temp > tMin)
                {
                    record.t = temp;
                    record.Point = r.PointAtParameter(record.t);
                    record.Normal = (record.Point - Center) / Radius;
                    return true;
                }

                temp = (-b + tempSqrt) / a;
                if(temp < tMax && temp > tMin)
                {
                    record.t = temp;
                    record.Point = r.PointAtParameter(record.t);
                    record.Normal = (record.Point - Center) / Radius;
                    return true;
                }
            }

            return false;            
        }
    }
}
