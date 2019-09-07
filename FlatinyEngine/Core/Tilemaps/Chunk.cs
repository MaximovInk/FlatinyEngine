using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximovInk.FlatinyEngine.Core.Compnents;

namespace MaximovInk.FlatinyEngine.Core.Tilemaps
{
    public class Chunk : Renderer
    {
        public int x { get; set; }
        public int y { get; set; }
        public TilemapRenderer renderer { get; private set; }

        private const int CHUNK_SIZE = 32; 

        private List<Tile> tiles = new List<Tile>();

        //private int[,] indices = new int[,];

        public void OnChangeSize(int x, int y)
        {
            
        }

        public Chunk(TilemapRenderer renderer,int x ,int y)
        {
            this.renderer = renderer;
            this.x = x;
            this.y = y;
        }

        private void CreateMesh()
        {
            
        }

        /*public Tile GetTile(int x ,int y)
        {
            
        }

        public void SetTile(int x, int y,Tile tile)
        {

        }*/
    }
}
