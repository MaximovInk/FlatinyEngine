using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public sealed class GUIImage : GUIRect
    {
        private Texture2D texture;
        private Color color;

        public GUIImage(GUICanvas canvas) : base(canvas)
        {
            
        }

        public void SetColor(Color color)
        {
            this.color = color;
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                mesh.vertices[i].Color = color;
            }
            mesh.ApplyData();
        }

        public Color GetColor() => color;

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        public Texture2D GetTexture() => texture;

        protected override void OnCreate()
        {
            base.OnCreate();
            mesh = Mesh.Quad;
        }

        public override void OnRender()
        {
            texture.Bind();

            base.OnRender();
        }
    }
}
