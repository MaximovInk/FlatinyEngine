using System.Drawing;
using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK.Graphics;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public interface w
    {
        bool enabled { get; set; }
        GameObject gameObject { get; set; }
        int offset { get; set; }
        int padding { get; set; }
        int pixelsPerTileP { get; set; }
        string tag { get; set; }
        Texture2D textureAtlas { get; set; }

        int GetTile(int x, int y);
        void Render(float deltaTime);
        void SetColor(int x, int y, Color color);
        void SetColor(int x, int y, Color4 color);
        void SetTile(int x, int y, int tileId);
        void SliceAtlas();
        void UpdateMesh();
    }
}