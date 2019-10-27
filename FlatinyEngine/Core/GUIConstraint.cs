using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public abstract class GUIConstraint
    {
        public Axis axis;   

        public GUIRect rect;

        public abstract float GetValue();

        public enum Axis
        {
            X,
            Y
        }
    }
}
