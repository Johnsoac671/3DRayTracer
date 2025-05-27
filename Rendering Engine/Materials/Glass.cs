using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    public class Glass : Material
    {
        public double IOR { get; }
        public Glass(double indexOfRefraction)
        {
            this.IOR = indexOfRefraction;
        }

        public override Ray Scatter(Ray ray, HitRecord record, ref Color3 attenuation)
        {
            attenuation = new Color3(1.0, 1.0, 1.0);
            double ri = record.IsFrontFace ? (1.0 / this.IOR) : this.IOR;

            Vector3 unit = Vector3.UnitVector(ray.Direction);
            Vector3 refracted = Vector3.Refract(unit, record.Normal, ri);

            return new Ray(record.Location, refracted);
        }
    }
}
