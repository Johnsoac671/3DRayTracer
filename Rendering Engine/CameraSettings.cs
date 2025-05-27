using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{
    internal class CameraSettings
    {
        public Point3 LookFrom { get; init; } = new(-2, 2, 1);
        public Point3 LookTo { get; init; } = new(0, 0, -1);

        public Vector3 LookUp { get; init; } = new(0, 1, 0);

        public double AspectRatio { get; init; } = 16.0 / 9.0;
        public int ImageWidth { get; init; } = 400;
        public double FeildOfView { get; init; } = 90;
        public double FocusAngle { get; init; } = 10.0;
        public double FocusDistance { get; init; } = 3.4;

        public int ImageHeight => Math.Max((int)(ImageWidth / AspectRatio), 1);
    }
}
