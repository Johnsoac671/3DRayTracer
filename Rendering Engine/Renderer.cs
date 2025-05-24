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
                Console.WriteLine($"Rendering Line: {j+1}");
                for (int i = 0; i < this.height; i++)
                {

                    Vector3 color = new Vector3((double)i / (this.width - 1), (double)j / (this.height - 1), 0);

                    int[] pixel = color.ToRGB();
                    pixels[i * this.width + j] = pixel;
                }

            }

            this.outputManager.WriteOutput(this.height, this.width, pixels);
        }
    }
}
