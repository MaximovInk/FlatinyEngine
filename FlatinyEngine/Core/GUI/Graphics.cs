using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public class Graphics : GUIRect
    {
        public Graphics(GUICanvas canvas) : base(canvas)
        {
        }

        public virtual Color Color { get; set; }

    }

}
