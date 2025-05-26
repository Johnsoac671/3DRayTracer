using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    internal class Metal : Material
    {
        public Color3 Albedo { get; }

        public Metal(Color3 albedo)
        {
            this.Albedo = albedo;
        }

        public override Ray Scatter(Ray r, HitRecord record, ref Color3 attenunation)
        {
            Vector3 reflected = Vector3.Reflect(r.Direction, record.Normal);
            Ray scatteredRay = new Ray(record.Location, reflected);
            attenunation = this.Albedo;

            return scatteredRay;

        }
    }
}
