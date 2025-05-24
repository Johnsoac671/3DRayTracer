using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine.Output
{
    public abstract class Format
    {
        public abstract void CreateImage(int height, int width);
        public abstract void Write(int[] rgb);
        public abstract void Save();
    }
}
