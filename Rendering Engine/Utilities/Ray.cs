using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rendering_Engine.Utilities
{
    /// <summary>
    /// A class representing a light ray in the scene
    /// </summary>
    public class Ray
    {
        private Point3 origin;
        private Vector3 direction;

        public Ray(Point3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public Point3 Origin
            {
                get
                {
                    return this.origin;
                }
            }

        public Vector3 Direction
        {
            get
            {
                return this.direction;
            }
        }

        public Point3 PointAtTime(double time)
        {
            return (Point3)(this.origin + (this.direction * time));
        }
    }
}
