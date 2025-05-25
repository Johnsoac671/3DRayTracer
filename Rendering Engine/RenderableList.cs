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

        public bool IsHit(Ray r, double tmin, double tmax, ref HitRecord record)
        {
            HitRecord temp = new HitRecord();
            bool hit = false;
            double closest = tmax;

            foreach (IRenderable obj in Objects)
            {
                if (obj.IsHit(r, tmin, closest, ref temp))
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
