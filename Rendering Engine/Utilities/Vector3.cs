global using Point3 = Rendering_Engine.Utilities.Vector3;
global using Color3 = Rendering_Engine.Utilities.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Utilities
{
    
    public class Vector3
    {

        private double[] values;
        private static Random Random = new Random();

        public Vector3(double x, double y, double z)
        {
            this.values = [x, y, z];
        }

        public Vector3()
        {
            this.values = [Random.NextDouble(), Random.NextDouble(), Random.NextDouble()];
        }

        public Vector3(double min, double max)
        {
            double range = max - min;
            this.values = [Random.NextDouble() * range + min, Random.NextDouble() * range + min, Random.NextDouble() * range + min];
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
            // Convert to Gamma space
            double r = this.X > 0 ? Math.Sqrt(this.X) : 0;
            double g = this.Y > 0 ? Math.Sqrt(this.Y) : 0;
            double b = this.Z > 0 ? Math.Sqrt(this.Z) : 0;

            int ir = Math.Clamp((int)(r * 255.999), 0, 255);
            int ig = Math.Clamp((int)(g * 255.999), 0, 255);
            int ib = Math.Clamp((int)(b * 255.999), 0, 255);

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

        public static Vector3 RandomUnitVector()
        {
            while (true)
            {
                Vector3 candidate = new Vector3(-1, 1);
                double squaredLength = candidate.SquaredLength;

                if (squaredLength <= 1 && 1e-160 < squaredLength)
                {
                    return candidate/Math.Sqrt(squaredLength);
                }
            }
        }

        public static Vector3 RandomVectorOnHemisphere(Vector3 normal)
        {
            Vector3 randomUnit = RandomUnitVector();
            
            if (Dot(normal, randomUnit) > 0)
            {
                return randomUnit;
            }
            else
            {
                return -randomUnit;
            }
        }

        public static Vector3 Reflect(Vector3 v,  Vector3 normal)
        {
            return v - 2 * Dot(v, normal) * normal;
        }

        public static Vector3 Refract(Vector3 uv, Vector3 normal, double eta)
        {
            double cosTheta = Math.Min(Dot(-uv, normal), 1.0);
            Vector3 rPerpendicular = eta * (uv + cosTheta * normal);
            Vector3 rParallel = -Math.Sqrt(Math.Abs(1.0 - rPerpendicular.SquaredLength)) * normal;

            return rParallel + rPerpendicular;

        }

        public static bool IsNearZero(Vector3 v)
        {
            var nearPoint = 1e-8;
            return Math.Abs(v.X) < nearPoint && Math.Abs(v.Y) < nearPoint && Math.Abs(v.Z) < nearPoint;
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
