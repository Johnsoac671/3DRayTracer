using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Materials;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Primitives
{
    /// <summary>
    /// Represents a sphere object in the scene.
    /// </summary>
    public class Sphere : IRenderable
    {
        private Point3 center;
        private double radius;
        private Material material;

        public Sphere(Point3 center, double radius, Material material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;
        }

        /// <summary>
        /// Determines if a ray intersects with the sphere.
        /// This method uses the ray-sphere intersection formula to find the hit point.
        /// </summary>
        public bool IsHit(Ray r, Interval rayT, ref HitRecord record)
        {
            Vector3 oc = this.center - r.Origin;
            var a = r.Direction.SquaredLength;
            var h = Vector3.Dot(r.Direction, oc);
            var c = oc.SquaredLength - radius * radius;

            var discriminant = h*h - a*c;

            if (discriminant < 0)
            {
                return false;
            }

            var sqrtD = Math.Sqrt(discriminant);

            var root = (h - sqrtD)/a;

            if (!rayT.Surrounds(root))
            {
                root = (h + sqrtD) / a;
                if (!rayT.Surrounds(root))
                {
                    return false;
                }
            }

            record.Time = root;
            record.Location = r.PointAtTime(root);
            record.Material = this.material;

            Vector3 outwardNormal = (record.Location - center) / radius;
            record.SetFaceNormal(r, outwardNormal);

            return true;
            

        }
    }
}
