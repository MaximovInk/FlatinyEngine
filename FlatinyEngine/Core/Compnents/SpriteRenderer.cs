using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public sealed class SpriteRenderer : Renderer
    {
        private Sprite sprite;

        public SpriteRenderer()
        {
            mesh = Mesh.Quad;
        }

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public Sprite GetSprite() => sprite;

        public override void OnRender(float deltaTime)
        {
            if (sprite != null)
            {
                base.OnRender(deltaTime);
            }
        }
    }
}
