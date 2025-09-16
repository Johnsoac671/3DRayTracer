using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Materials;
using Rendering_Engine.Output;
using Rendering_Engine.Primitives;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{
    public class Renderer
    {
        private Scene scene;
        private RenderSettings renderSettings;
        private RayTracer rayTracer;
        private OutputManager outputManager;

        public Renderer(Scene scene, RenderSettings renderSettings)
        {
            this.scene = scene;
            this.renderSettings = renderSettings;
            this.rayTracer = new RayTracer(renderSettings);
            this.outputManager = new OutputManager();
        }


        public void Render()
        {
            Console.WriteLine("Starting ray tracing render...");

            int[][] pixels = new int[this.renderSettings.ImageHeight * this.renderSettings.ImageWidth][];

            for (int j = 0; j < this.renderSettings.ImageHeight; j++)
            {
                Console.WriteLine($"Scanlines remaining: {this.renderSettings.ImageHeight - j}");
                for (int i = 0; i < this.renderSettings.ImageWidth; i++)
                {
                    Color3 pixelColor = Color3.Black;

                    for (int sample = 0; sample < this.renderSettings.SamplesPerPixel; sample++)
                    {
                        Ray ray = this.scene.Camera.GetRayForPixel(i, j);
                        pixelColor += this.rayTracer.CalculateRayColor(ray, 0, this.scene.World);
                    }

                    pixelColor = this.renderSettings.SampleScale * pixelColor;
                    int[] pixel = pixelColor.ToRGB();

                    pixels[j * this.renderSettings.ImageWidth + i] = pixel;
                }
            }

            this.outputManager.WriteOutput(this.renderSettings.ImageHeight, this.renderSettings.ImageWidth, pixels);
            Console.WriteLine("Rendering Complete");
        }

    }
}
