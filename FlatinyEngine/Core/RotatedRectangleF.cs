using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public struct RotatedRectangleF
    {
        public float X, Y, Width, Height,Angle,OffsetX,OffsetY;

        public RotatedRectangleF(float x, float y, float width, float height, float angle, float offsetX, float offsetY)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }

        public bool IntersectWith(float pointX, float pointY)
        {
            var relX = pointX - X;
            var relY = pointY - Y;
            var angle = -Angle;
            var angleCos = Math.Cos(angle);
            var angleSin = Math.Sin(angle);
            var localX = angleCos * relX - angleSin * relY;
            var localY = angleSin * relX + angleCos * relY;
            return localX >= -OffsetX && localX <= Width - OffsetX &&
                   localY >= -OffsetY && localY <= Height - OffsetY;
        }
    }
}
