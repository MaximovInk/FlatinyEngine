using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public abstract class ScreenRenderer : Renderer
    {
        protected override Matrix4 CameraProjection()
        {
            return Screen.ScreenProjectionMatrix;
        }
    }
}
