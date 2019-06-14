using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

using RayTracingIn1Weekend.w1readstruct.Math;
using RayTracingIn1Weekend.w1readstruct.Materials;
using RayTracingIn1Weekend.w1readstruct.Scene;

using static RayTracingIn1Weekend.w1readstruct.Math.VecMath;

namespace RayTracingIn1Weekend.w1readstruct.Scene
{
    public class HitableList : HitableBase
    {
        protected List<HitableBase> HitList;
        protected HitableBase[] HitArray;

        public HitableList(params object[] hitable)
        {
            HitList = new List<HitableBase>();

            foreach (var o in hitable)
                if (o is HitableBase h)
                    HitList.Add(h);
        }

        public override bool Hit(in Rayf r, float tMin, float tMax, ref HitRecord record)
        {
            if (HitArray == null)
                HitArray = HitList.ToArray();

            HitRecord tmpRecord = new HitRecord();

            bool flagHitAnything = false;
            float closes_so_far = tMax;

            foreach(var h in HitArray)
            {
                if(h.Hit(r, tMin, closes_so_far, ref tmpRecord))
                {
                    flagHitAnything = true;
                    closes_so_far = tmpRecord.t;
                    record = tmpRecord;
                }
            }

            return flagHitAnything;
        }

        public void Add(HitableBase hitable)
        {
            this.HitList.Add(hitable);
        }
    }
}
