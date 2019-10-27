using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUIImage : GUIRect
    {
        public Sprite Sprite { get => sprite; set { sprite = value; UpdateSpriteMesh(); } }
        private Sprite sprite;

        public GUIImage()
        {
            mesh = Mesh.Quad;
        }

        private void UpdateSpriteMesh()
        {
            if (Sprite == null)
                return;
        }

        public override void Render(float deltaTime)
        {
            if (Sprite != null)
                Sprite.Bind();

            base.Render(deltaTime);

            if (Sprite != null)
                Sprite.Unbind();
        }
    }
}
