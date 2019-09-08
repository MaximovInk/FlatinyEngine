using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public sealed class SpriteRenderer : Renderer,IComponent
    {
        private Sprite sprite;

        public bool enabled { get; set; }
        public GameObject gameObject { get; set; }
        public string tag { get; set; }
        protected override Matrix4 GetMatrix()
        {
            return gameObject.transform.GetGlobalMatrix();
        }

        public SpriteRenderer()
        {
            mesh = Mesh.Quad;
        }

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public Sprite GetSprite() => sprite;

        public override void Render(float deltaTime)
        {
            if (sprite == null)
                return;
            base.Render(deltaTime);
        }


    }
}
