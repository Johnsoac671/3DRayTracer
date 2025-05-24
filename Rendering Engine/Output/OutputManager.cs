using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Output
{
    internal class OutputManager
    {
        private Format outputFormat;

        public OutputManager()
        {
            this.outputFormat = new PPMFormat();
        }

        public void WriteOutput(int height, int width, int[][] pixels)
        {
            this.outputFormat.CreateImage(height, width);

            foreach (var pixel in pixels)
            {
                outputFormat.Write(pixel);
            }

            outputFormat.Save();
        }
    }
}
