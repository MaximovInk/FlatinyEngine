using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public static class SpriteBatch
    {
        private static bool _beginCalled = false;

        private static List<RenderOperation> _operations;


        private abstract class RenderOperation
        {
            public abstract void Render();
        }

        private class Texture2D_RO : RenderOperation
        {
            private readonly Texture2D tex;
            private readonly Vector2 pos;
            private readonly Vector2 scale;
            private readonly float rotation;
            private readonly Color color;

            public override void Render()
            {
                if (tex == null)
                {
                    GL.Color4(color);
                }
                else
                {

                }
            }
        }



        public static void Begin()
        {

        }
        /*
        public static void DrawText(TextureFont font, string text, Vector3 position, Color color, float scale = 10,float spacing = 0)
        {
            font.Bind();
            

            string[] lines = text.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
            );

            if (lines.Length > 0)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                   

                    for (int i = 0; i < lines[j].Length; i++)
                    {
                        GL.PushMatrix();
                        GL.Scale(scale, scale, scale);
                        GL.Translate(new Vector3(position.X, position.Y+j*font.charHeight , position.Z));
                        int ch = Core.Utilites.CharSheet.IndexOf(lines[j][i]);

                        float x = font.texCoords[ch].X;
                        float y = font.texCoords[ch].Y;
                        float width = font.texCoords[ch].Z;
                        float height = font.texCoords[ch].W;

                        float char_width = font.charWidth;
                        float char_height = font.charHeight;

                        GL.Begin(PrimitiveType.Triangles);

                        GL.Color4(1f, 1f, 1f, 1f);

                        GL.TexCoord2(x, y); GL.Vertex2(i * char_width + spacing * i, 0);
                        GL.TexCoord2(x + width, y + height); GL.Vertex2(i * char_width + char_width + spacing * i, char_height);
                        GL.TexCoord2(x, y + height); GL.Vertex2(i * char_width + spacing * i, char_height);

                        GL.TexCoord2(x, y);  GL.Vertex2(i * char_width + spacing * i, 0);
                        GL.TexCoord2(x + width, y); GL.Vertex2(i * char_width + char_width + spacing * i,0);
                        GL.TexCoord2(x + width, y + height); GL.Vertex2(i * char_width+ char_width + spacing * i, char_height);

                        GL.End();
                        GL.PopMatrix();
                    }
                    
                }
            }
        }
        */
        public static void DrawTexture(Texture2D texture, Vector3 position, Vector2 size, Color color)
        {
            texture.Bind();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(1f, 1f, 1f, 1f);

            var half = size / 2;

            GL.TexCoord2(0, 0); GL.Vertex2(position.X - half.X, position.Y - half.Y);
            GL.TexCoord2(1, 1); GL.Vertex2(position.X + half.X, position.Y + half.Y);
            GL.TexCoord2(0, 1); GL.Vertex2(position.X - half.X, position.Y + half.Y);

            GL.TexCoord2(0, 0); GL.Vertex2(position.X - half.X, position.Y - half.Y);
            GL.TexCoord2(1, 0); GL.Vertex2(position.X + half.X, position.Y - half.Y);
            GL.TexCoord2(1, 1); GL.Vertex2(position.X + half.X, position.Y + half.Y);

            GL.End();
        }

        /* private static void DrawChar(int character, float x, float y, float charHeight, int VBO)
         {
             GL.PushMatrix();

             x = x / (Game.Width / 2) - 1;
             y = (Game.Height - y - charHeight) / (Game.Height / 2) - 1;
             Vector3 Position = new Vector3(x, y, 0);

             Matrix4 modelMatrix = Matrix4.CreateScale(charHeight) * Matrix4.CreateTranslation(Position);
             GL.LoadMatrix(ref modelMatrix);

             // Draw
             GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
             GL.EnableClientState(ArrayCap.VertexArray);
             GL.EnableClientState(ArrayCap.TextureCoordArray);
             GL.EnableClientState(ArrayCap.ColorArray);

             GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, character);
             GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, Vector2.SizeInBytes* character);
             GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, Vector2.SizeInBytes * 2* character);

             GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

             GL.DisableClientState(ArrayCap.VertexArray);
             GL.DisableClientState(ArrayCap.TextureCoordArray);
             GL.DisableClientState(ArrayCap.ColorArray);
             GL.PopMatrix();
         }*/


        public static void End()
        {
            if (!_beginCalled)
                throw new InvalidOperationException("Begin must be called before calling End.");



            _beginCalled = false;

        }
    }
}
