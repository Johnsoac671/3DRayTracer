using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    internal class Diffuse : Material
    {
        private Color3 albedo;
        public Diffuse(Color3 albedo)
        {
            this.albedo = albedo;
        }

        public Color3 Albedo
        {
            get
            {
                return albedo;
            }
        }

        public override Ray Scatter(Ray r, HitRecord record, ref Color3 attenuation)
        {
            Vector3 scatterDirection = record.Normal + Vector3.RandomUnitVector();

            if (Vector3.IsNearZero(scatterDirection))
            {
                scatterDirection = record.Normal;
            }

            Ray scatteredRay = new Ray(record.Location, scatterDirection);
            attenuation = this.Albedo;

            return scatteredRay;
        }
    }
}
