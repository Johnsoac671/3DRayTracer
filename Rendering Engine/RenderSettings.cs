using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{
    public class RenderSettings
    {

        public int SamplesPerPixel { get; init; } = 100;
        public int MaxDepth { get; init; } = 50;

        public double SampleScale => 1.0 / SamplesPerPixel;

        public double AspectRatio { get; init; } = 16.0 / 9.0;
        public int ImageWidth { get; init; } = 400;
        public int ImageHeight => Math.Max((int)(ImageWidth / AspectRatio), 1);
    }
}
