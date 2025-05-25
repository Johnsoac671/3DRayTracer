using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rendering_Engine.Utilities
{
    public class Ray
    {
        private Point3 origin;
        private Vector3 direction;

        public Ray(Vector3 origin, Vector3 direction)
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
            return this.origin + (this.direction * time);
        }
    }
}
