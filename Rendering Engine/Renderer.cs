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

        private double aspectRatio;
        private double focalLength;

        private OutputManager outputManager;



        public Renderer(double aspectRatio, int width)
        {
            this.imageWidth = width;
            this.aspectRatio = aspectRatio;

            this.imageHeight = Math.Max((int)(width / aspectRatio), 1);

            this.viewportHeight = 2.0;
            this.viewportWidth = viewportHeight * ((double)imageWidth / imageHeight);

            this.outputManager = new OutputManager();
        }


        public void Render()
        {
            double focalLength = 1.0;
            Point3 cameraCenter = new Point3(0, 0, 0);

            Vector3 viewportU = new Vector3(viewportWidth, 0, 0);
            Vector3 viewportV = new Vector3(0, -viewportHeight, 0);

            Vector3 pixelDeltaU = viewportU / imageWidth;
            Vector3 pixelDeltaV = viewportV / imageHeight;

            Vector3 viewportStart = cameraCenter - new Vector3(0, 0, focalLength) - viewportU/2 - viewportV/2;
            Vector3 pixel00Location = viewportStart + 0.5 * (pixelDeltaU + pixelDeltaV);

            Console.WriteLine("Rendering image...");
            int[][] pixels = new int[imageWidth * imageHeight][];

            for (int j = 0; j < this.imageWidth; j++)
            {
                Console.WriteLine($"Rendering Line: {j+1}");
                for (int i = 0; i < this.imageHeight; i++)
                {

                    Vector3 pixelCenter = pixel00Location + j*pixelDeltaU + i*pixelDeltaV;
                    Vector3 rayDirection = pixelCenter - cameraCenter;
                    Ray ray = new Ray(cameraCenter, rayDirection);

                    Color3 color = CalculateRayColor(ray);

                    int[] pixel = color.ToRGB();
                    pixels[i * this.imageWidth + j] = pixel;
                }

            }

            this.outputManager.WriteOutput(this.imageHeight, this.imageWidth, pixels);
        }

        private Color3 CalculateRayColor(Ray ray)
        {
            Vector3 unitDirection = Vector3.UnitVector(ray.Direction);
            double a = 0.5 * (unitDirection.Y + 1.0);

            return (1.0 - a) * new Color3(1.0, 1.0, 1.0) + a * new Color3(0.5, 0.7, 1.0);
        }

    }
}
