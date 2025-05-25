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
        private bool frontFace; 

        public HitRecord(Point3 location, Vector3 normal, double time)
        {
            this.location = location;
            this.normal = normal;
            this.time = time;
        }

        public HitRecord()
        {

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

        public void SetFaceNormal(Ray r, Vector3 outwardNormal)
        {
            this.frontFace = Vector3.Dot(r.Direction, outwardNormal) < 0;
            this.normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }
}
