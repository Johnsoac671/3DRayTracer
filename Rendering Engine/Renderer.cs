using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Output;

namespace Rendering_Engine
{
    public class Renderer
    {


        private OutputManager outputManager;
        private Camera camera;


        public Renderer(double aspectRatio, int width)
        {
            this.outputManager = new OutputManager();
            this.camera = new Camera(aspectRatio, width);
        }


        public void Render()
        {
            int[][] pixels = this.camera.Render();
            this.outputManager.WriteOutput(this.camera.ImageHeight, this.camera.ImageWidth, pixels);
        }

    }
}
