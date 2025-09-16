global using Point3 = Rendering_Engine.Utilities.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine;
using Rendering_Engine.Materials;
using Rendering_Engine.Utilities;
using Rendering_Engine.Primitives;
namespace Frontend
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // 1. Create Settings
            var renderSettings = new RenderSettings();
            var cameraSettings = new CameraSettings();
            var random = new RandomGenerator();

            // 2. Create the Camera
            var camera = new Camera(cameraSettings, renderSettings, random);

            // 3. Create the World
            var world = new RenderableList();
            var groundMaterial = new Diffuse(new Color3(0.8, 0.8, 0.0));
            var centerMaterial = new Diffuse(new Color3(0.1, 0.2, 0.5));
            var leftMaterial = new Dielectric(1.5);
            var rightMaterial = new Metal(new Color3(0.8, 0.6, 0.2), 0.0);


            world.Add(new Sphere(new Point3(0.0, -100.5, -1.0), 100.0, groundMaterial));
            world.Add(new Sphere(new Point3(0.0, 0.0, -1.0), 0.5, centerMaterial));
            world.Add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.5, leftMaterial));
            world.Add(new Sphere(new Point3(1.0, 0.0, -1.0), 0.5, rightMaterial));

            // 4. Create the Scene
            var scene = new Scene(world, camera);

            // 5. Create the Renderer and Render
            var renderer = new Renderer(scene, renderSettings);
            renderer.Render();
        }
    }
}
