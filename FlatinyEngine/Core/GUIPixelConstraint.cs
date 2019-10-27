using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUIPixelConstraint : GUIConstraint
    {
        private int pixels;

        public GUIPixelConstraint(int pixels)
        {
            this.pixels = pixels;
        }

        public override float GetValue()
        {
            return pixels;
        }
    }
}
