using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Utilities
{
    public class Color3 : Vector3
    {
        public double R
        {
            get 
            {
                return this.X;
            }
            set
            { 

                this.X = value;
            }
        }

        public double G
        {
            get
            {
                return this.Y;
            }
            set
            {

                this.Y = value;
            }
        }

        public double B
        {
            get
            {
                return this.Z;
            }
            set
            {

                this.Z = value;
            }
        }

        public Color3(double r, double g, double b) : base(r, g, b)
        {
        

        }

        public static Color3 Black => new(0, 0, 0);
        public static Color3 White => new(1, 1, 1);
        public static Color3 Red => new(1, 0, 0);
        public static Color3 Green => new(0, 1, 0);
        public static Color3 Blue => new(0, 0, 1);
        public static Color3 LightBlue => new(0.5, 0.7, 1.0);

        public override int[] ToRGB()
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

        public static Color3 operator -(Color3 v)
        {
            return new Color3(-v.X, -v.Y, -v.Z);
        }

        public static Color3 operator +(Color3 v1, Color3 v2)
        {
            return new Color3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Color3 operator -(Color3 v1, Color3 v2)
        {
            return new Color3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Color3 operator *(Color3 v1, Color3 v2)
        {
            return new Color3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }


        public static Color3 operator *(double t, Color3 v)
        {
            return new Color3(v.X * t, v.Y * t, v.Z * t);
        }

        public static Color3 operator *(Color3 v, double t)
        {
            return t * v;
        }

        public static Color3 operator /(Color3 v, double t)
        {
            return (1 / t) * v;
        }
    }
}
