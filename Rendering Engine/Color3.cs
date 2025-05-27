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
    }
}
