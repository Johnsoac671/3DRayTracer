using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Output;
using Rendering_Engine.Primitives;

namespace Rendering_Engine
{
    public class Renderer
    {


        private OutputManager outputManager;
        private Camera camera;
        private RenderableList world;


        public Renderer(double aspectRatio, int width)
        {
            this.outputManager = new OutputManager();
            this.camera = new Camera(aspectRatio, width);


            this.world = new RenderableList();
            this.world.Add(new Sphere(new Point3(0, 0, -1), 0.5));
            this.world.Add(new Sphere(new Point3(0, -100.5, -1), 100));
        }


        public void Render()
        {

            int[][] pixels = this.camera.Render(this.world);
            this.outputManager.WriteOutput(this.camera.ImageHeight, this.camera.ImageWidth, pixels);
        }

    }
}
