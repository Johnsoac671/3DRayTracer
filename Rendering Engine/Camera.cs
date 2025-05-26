﻿using System;
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
        Point3 center;
        Point3 pixel00Location;
        Vector3 pixelDeltaU;
        Vector3 pixelDeltaV;

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
            this.maxDepth = 10;
            this.random = new Random();

            // Initialize Viewport
            this.viewportHeight = 2.0;
            this.viewportWidth = viewportHeight * ((double)imageWidth / imageHeight);
            this.focalLength = 1.0;
            center = new Point3(0, 0, 0);

            // Calculating pixel positioning
            Vector3 viewportU = new Vector3(this.viewportWidth, 0, 0);
            Vector3 viewportV = new Vector3(0, -this.viewportHeight, 0);

            this.pixelDeltaU = viewportU / this.imageWidth;
            this.pixelDeltaV = viewportV / this.imageHeight;

            Point3 viewportStart = this.center - new Vector3(0, 0, focalLength) - viewportU / 2 - viewportV / 2;
            this.pixel00Location = viewportStart + 0.5 * (pixelDeltaU + pixelDeltaV);

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

            return new Ray(this.center, sampleLocation - center);

        }

        private Vector3 GetSampleVector()
        {
            return new Vector3(this.random.NextDouble() - 0.5, this.random.NextDouble() - 0.5, 0);
        }
    }
}
