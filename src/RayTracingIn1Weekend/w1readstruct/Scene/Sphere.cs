using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend.w1readstruct.Math;
using RayTracingIn1Weekend.w1readstruct.Materials;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Scene
{
    [DebuggerDisplay("O={Center}, R={Radius}")]
    public class Sphere : HitableBase
    {
        public Vec3f Center;
        public float Radius;

        public MaterialBase Material;

        public Sphere(in Vec3f center, float r, MaterialBase m)
        {
            this.Center = center;
            this.Radius = r;
            this.Material = m;
        }

        public override bool Hit(in Rayf r, float tMin, float tMax, ref HitRecord record)
        {
            Vec3f oc = r.Origin - Center;

            float a = Dot(r.Direction, r.Direction);
            float b = Dot(oc, r.Direction);
            float c = Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - a * c;

            if (discriminant > 0)
            {
                //float tmpSqrt = MathF.Sqrt(b * b - a * c);
                float tmpSqrt = MathF.Sqrt(discriminant);
                float temp = (-b - tmpSqrt) / a;

                if (temp < tMax && temp > tMin)
                {
                    record.t = temp;
                    record.Point = r.PointAtParameter(record.t);
                    record.Normal = (record.Point - Center) / Radius;
                    record.Material = this.Material;
                    return true;
                }

                temp = (-b + tmpSqrt) / a;
                if (temp < tMax && temp > tMin)
                {
                    record.t = temp;
                    record.Point = r.PointAtParameter(record.t);
                    record.Normal = (record.Point - Center) / Radius;
                    record.Material = this.Material;
                    return true;
                }
            }
            return false;
        }

        //public override bool Hit(in Rayf r, float tMin, float tMax, ref HitRecord record)
        //{
        //    Vec3f oc = r.Origin - Center;

        //    float a = Dot(r.Direction, r.Direction);
        //    float b = Dot(oc, r.Direction);
        //    float c = Dot(oc, oc) - Radius * Radius;
        //    float discriminant = b * b - a * c;

        //    if(discriminant > 0)
        //    {
        //        //float tmpSqrt = MathF.Sqrt(b * b - a * c);
        //        float tmpSqrt = MathF.Sqrt(discriminant);
        //        float temp = (-b - tmpSqrt) / a;

        //        if(temp < tMax && temp > tMin)
        //        {
        //            record = new HitRecord(
        //                    temp,
        //                    r.PointAtParameter(record.t),
        //                    (record.Point - Center) / Radius,
        //                    Material
        //                );
        //            return true;
        //        }

        //        temp = (-b + tmpSqrt) / a;
        //        if(temp < tMax && temp > tMin)
        //        {
        //            record = new HitRecord(
        //                    temp,
        //                    r.PointAtParameter(record.t),
        //                    (record.Point - Center) / Radius,
        //                    Material
        //                );
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
