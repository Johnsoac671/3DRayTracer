using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine;
namespace Frontend
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Renderer render = new Renderer(16 / 9, 256);
            render.Render();
        }

    }
}
