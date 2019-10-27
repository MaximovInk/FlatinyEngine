using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUICenterConstraint : GUIConstraint
    {
        public override float GetValue()
        {
            if (rect.Parent == null)
                return 0;

            if (axis == Axis.X)
            {
                return rect.Parent.Size.X * 0.5f;
            }

                return rect.Parent.Size.Y * 0.5f;
        }
    }
}
