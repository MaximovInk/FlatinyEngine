using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUICanvas : GUIRect
    {
        public override Vector2 Size { get => Screen.Size; }

        public GUICanvas()
        {
            GUI.RegisterCanvas(this);
        }

        ~GUICanvas()
        {
            GUI.RemoveCanvas(this);
        }
    }
}
