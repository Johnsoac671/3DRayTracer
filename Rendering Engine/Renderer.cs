using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Output;

namespace Rendering_Engine
{
    public class Renderer
    {
        private int width;
        private int height;
        private OutputManager outputManager;

        public Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.outputManager = new OutputManager();
        }

        public void Render()
        {
            Console.WriteLine("Rendering image...");
            int[][] pixels = new int[width * height][];

            for (int j = 0; j < this.width; j++)
            {
                for (int i = 0; i < this.height; i++)
                {
                    double r = (double)i / (this.width - 1);
                    double g = (double)j / (this.height - 1);
                    double b = 0;

                    int ir = (int)(r * 255.999);
                    int ig = (int)(g * 255.999);
                    int ib = (int)(b * 255.999);

                    int[] pixel = [ir, ig, ib];
                    pixels[i * this.width + j] = pixel;
                }

            }

            this.outputManager.WriteOutput(this.height, this.width, pixels);
        }
    }
}
