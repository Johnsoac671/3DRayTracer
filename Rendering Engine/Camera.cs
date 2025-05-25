using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{
    public class Camera
    {
        private int imageWidth;
        private int imageHeight;

        private double viewportWidth;
        private double viewportHeight;

        private double aspectRatio;
        private double focalLength;

        Point3 center;

        public Camera(double aspectRatio, int width)
        {
            this.imageWidth = width;
            this.aspectRatio = aspectRatio;

            this.imageHeight = Math.Max((int)(width / aspectRatio), 1);

            this.viewportHeight = 2.0;
            this.viewportWidth = viewportHeight * ((double)imageWidth / imageHeight);

            this.focalLength = 1.0;
            center = new Point3(0, 0, 0);
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

        public int[][] Render()
        {

            Vector3 viewportU = new Vector3(this.viewportWidth, 0, 0);
            Vector3 viewportV = new Vector3(0, -this.viewportHeight, 0);

            Vector3 pixelDeltaU = viewportU / this.imageWidth;
            Vector3 pixelDeltaV = viewportV / this.imageHeight;

            Point3 viewportStart = this.center - new Vector3(0, 0, focalLength) - viewportU / 2 - viewportV / 2;
            Point3 pixel00Location = viewportStart + 0.5 * (pixelDeltaU + pixelDeltaV);

            Console.WriteLine("Rendering image...");
            int[][] pixels = new int[imageWidth * imageHeight][];

            for (int j = 0; j < this.imageWidth; j++)
            {
                Console.WriteLine($"Rendering Line: {j + 1}");
                for (int i = 0; i < this.imageHeight; i++)
                {

                    Point3 pixelCenter = pixel00Location + j * pixelDeltaU + i * pixelDeltaV;
                    Vector3 rayDirection = pixelCenter - this.center;

                    Ray ray = new Ray(this.center, rayDirection);

                    Color3 color = CalculateRayColor(ray);

                    int[] pixel = color.ToRGB();
                    pixels[i * this.imageWidth + j] = pixel;
                }

            }

            return pixels;

        }

        private double HitSphere(Point3 sphereCenter, double radius, Ray r)
        {
            Vector3 oc = sphereCenter - r.Origin;
            var a = r.Direction.SquaredLength;
            var h = Vector3.Dot(r.Direction, oc);
            var c = oc.SquaredLength - radius*radius;

            var discriminant = h*h - a*c;
            
            if (discriminant < 0)
            {
                return -1.0;
            }
            else
            {
                return (h - Math.Sqrt(discriminant)) / a;
            }
        }

        private Color3 CalculateRayColor(Ray ray)
        {
            double t = HitSphere(new Point3(0, 0, -1), 0.5, ray);
            if (t > 0.0)
            {
                Vector3 n = Vector3.UnitVector(ray.PointAtTime(t) - new Vector3(0, 0, -1));
                return 0.5 * new Color3(n.X + 1, n.Y + 1, n.Z + 1);
            }

            Vector3 unitDirection = Vector3.UnitVector(ray.Direction);
            double a = 0.5 * (unitDirection.Y + 1.0);

            return (1.0 - a) * new Color3(1.0, 1.0, 1.0) + a * new Color3(0.5, 0.7, 1.0);
        }
    }
}
