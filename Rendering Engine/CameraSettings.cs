using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{
    public class CameraSettings
    {
        public Point3 LookFrom { get; init; } = new(-2, 2, 1);
        public Point3 LookTo { get; init; } = new(0, 0, -1);
        public Vector3 LookUp { get; init; } = new(0, 1, 0);

        public double FieldOfView { get; init; } = 90;
        public double FocusAngle { get; init; } = 0.0;
        public double FocusDistance { get; init; } = 3.4;


    }
}
