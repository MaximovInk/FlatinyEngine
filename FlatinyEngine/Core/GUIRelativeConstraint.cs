using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUIRelativeConstraint : GUIConstraint
    {
        private float value;

        public GUIRelativeConstraint(float relativeValue)
        {
            value = relativeValue;
        }

        public override float GetValue()
        {
            if (rect.Parent == null)
                return 0;

            if (axis == Axis.X)
                return rect.Parent.Size.X*value;

            return rect.Parent.Size.Y * value;
        }
    }
}
