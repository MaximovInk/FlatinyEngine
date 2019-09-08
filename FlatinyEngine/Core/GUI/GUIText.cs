using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public sealed class GUIText : Graphics
    {
        public string Text { get; set; }

        public GUIText(GUICanvas canvas) : base(canvas)
        {
        }

        public Color Color {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
