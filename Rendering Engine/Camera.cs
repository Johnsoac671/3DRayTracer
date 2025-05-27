using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Primitives;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{
    public class Camera
    {

        private CameraSettings cameraSettings;
        private RenderSettings renderSettings;
        private RandomGenerator random;

        private Point3 center;
        private Point3 pixel00Location;
        private Vector3 pixelDeltaU;
        private Vector3 pixelDeltaV;

        private Vector3 u, v, w;

        private Vector3 defocusDiskU;
        private Vector3 defocusDiskV;

        public Camera(CameraSettings cameraSettings, RenderSettings renderSettings, RandomGenerator random)
        {
            this.cameraSettings = cameraSettings;
            this.renderSettings = renderSettings;
            this.random = random;

            this.center = cameraSettings.LookFrom;

            this.w = Vector3.UnitVector(cameraSettings.LookFrom - cameraSettings.LookTo);
            this.u = Vector3.Cross(cameraSettings.LookUp, this.w);
            this.v = Vector3.Cross(this.w, this.u);

            double theta = (Math.PI / 180) * cameraSettings.FieldOfView;
            double h = Math.Tan(theta / 2);

            double viewportHeight = 2 * h * cameraSettings.FocusDistance;
            double viewportWidth = viewportHeight * ((double)renderSettings.ImageWidth / renderSettings.ImageHeight);

            Vector3 viewportU = viewportWidth * u;
            Vector3 viewportV = viewportHeight * -v;

            this.pixelDeltaU = viewportU / renderSettings.ImageWidth;
            this.pixelDeltaV = viewportV / renderSettings.ImageHeight;

            Point3 viewportStart = center - (cameraSettings.FocusDistance * w) - viewportU / 2 - viewportV / 2;
            this.pixel00Location = viewportStart + 0.5 * (pixelDeltaU + pixelDeltaV);

            double defocusRadius = cameraSettings.FocusDistance * Math.Tan((Math.PI / 180) * (cameraSettings.FocusAngle / 2));
            this.defocusDiskU = u * defocusRadius;
            this.defocusDiskV = v * defocusRadius;

        }

        public int[][] Render(RenderableList world)
        {
            Console.WriteLine("Rendering image...");
            int[][] pixels = new int[imageWidth * imageHeight][];

            for (int j = 0; j < this.imageHeight; j++)
            {
                Console.WriteLine($"Scanlines remaining: {this.imageHeight - j}");
                for (int i = 0; i < this.imageWidth; i++)
                {
                    Color3 pixelColor = new Color3(0, 0, 0);

                    for (int sample = 0; sample < samplesPerPixel; sample++)
                    {
                        Ray ray = GetRandomRay(i, j);
                        pixelColor += CalculateRayColor(ray, 0, world);
                    }

                    pixelColor = sampleScale * pixelColor;
                    int[] pixel = pixelColor.ToRGB();

                    pixels[j * this.imageWidth + i] = pixel;
                }
            }

            return pixels;
        }

        private Color3 CalculateRayColor(Ray ray, int depth, IRenderable world)
        {
            if (depth >= this.maxDepth)
            {
                return new Color3(0, 0, 0);
            }
            HitRecord record = new HitRecord();
            if (world.IsHit(ray, new Interval(0, double.PositiveInfinity), ref record))
            {
                Color3 attenuation = new Color3(0, 0, 0);
                Ray scatteredRay = record.Material.Scatter(ray, record, ref attenuation);

                return attenuation * CalculateRayColor(scatteredRay, depth+1, world);
            }


            Vector3 unitDirection = Vector3.UnitVector(ray.Direction);
            double a = 0.5 * (unitDirection.Y + 1.0);

            return (1.0 - a) * new Color3(1.0, 1.0, 1.0) + a * new Color3(0.5, 0.7, 1.0);
        }

        private Ray GetRandomRay(int i, int j)
        {
            Vector3 offset = GetSampleVector();
            Point3 sampleLocation = this.pixel00Location
                + ((i + offset.X) * this.pixelDeltaU)
                + ((j + offset.Y) * this.pixelDeltaV);

            Point3 rayOrigin = (this.cameraSettings.FocusAngle <= 0) ? this.center : GetDefocusSample();
            return new Ray(rayOrigin, sampleLocation - center);

        }

        private Point3 GetDefocusSample()
        {
            Vector3 p = Vector3.RandomUnitOnDisk();

            return this.center + (p.X * defocusDiskU) + (p.Y * defocusDiskV);
        }

        private Vector3 GetSampleVector()
        {
            return new Vector3(this.random.NextDouble() - 0.5, this.random.NextDouble() - 0.5, 0);
        }
    }
}
