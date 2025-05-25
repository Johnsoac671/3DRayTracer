using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Utilities
{
    internal class Interval
    {
        public double Min {  get; set; }
        public double Max { get; set; }

        public Interval()
        {
            Min = double.NegativeInfinity;
            Max = double.PositiveInfinity;
        }

        public Interval(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public double Size
        {
            get
            {
                return Max - Min;
            }
        }

        public bool Contains(double value)
        {
            return Min <= value && Max <= value;
        }

        public bool Surrounds(double value)
        {
            return Min < value && Max < value;
        }
    }
}
