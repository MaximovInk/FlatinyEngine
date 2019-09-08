using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Tilemaps
{
    public class Tile
    {
        public Vector4 uv { get; private set; }

        public Tile(Vector4 uv)
        {
            this.uv = uv;
        }

        public void OnCreate()
        {
            
        }
    }
}
