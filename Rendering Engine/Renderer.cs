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
        private int imageWidth;
        private int imageHeight;

        private double viewportWidth;
        private double viewportHeight;

        private double aspect_ratio;

        private OutputManager outputManager;



        public Renderer(double aspect_ratio, int width)
        {
            this.imageWidth = width;
            this.aspect_ratio = aspect_ratio;

            this.imageHeight = Math.Max((int)(width / aspect_ratio), 1);

            this.viewportHeight = 2.0;
            this.viewportWidth = viewportHeight * ((double)imageWidth / imageWidth);

            this.outputManager = new OutputManager();
        }


        public void Render()
        {
            Console.WriteLine("Rendering image...");
            int[][] pixels = new int[imageWidth * imageHeight][];

            for (int j = 0; j < this.imageWidth; j++)
            {
                Console.WriteLine($"Rendering Line: {j+1}");
                for (int i = 0; i < this.imageHeight; i++)
                {

                    Color3 color = new Color3((double)i / (this.imageWidth - 1), (double)j / (this.imageHeight - 1), 0);

                    int[] pixel = color.ToRGB();
                    pixels[i * this.imageWidth + j] = pixel;
                }

            }

            this.outputManager.WriteOutput(this.imageHeight, this.imageWidth, pixels);
        }

    }
}
