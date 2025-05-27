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
    }
}
