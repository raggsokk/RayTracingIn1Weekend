using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RayTracingIn1Weekend.Week1
{
    public class HitableList : HitableBase
    {
        public List<HitableBase> HitList;

        public HitableList(params object[] hitable)
        {
            HitList = new List<HitableBase>();

            foreach (var o in hitable)
                if(o is HitableBase h)
                    HitList.Add(h);
        }

        public override bool Hit(Ray r, float tMin, float tMax, ref HitRecord record)
        {
            HitRecord tmpRecord = new HitRecord();

            bool flagHitAnything = false;
            float closest_so_far = tMax;

            foreach(var h in HitList)
            {
                if(h.Hit(r, tMin, closest_so_far, ref tmpRecord))
                {
                    flagHitAnything = true;
                    closest_so_far = tmpRecord.t;
                    record = tmpRecord;
                }
            }

            return flagHitAnything;

            //throw new NotImplementedException();
        }
    }
}
