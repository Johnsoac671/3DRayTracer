using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    public class Metal : Material
    {
        public Color3 Albedo { get; }
        public double Roughness {  get; }

        public Metal(Color3 albedo, double roughness = 0.0)
        {
            this.Albedo = albedo;
            this.Roughness = roughness < 1 ? roughness : 1;
        }

        public override Ray Scatter(Ray r, HitRecord record, ref Color3 attenunation)
        {
            Vector3 reflected = Vector3.Reflect(r.Direction, record.Normal);
            reflected = Vector3.UnitVector(reflected) + (this.Roughness * Vector3.RandomUnitVector());
            Ray scatteredRay = new Ray(record.Location, reflected);
            attenunation = this.Albedo;

            return scatteredRay;

        }
    }
}
