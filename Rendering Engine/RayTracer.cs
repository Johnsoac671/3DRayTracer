using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Materials;
using Rendering_Engine.Output;
using Rendering_Engine.Primitives;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{
    public class RayTracer
    {
        private OutputManager outputManager;
        private Camera camera;
        private RenderSettings renderSettings;
        private RandomGenerator random;

        public RayTracer(CameraSettings cameraSettings, RenderSettings renderSettings)
        {
            this.outputManager = new OutputManager();
            this.random = new RandomGenerator();
            this.renderSettings = renderSettings;
            this.camera = new Camera(cameraSettings, renderSettings, random);
        }

        public void Render(RenderableList world)
        {
            Console.WriteLine("Starting ray tracing render...");
            int[][] pixels = new int[renderSettings.ImageWidth * renderSettings.ImageHeight][];

            for (int j = 0; j < renderSettings.ImageHeight; j++)
            {
                Console.WriteLine($"Scanlines remaining: {renderSettings.ImageHeight - j}");
                for (int i = 0; i < renderSettings.ImageWidth; i++)
                {
                    Color3 pixelColor = Color3.Black;

                    for (int sample = 0; sample < renderSettings.SamplesPerPixel; sample++)
                    {
                        Ray ray = camera.GetRayForPixel(i, j);
                        pixelColor += CalculateRayColor(ray, 0, world);
                    }

                    pixelColor = renderSettings.SampleScale * pixelColor;
                    int[] pixel = pixelColor.ToRGB();

                    pixels[j * renderSettings.ImageWidth + i] = pixel;
                }
            }

            this.outputManager.WriteOutput(renderSettings.ImageHeight, renderSettings.ImageWidth, pixels);
            Console.WriteLine("Ray tracing complete!");
        }

        private Color3 CalculateRayColor(Ray ray, int depth, IRenderable world)
        {
            if (depth >= renderSettings.MaxDepth)
            {
                return Color3.Black;
            }

            HitRecord record = new HitRecord();
            if (world.IsHit(ray, new Interval(0.001, double.PositiveInfinity), ref record))
            {
                Color3 attenuation = Color3.Black;
                Ray scatteredRay = record.Material.Scatter(ray, record, ref attenuation);

                return attenuation * CalculateRayColor(scatteredRay, depth + 1, world);
            }

            // Background gradient (sky)
            Vector3 unitDirection = Vector3.UnitVector(ray.Direction);
            double a = 0.5 * (unitDirection.Y + 1.0);

            return (1.0 - a) * Color3.White + a * Color3.LightBlue;
        }
    }
}