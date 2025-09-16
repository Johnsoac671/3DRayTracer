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
    /// <summary>
    /// Represents the virtual camera in the scene.
    /// This class is responsible for generating rays from the camera's position through each pixel of the viewport.
    /// </summary>
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

            Initialize();
        }

        /// <summary>
        /// Initializes the camera's properties based on the provided settings.
        /// This method calculates the viewport dimensions, pixel deltas, and defocus disk vectors.
        /// </summary>
        private void Initialize()
        {
            this.center = cameraSettings.LookFrom;

            this.w = Vector3.UnitVector(cameraSettings.LookFrom - cameraSettings.LookTo);
            this.u = Vector3.UnitVector(Vector3.Cross(cameraSettings.LookUp, this.w));
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

        /// <summary>
        /// Generates a ray for a specific pixel in the viewport.
        /// </summary>
        public Ray GetRayForPixel(int i, int j)
        {
            Vector3 offset = GetSampleVector();
            Point3 sampleLocation = this.pixel00Location
                + ((i + offset.X) * this.pixelDeltaU)
                + ((j + offset.Y) * this.pixelDeltaV);

            Point3 rayOrigin = (this.cameraSettings.FocusAngle <= 0) ? this.center : GetDefocusSample();
            return new Ray(rayOrigin, sampleLocation - rayOrigin);
        }

        /// <summary>
        /// If needed, returns a sample with a shift relative to the focal values of the camera
        /// </summary>
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