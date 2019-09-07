using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public class MeshRenderer : Renderer
    {
        public Mesh Mesh { get { return mesh; }set { mesh = value; } }
    }
}
