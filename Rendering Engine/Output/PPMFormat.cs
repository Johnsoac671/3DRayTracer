using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Output
{
    public class PPMFormat : Format
    {
        private StreamWriter? writer;

        public PPMFormat(){ }

        public override void CreateImage(int height, int width)
        {
            this.writer = new StreamWriter("output.ppm");

            this.writer.WriteLine("P3");
            this.writer.WriteLine($"{width} {height}");
            this.writer.WriteLine("255");
        }

        public override void Save()
        {
            this.writer.Close();
        }

        public override void Write(int[] rgb)
        {
            this.writer.WriteLine($"{rgb[0]} {rgb[1]} {rgb[2]}");
        }
    }
}
