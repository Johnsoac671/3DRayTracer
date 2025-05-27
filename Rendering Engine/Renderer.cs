using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Materials;
using Rendering_Engine.Output;
using Rendering_Engine.Primitives;

namespace Rendering_Engine
{
    public class Renderer
    {


        private OutputManager outputManager;
        private Camera camera;
        private RenderableList world;
        private int samplesPerPixel;

        public Renderer(double aspectRatio, int width, int samplesPerPixel)
        {
            this.outputManager = new OutputManager();
            this.camera = new Camera(aspectRatio, width, samplesPerPixel);

            Material ground = new Diffuse(new Color3(0.8, 0.8, 0.8));

            Material middle = new Diffuse(new Color3(0.1, 0.2, 0.5));
            Material left = new Glass(1.5);
            Material right = new Metal(new Color3(0.8, 0.6, 0.2), 1.0);


            this.world = new RenderableList();
            this.world.Add(new Sphere(new Point3(0, 0, -1), 0.5, middle));
            this.world.Add(new Sphere(new Point3(-1.0, 0, -1), 0.5, left));
            this.world.Add(new Sphere(new Point3(1.0, 0, -1), 0.5, right));
            this.world.Add(new Sphere(new Point3(0, -100.5, -1), 100, ground));
        }


        public void Render()
        {

            int[][] pixels = this.camera.Render(this.world);
            this.outputManager.WriteOutput(this.camera.ImageHeight, this.camera.ImageWidth, pixels);
        }

    }
}
