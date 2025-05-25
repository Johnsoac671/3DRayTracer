using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{
    internal class RenderableList : IRenderable
    {
        public List<IRenderable> Objects {  get; private set; }

        public RenderableList()
        {
            Objects = new List<IRenderable>();
        }

        public void Add(IRenderable obj)
        {
            Objects.Add(obj);
        }

        public bool IsHit(Ray r, Interval rayT, ref HitRecord record)
        {
            HitRecord temp = new HitRecord();
            bool hit = false;
            double closest = rayT.Max;

            foreach (IRenderable obj in Objects)
            {
                if (obj.IsHit(r, new Interval(rayT.Min, closest), ref temp))
                {
                    hit = true;
                    closest = temp.Time;
                    record = temp;

                }
            }

            return hit;
        }
    }
}
