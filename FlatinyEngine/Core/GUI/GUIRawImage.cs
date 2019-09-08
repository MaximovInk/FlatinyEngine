using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public class GUIRawImage : Graphics
    {
        public Texture2D Texture { get; set; }
        public override Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                mesh.vertices[0].Color = Color;
                mesh.vertices[1].Color = Color;
                mesh.vertices[2].Color = Color;
                mesh.vertices[3].Color = Color;
                mesh.ApplyData();
            }
        }

        private Color _color;

        public GUIRawImage(GUICanvas canvas) : base(canvas)
        {

        }

        protected override void OnCreate()
        {
            base.OnCreate();
            mesh = Mesh.Quad;
        }

        public override void OnRender()
        {
            Texture.Bind();

            base.OnRender();

            Texture.Unbind();
        }

    }
}
