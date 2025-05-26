using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    public abstract class Material
    {
        public abstract Ray Scatter(Ray r, HitRecord record, ref Color3 attenuation);
    }
}
