using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering_Engine
{
    public class Scene
    {
        public RenderableList World {  get; }
        public Camera Camera { get; }

        public Scene(RenderableList world, Camera camera)
        {
            this.World = world;
            this.Camera = camera;
        }
    }
}
