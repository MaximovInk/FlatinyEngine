using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUIButton : GUIRect
    {
        public bool Interactable { get; set; }

        public IGUIGraphics Graphics { get; set; }

        public Action OnClick { get; set; }

        public Color ColorOnDrag = Color.Red;
        public Color ColorOnOver = Color.Blue;

        private bool Over;
        private bool Drag;

        public GUIButton()
        {
            Effect = new Effect(Shaders.VERTEX, Shaders.COLORED_FRAGMENT);

            if(Graphics != null)
                Graphics.Color = Color.White;
        }

        public override void Update(float deltaTime)
        {

            if (Graphics != null)
                Graphics.Color = Drag ? ColorOnDrag : Over ? ColorOnOver : Color.White;

            base.Update(deltaTime);
        }

        public override void OnMousePress()
        {
            Drag = true;
        }

        public override void OnMouseUp()
        {
            Drag = false;
        }

        public override void OnMouseOver()
        {
            Over = true;
        }

        public override void OnMouseExit()
        {
            Over = false;
        }
    }
}
