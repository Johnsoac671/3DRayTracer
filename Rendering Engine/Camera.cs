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
        // Image Properties
        private int imageWidth;
        private int imageHeight;

        // Viewport Properties
        private double viewportWidth;
        private double viewportHeight;
        private double aspectRatio;
        private double focalLength;

        // Camera Properties
        private Point3 center;
        private Point3 pixel00Location;
        private Vector3 pixelDeltaU;
        private Vector3 pixelDeltaV;
        private double fov;

        private Point3 lookFrom;
        private Point3 lookTo;
        private Vector3 lookUp;

        private Vector3 u, v, w;

        private Vector3 defocusDiskU;
        private Vector3 defocusDiskV;

        private double defocusAngle;
        private double focusDistance;

        // Sampling Properties
        private int samplesPerPixel;
        private double sampleScale;
        private int maxDepth;
        private Random random;

        public Camera(double aspectRatio, int width, int samplesPerPixel)
        {
            this.aspectRatio = aspectRatio;
            this.imageWidth = width;
            this.imageHeight = Math.Max((int)(width / aspectRatio), 1);
            this.samplesPerPixel = samplesPerPixel;
            this.sampleScale = 1.0 / samplesPerPixel;
            this.maxDepth = 50;
            this.random = new Random();

            this.fov = 20;
            this.defocusAngle = 10;
            this.focusDistance = 3.4;


            this.lookFrom = new Point3(-2, 2, 1);
            this.lookTo = new Point3(0, 0, -1);
            this.lookUp = new Vector3(0, 1, 0);

            this.center = this.lookFrom;

            // Initialize Viewport
            double theta = (Math.PI / 180) * fov;
            double h = Math.Tan(theta / 2);

            this.viewportHeight = 2 * h * this.focusDistance;
            this.viewportWidth = viewportHeight * ((double)imageWidth / imageHeight);

            this.w = Vector3.UnitVector(this.lookFrom -  this.lookTo);
            this.u = Vector3.UnitVector(Vector3.Cross(this.lookUp, w));
            this.v = Vector3.Cross(w, u);

            // Calculating pixel positioning
            Vector3 viewportU = viewportWidth * u;
            Vector3 viewportV = viewportHeight * -v;

            this.pixelDeltaU = viewportU / this.imageWidth;
            this.pixelDeltaV = viewportV / this.imageHeight;

            Point3 viewportStart = this.center - (focusDistance * w) - viewportU / 2 - viewportV / 2;
            this.pixel00Location = viewportStart + 0.5 * (pixelDeltaU + pixelDeltaV);

            double defocusRadius = focusDistance * Math.Tan((Math.PI / 180) * (defocusAngle / 2));
            this.defocusDiskU = u * defocusRadius;
            this.defocusDiskV = v * defocusRadius;

        }

        public int ImageWidth
        {
            get
            {
                return this.imageWidth;
            }
        }

        public int ImageHeight
        {
            get
            {
                return this.imageHeight;
            }
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

            Point3 rayOrigin = (defocusAngle <= 0) ? center : defocusDiskSample();
            return new Ray(rayOrigin, sampleLocation - center);

        }

        private Point3 defocusDiskSample()
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
