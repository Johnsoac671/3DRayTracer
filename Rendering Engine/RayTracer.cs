using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Materials;
using Rendering_Engine.Output;
using Rendering_Engine.Primitives;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{

    /// <summary>
    /// Handles the core ray tracing logic.
    /// This class is responsible for calculating the color of each ray by tracing its path through the scene.
    /// </summary>
    public class RayTracer
    {
        private RenderSettings renderSettings;

        public RayTracer(RenderSettings renderSettings)
        {
            this.renderSettings = renderSettings;
        }

        /// <summary>
        /// Recursively calculates the color of a ray.
        /// The method traces the ray through the scene, and each time it hits an object,
        /// it scatters the ray based on the material properties until the maximum depth is reached.
        /// </summary>
        public Color3 CalculateRayColor(Ray ray, int depth, IRenderable world)
        {
            if (depth >= renderSettings.MaxDepth)
            {
                return Color3.Black;
            }

            HitRecord record = new HitRecord();
            if (world.IsHit(ray, new Interval(0.001, double.PositiveInfinity), ref record))
            {
                Color3 attenuation = Color3.Black;
                Ray scatteredRay = record.Material.Scatter(ray, record, ref attenuation);

                return attenuation * CalculateRayColor(scatteredRay, depth + 1, world);
            }

            // Background gradient (sky)
            Vector3 unitDirection = Vector3.UnitVector(ray.Direction);
            double a = 0.5 * (unitDirection.Y + 1.0);

            return (1.0 - a) * Color3.White + a * Color3.LightBlue;
        }
    }
}