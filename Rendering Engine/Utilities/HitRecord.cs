using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Utilities
{
    internal class HitRecord
    {
        private Point3 location;
        private Vector3 normal;
        private double time;

        public HitRecord(Point3 location, Vector3 normal, double time)
        {
            this.location = location;
            this.normal = normal;
            this.time = time;
        }

        public Point3 Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public Vector3 Normal
        {
            get
            {
                return this.normal;
            }
            set
            {
                this.normal = value;
            }
        }

        public double Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }
    }
}
