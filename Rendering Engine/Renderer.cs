using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{
    public class Renderer
    {
        private int width;
        private int height;

        public Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Render()
        {
            Console.WriteLine("Rendering image...");

            using (StreamWriter outputFile = new StreamWriter("test.ppm"))
            {
                outputFile.WriteLine($"P3\n{this.width}\n{this.height}\n255");

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

                        outputFile.WriteLine($"{ir} {ig} {ib}");
                    }

                }
            }
        }
    }
}
