using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public sealed class TextureRenderer : Renderer,IComponent
    {
        public Texture2D Texture { get; set; }
        private Color color { get; set; } = Color.White;

        public override Effect Effect { get; set; }
        public bool enabled { get; set; }
        public GameObject gameObject { get; set; }
        public string tag { get; set; }

        protected override Matrix4 GetMatrix()
        {
            return gameObject.transform.GetGlobalMatrix();
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

        public TextureRenderer()
        {
            mesh = Mesh.Quad;
        }

        public override void Render(float deltaTime)
        {
           
            if (Texture != null)
            {
                Texture.Bind();

                base.Render(deltaTime);

                Texture.Unbind();
            }

           
        }
    }
}
