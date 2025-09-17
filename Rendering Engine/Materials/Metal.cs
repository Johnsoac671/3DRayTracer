using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    /// <summary>
    /// Represents a reflective, metallic material.
    /// </summary>
    public class Metal : Material
    {
        public Color3 Albedo { get; }
        public double Roughness {  get; }

        public Metal(Color3 albedo, double roughness = 0.0)
        {
            this.Albedo = albedo;
            this.Roughness = roughness < 1 ? roughness : 1;
        }

        /// <summary>
        /// Calculates the scattered ray for a metal material.
        /// The ray is reflected off the surface, with the reflection direction
        /// perturbed by the roughness of the material.
        /// </summary>
        public override Ray Scatter(Ray ray, HitRecord record, ref Color3 attenunation)
        {
            Vector3 reflected = Vector3.Reflect(ray.Direction, record.Normal);
            reflected = Vector3.UnitVector(reflected) + (this.Roughness * Vector3.RandomUnitVector());
            Ray scatteredRay = new Ray(record.Location, reflected, ray.Time);
            attenunation = this.Albedo;

            return scatteredRay;

        }
    }
}
