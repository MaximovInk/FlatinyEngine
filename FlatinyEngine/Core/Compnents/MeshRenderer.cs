using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public class MeshRenderer : Renderer,IComponent
    {
        public Mesh Mesh { get { return mesh; }set { mesh = value; } }

        public bool enabled { get; set; }
        public GameObject gameObject { get; set; }
        public string tag { get; set; }

        protected override Matrix4 GetMatrix()
        {
            return gameObject.transform.GetGlobalMatrix();            
        }
    }
}
