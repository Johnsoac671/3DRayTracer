using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Materials;
using Rendering_Engine.Output;
using Rendering_Engine.Primitives;
using Rendering_Engine.Utilities;

namespace Rendering_Engine
{
    public class Renderer
    {
        private RenderableList world;
        private RayTracer rayTracer;
        public Renderer()
        {
            CameraSettings cameraSettings = new CameraSettings();
            RenderSettings renderSettings = new RenderSettings();
            rayTracer = new RayTracer(cameraSettings, renderSettings);

            Material ground = new Diffuse(new Color3(0.8, 0.8, 0.8));

            Material middle = new Diffuse(new Color3(0.1, 0.2, 0.5));
            Material left = new Dielectric(1.5);
            Material right = new Metal(new Color3(0.8, 0.6, 0.2), 1.0);


            this.world = new RenderableList();
            this.world.Add(new Sphere(new Point3(0, 0, -1), 0.5, middle));
            this.world.Add(new Sphere(new Point3(-1.0, 0, -1), 0.5, left));
            this.world.Add(new Sphere(new Point3(1.0, 0, -1), 0.5, right));
            this.world.Add(new Sphere(new Point3(0, -100.5, -1), 100, ground));
        }


        public void Render()
        {
            rayTracer.Render(world);
        }

    }
}
