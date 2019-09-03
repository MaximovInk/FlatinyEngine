using OpenTK;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public struct Vertex
    {
        public Vector2 position;
        public Vector2 texCoord;

        public static int SizeInBytes { get { return Vector2.SizeInBytes * 2; } }

        public Vertex(Vector2 position, Vector2 texCoord)
        {
            this.position = position;
            this.texCoord = texCoord;
        }

    }
}
