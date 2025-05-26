using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Utilities
{
    public class Interval
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
            return Min <= value && value <= Max;
        }

        public bool Surrounds(double value)
        {
            return Min < value && value < Max;
        }

        public double Clamp(double value)
        {
            if (value < Min)
            {
                return Min;
            }

            if (value > Max)
            {
                return Max;
            }

            return value;
        }
    }
}
