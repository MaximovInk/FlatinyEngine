using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximovInk.FlatinyEngine.Core.Compnents;
using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;

namespace MaximovInk.FlatinyEngine.Core.Tilemaps
{
   

    public class Chunk : Renderer
    {
        private class TileVertexData
        {
            public readonly int index;
            public uint[] colors = new uint[4];
            public uint[] indices = new uint[6];
            public Vertex[] vertices = new Vertex[4];

            public TileVertexData(int index)
            {
                this.index = index;
            }
        }

        public int x { get; set; }
        public int y { get; set; }
        public TilemapRenderer renderer { get; private set; }
        public bool isDirty { get; private set; }

        private int[] tileIds = new int[TilemapRenderer.CHUNK_SIZE*TilemapRenderer.CHUNK_SIZE];
        private List<TileVertexData> tilesVertices = new List<TileVertexData>();

        public Chunk(TilemapRenderer renderer,int x ,int y)
        {
            this.renderer = renderer;
            this.x = x;
            this.y = y;

          
            mesh = Mesh.Grid(TilemapRenderer.CHUNK_SIZE, TilemapRenderer.CHUNK_SIZE);
        }

        public void SetColor(int x, int y, Color color)
        {
            var index = x * 4 + y * TilemapRenderer.CHUNK_SIZE * 4;
         

            isDirty = true;
        }

        public void UpdateMesh()
        {
           
            mesh.ApplyData();
            isDirty = false;
        }


        
        public int GetTile(int x ,int y)
        {
            return tileIds[x + y * TilemapRenderer.CHUNK_SIZE];
        }

        public void SetTile(int x, int y,int tileId)
        {
                tileIds[x+y*TilemapRenderer.CHUNK_SIZE] = tileId;
                

                isDirty = true;
        }

        private TileVertexData GetOrCreateVertexData(int index)
        {
            var getted = tilesVertices.FirstOrDefault(n => n.index == index);

            if (getted != null)
                return getted;

            var maded = new TileVertexData(index);

            return maded;
        }

        public void SetTileCoords(int x, int y, Tile tile)
        {
            /* if (tile == null)
                 return;
             var index = x * 4 + y * TilemapRenderer.CHUNK_SIZE * 4;
             mesh.vertices[index].texCoord = tile.uv.Xy;
             mesh.vertices[index+1].texCoord = tile.uv.Zy;
             mesh.vertices[index+2].texCoord = tile.uv.Zw;
             mesh.vertices[index+3].texCoord = tile.uv.Xw;

             var data = GetOrCreateVertexData(index);
             data.vertices[]


             isDirty = true;*/
        }

        protected override Matrix4 GetMatrix()
        {
            return renderer.gameObject.transform.GetGlobalMatrix()*Matrix4.CreateTranslation(x*TilemapRenderer.CHUNK_SIZE,y*TilemapRenderer.CHUNK_SIZE,0);
        }
    }
}
