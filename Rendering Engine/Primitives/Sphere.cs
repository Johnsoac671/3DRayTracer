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
        private Ray center;
        private double radius;
        private Material material;

        public Sphere(Point3 center, double radius, Material material)
        {
            this.center = new Ray(center, new Vector3(0, 0, 0));
            this.radius = radius;
            this.material = material;
        }

        public Sphere(Point3 startLocation, Point3 endLocation, double radius, Material material)
        {
            this.center = new Ray(startLocation, endLocation - startLocation);
            this.radius = radius;
            this.material = material;
        }

        /// <summary>
        /// Determines if a ray intersects with the sphere.
        /// This method uses the ray-sphere intersection formula to find the hit point.
        /// </summary>
        public bool IsHit(Ray r, Interval rayT, ref HitRecord record)
        {
            Point3 currentLocation = this.center.PointAtTime(r.Time);
            Vector3 oc = currentLocation - r.Origin;
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

            Vector3 outwardNormal = (record.Location - currentLocation) / radius;
            record.SetFaceNormal(r, outwardNormal);

            return true;
            

        }
    }
}
