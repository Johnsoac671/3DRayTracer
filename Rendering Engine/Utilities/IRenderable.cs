using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Utilities
{
    internal interface IRenderable
    {
        public bool IsHit(Ray r, Interval rayT, ref HitRecord record);
    }
}
