using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximovInk.FlatinyEngine.Core.Graphics;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUIRawImage : GUIRect,IGUIGraphics
    {
        public Texture2D Texture { get; set; }

        public Color Color { get => color; set { color = value; Effect.SetUniform4("color", color.ToVector()); } }
        private Color color = Color.White;

        public GUIRawImage() 
        {
            mesh = Mesh.Quad;
            Effect = new Effect(Shaders.VERTEX,Shaders.COLORED_FRAGMENT);
            Color = Color.White;
        }
        
        public override void Render(float deltaTime)
        {
            if (Texture != null)
                Texture.Bind();

            base.Render(deltaTime);

            if (Texture != null)
                Texture.Unbind();
        }
    }


}
