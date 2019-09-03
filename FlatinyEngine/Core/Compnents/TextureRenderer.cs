using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public sealed class TextureRenderer : MeshRenderer
    {
        public Texture2D Texture { get; set; }
        private Color color = Color.White;

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

        public TextureRenderer()
        {
            mesh = Mesh.Quad;
        }

        public override void OnRender(float deltaTime)
        {
            if (Texture != null)
            {
                Texture.Bind();

                base.OnRender(deltaTime);

                Texture.Unbind();
            }
        }
    }
}
