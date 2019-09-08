using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public sealed class GUIButton : GUIRect
    {
        public bool Interactable { get; set; }

        public bool IsPressed { get; private set; }
        public bool IsOver { get; private set; }

        public Graphics Graphics { get; set; }

        public Action action { get; set; }

        public GUIButton(GUICanvas canvas):base(canvas)
        {
            
        }

        public override void OnRender()
        {
            

            if (Graphics != null)
            {
                if (IsPressed)
                {
                    Graphics.Color = Color.Red;
                }
                else if (IsOver)
                {
                    Graphics.Color = Color.Blue;
                }
                else
                {
                    Graphics.Color = Color.White;
                }
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
