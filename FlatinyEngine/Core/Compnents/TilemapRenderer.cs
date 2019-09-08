using MaximovInk.FlatinyEngine.Core.Graphics;
using MaximovInk.FlatinyEngine.Core.Tilemaps;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public class TilemapRenderer : IComponent, IRender, w
    {
        public Texture2D textureAtlas { get; set; }
        public int pixelsPerTileP { get; set; } = 8;
        public int offset { get; set; } = 0;
        public int padding { get; set; } = 1;

        private List<Chunk> chunks = new List<Chunk>();
        private List<Tile> tiles { get; set; } = new List<Tile>();

        public bool enabled { get; set; }
        public GameObject gameObject { get; set; }
        public string tag { get; set; }

        public const int CHUNK_SIZE = 32;

        public void UpdateMesh()
        {
            for (int i = 0; i < chunks.Count; i++)
            {
                if(chunks[i].isDirty)
                    chunks[i].UpdateMesh();
            }
        }

        public void SetTile(int x, int y, int tileId)
        {
            var ch = GetOrCreateChunk(x, y);
            ch.SetTile(IntoChunk(x), IntoChunk(y), tileId);
            if (tileId < tiles.Count && tileId >= 0)
            {
                ch.SetTileCoords(IntoChunk(x), IntoChunk(y), tiles[tileId]);
            }
        }

        public void Erase(int x, int y)
        {
            SetTile(x, y, -1);
        }

        public int GetTile(int x, int y)
        {
            var ch = GetOrCreateChunk(x, y);
            return ch.GetTile(IntoChunk(x), IntoChunk(y));
        }

        public void SliceAtlas()
        {
            tiles = new List<Tile>();
            float w = textureAtlas.Width;
            float h = textureAtlas.Height;
            var g = pixelsPerTileP + padding;


            for (int y = 0; y < h / g; y++)
            {
                for (int x = 0; x < w / g; x++)
                {

                    float ix = (offset + (float)pixelsPerTileP * x + (float)padding * x) / w;
                    float iy = (offset + (float)pixelsPerTileP * y + (float)padding * y) / h;
                    float iw = pixelsPerTileP / w + ix;
                    float ih = pixelsPerTileP / h + iy;

                    tiles.Add(new Tile(
                        new Vector4(
                            ix,
                            iy,
                            iw,
                            ih
                            )));
                }


            }
        }

        private int IntoChunk(int pos)
        {
            return (int)((float)pos % CHUNK_SIZE);
        }

        public void SetColor(int x, int y, Color color)
        {
            var ch = GetOrCreateChunk(x, y);
            ch.SetColor(IntoChunk(x), IntoChunk(y), color);
        }

        public void SetColor(int x, int y, Color4 color)
        {
            SetColor(x, y, color.ToColor());
        }

        private Chunk GetOrCreateChunk(int x, int y)
        {
            int cx = (int)(x / (float)CHUNK_SIZE);
            int cy = (int)(y / (float)CHUNK_SIZE);

            var chunk = chunks.FirstOrDefault(ch => ch.x == cx && ch.y == cy);

            if (chunk == null)
            {
                chunk = new Chunk(this, cx, cy);
                chunks.Add(chunk);
            }

            return chunk;
        }

        public void Render(float deltaTime)
        {
            textureAtlas.Bind();
            for (int i = 0; i < chunks.Count; i++)
            {
                chunks[i].Render(deltaTime);
            }
            textureAtlas.Unbind();
        }
    }
}
