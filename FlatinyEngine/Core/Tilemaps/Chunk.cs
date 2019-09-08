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
        public int x { get; set; }
        public int y { get; set; }
        public TilemapRenderer renderer { get; private set; }
        public bool isDirty { get; private set; }

        private int[,] indices = new int[TilemapRenderer.CHUNK_SIZE, TilemapRenderer.CHUNK_SIZE];

        private uint[] colors = new uint[TilemapRenderer.CHUNK_SIZE*TilemapRenderer.CHUNK_SIZE*4];

        public Chunk(TilemapRenderer renderer,int x ,int y)
        {
            this.renderer = renderer;
            this.x = x;
            this.y = y;

            for (int ix = 0; ix < indices.GetLength(0); ix++)
            {
                for (int iy = 0; iy < indices.GetLength(1); iy++)
                {
                    indices[ix, iy] = -1;
                }
            }

            colors.Fill(uint.MaxValue);

            mesh = Mesh.Grid(TilemapRenderer.CHUNK_SIZE, TilemapRenderer.CHUNK_SIZE);
        }

        public void SetColor(int x, int y, Color color)
        {
            var index = x * 4 + y * TilemapRenderer.CHUNK_SIZE * 4;
            colors[index] = color.ToUInt();
            colors[index+1] = color.ToUInt();
            colors[index+2] = color.ToUInt();
            colors[index+3] = color.ToUInt();
            isDirty = true;
        }

        public void UpdateMesh()
        {
            if (colors.Length != mesh.vertices.Length)
                throw new Exception("Chunk update mesh error , colors lenght not equals to mesh vertices");

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                if (indices[i/4 % TilemapRenderer.CHUNK_SIZE, i/4 / TilemapRenderer.CHUNK_SIZE] == -1)
                    mesh.vertices[i].color = Vector4.Zero;
                else
                    mesh.vertices[i].color = colors[i].ToColor().ToVector();
            }

            mesh.ApplyData();
            isDirty = false;
        }


        
        public int GetTile(int x ,int y)
        {
            return indices[x, y];
        }

        public void SetTile(int x, int y,int tileId)
        {
            indices[x, y] = tileId;
            isDirty = true;
        }

        public void SetTileCoords(int x, int y, Tile tile)
        {
            if (tile == null)
                return;
            var index = x * 4 + y * TilemapRenderer.CHUNK_SIZE * 4;
            Logger.Log(tile.uv);
            mesh.vertices[index].texCoord = tile.uv.Xy;
            mesh.vertices[index+1].texCoord = tile.uv.Zy;
            mesh.vertices[index+2].texCoord = tile.uv.Zw;
            mesh.vertices[index+3].texCoord = tile.uv.Xw;
            isDirty = true;
        }

        protected override Matrix4 GetMatrix()
        {
            return renderer.gameObject.transform.GetGlobalMatrix()*Matrix4.CreateTranslation(x*TilemapRenderer.CHUNK_SIZE,y*TilemapRenderer.CHUNK_SIZE,0);
        }
    }
}
