using OpenTK;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public struct ColoredVertex
    {
        public Vector2 position;
        public Vector2 texCoord;
        public Vector4 color;

        public static int SizeInBytes { get { return Vector2.SizeInBytes * 2 + Vector4.SizeInBytes; } }

        public ColoredVertex(Vector2 position, Vector2 texCoord, Color color)
        {
            this.position = position;
            this.texCoord = texCoord;
            this.color = new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        public ColoredVertex(Vector2 position, Vector2 texCoord, Vector4 color)
        {
            this.position = position;
            this.texCoord = texCoord;
            this.color = color;
        }

        public Color Color
        {
            get { return Color.FromArgb((int)(color.W*255), (int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255)); }
            set { color = new Vector4(value.R / 255f, value.G / 255f, value.B / 255f, value.A / 255f); }
        }
    }
}
