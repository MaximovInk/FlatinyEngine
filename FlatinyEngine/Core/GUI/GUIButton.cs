using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public sealed class GUIButton : GUIImage
    {
        public bool Interactable { get; set; }

        public bool IsPressed { get; private set; }
        public bool IsOver { get; private set; }

        public Action action { get; set; }



        public GUIButton(GUICanvas canvas):base(canvas)
        {
            
        }

        public override void OnRender()
        {
            if (IsPressed)
            {
                SetColor(System.Drawing.Color.Red);
            }
            else if (IsOver)
            {
                SetColor(System.Drawing.Color.Blue);
            }
            else
            {
                SetColor(System.Drawing.Color.White);
            }
            base.OnRender();
        }

        public override void OnDragStart()
        {
            IsPressed = true;
            action?.Invoke();
        }

        public override void OnDragEnd()
        {
            IsPressed = false;
        }

        public override void OnMouseEnter()
        {
            IsOver = true;
        }

        public override void OnMouseExit()
        {
            IsOver = false;
        }
    }
}
