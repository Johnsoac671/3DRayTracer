using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{
    public class RandomGenerator
    {
        Random random;
        public RandomGenerator()
        {
            this.random = new Random();
        }

        public double NextDouble()
        {
            return this.random.NextDouble();
        }

        public double NextDouble(double min, double max)
        {
            return min + (max - min) * NextDouble();
        }

    }
}
