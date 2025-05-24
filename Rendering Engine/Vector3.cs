using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{

    public class Vector3
    {
        private double[] values;

        public Vector3(double x, double y, double z)
        {
            this.values = [x, y, z];
        }



        public double X
        {
            get
            {
                return this.values[0]; 
            }

            set
            {
                this.values[0] = value;
            }
        }

        public double Y
        {
            get
            {
                return this.values[1];
            }

            set
            {
                this.values[1] = value;
            }
        }

        public double Z
        {
            get
            {
                return this.values[2];
            }

            set
            {
                this.values[2] = value;
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.SquaredLength);
            }
        }

        public double SquaredLength
        {
            get
            {
                return Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2);
            }
        }

        public int[] ToRGB()
        {
            int ir = Math.Clamp((int)(this.X * 255.999), 0, 255);
            int ig = Math.Clamp((int)(this.Y * 255.999), 0, 255);
            int ib = Math.Clamp((int)(this.Z * 255.999), 0, 255);

            return new int[] { ir, ig, ib };
        }



        public static double Dot(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X);
        }

        public static Vector3 UnitVector(Vector3 v)
        {
            return v / v.Length;
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }


        public static Vector3 operator *(double t, Vector3 v)
        {
            return new Vector3(v.X * t, v.Y * t, v.Z * t);
        }

        public static Vector3 operator *(Vector3 v, double t)
        {
            return t * v;
        }

        public static Vector3 operator /(Vector3 v, double t)
        {
            return (1/t) * v;
        }
    }
}
