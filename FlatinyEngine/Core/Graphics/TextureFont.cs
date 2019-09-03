using System;
using System.Drawing;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public struct Char
    {
        public char Value;
        public Vector4 Uv;
        public Vector2 PSize;

        public Char(char value, Vector4 uv, Vector2 size)
        {
            Value = value;
            Uv = uv;
            PSize = size;
        }
    }

    public sealed class TextureFont : IDisposable
    {
        private Texture2D texture;
        public int CharWidth { get; private set; }
        public int CharHeight { get; private set; }
        private Char[] Chars;
        public int CharsCount { get; private set; }
        private Bitmap rawData;

        public Char GetChar(int index)
        {
            return Chars[index];
        }

        public Char GetChar(char character)
        {
            return Chars.FirstOrDefault(n=>n.Value==character);
        }

        public void Bind()
        {
            texture.Bind();
        }

        public void Unbind()
        {
            texture.Unbind();
        }

        public float MeasureString(string text) => text.Length * 12;

        public TextureFont(string path, int renderSize = 100)
        {
            CharsCount = Utilites.CharSheet.Length;
            
            PrivateFontCollection collection = new PrivateFontCollection();
            collection.AddFontFile(path);
            FontFamily fontFamily = new FontFamily(collection.Families[0].Name, collection);
            var font = new Font(fontFamily,renderSize,GraphicsUnit.Pixel);

            rawData = Utilites.FontToTexture(font,out var texCoords,out var pSizes);

            Chars = new Char[CharsCount];
            for (int i = 0; i < Chars.Length; i++)
            {
                Chars[i] = new Char(Utilites.CharSheet[i], texCoords[i], pSizes[i]);
            }

            CharWidth = (int)Math.Ceiling(font.Size);
            CharHeight = font.Height;

            var data = rawData.LockBits(new Rectangle(0, 0, rawData.Width, rawData.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            texture = new Texture2D(rawData.Width, rawData.Height);
            texture.SetData(data.Scan0);
            texture.SetFilter(TextureMagFilter.Linear, TextureMinFilter.Linear);
            rawData.UnlockBits(data);
            
        }

        public void SaveTexture(string path)
        {
            rawData.Save(path);
        }

        public void Dispose()
        {
            texture.Dispose();
            rawData.Dispose();
            Chars = null;

        }
    }
}
