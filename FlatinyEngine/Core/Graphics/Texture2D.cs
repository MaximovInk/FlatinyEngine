using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public class Texture2D : IDisposable
    {
        private int Handle;
        private int Width;
        private int Height;

        public int GetWidth() => Width;

        public int GetHeight() => Height;

        public static readonly Texture2D OnePixel;

        public Texture2D(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            Handle = GL.GenTexture();

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
               System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            
            Width = bitmap.Width;
            Height = bitmap.Height;

            SetData(data.Scan0, PixelFormat.Rgba, PixelInternalFormat.Rgba, PixelType.UnsignedByte);

            bitmap.UnlockBits(data);

            SetDefaultParams();
        }

        public Texture2D(int width , int height)
        {
            Width = width;
            Height = height;
            Handle = GL.GenTexture();
            SetDefaultParams();
        }

        public void SetData(IntPtr data,PixelFormat format = PixelFormat.Rgba, PixelInternalFormat iFormat = PixelInternalFormat.Rgba, PixelType type = PixelType.UnsignedByte)
        {
            Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, iFormat, Width, Height, 0, format, type, data);
            Unbind();
        }

        public void SetData<T>(PixelFormat format, PixelInternalFormat iFormat, PixelType type, T[] data) where T : struct
        {
            Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, iFormat, Width, Height, 0, format, type, data);
            Unbind();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D,0);
        }

        private void SetDefaultParams()
        {
            Bind();
            SetWrapMode(TextureWrapMode.Clamp, TextureWrapMode.Clamp);
            SetFilter(TextureMagFilter.Nearest, TextureMinFilter.Nearest);
            Unbind();
        }

        public void SetFilter(TextureMagFilter magFilter, TextureMinFilter minFilter)
        {
            Bind();
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
            Unbind();
        }

        public void SetWrapMode(TextureWrapMode s, TextureWrapMode t)
        {
            Bind();
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)s);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)t);
            Unbind();
        }

        public void Dispose()
        {
            GL.DeleteTexture(Handle);
        }

        static Texture2D()
        {
            OnePixel = new Texture2D(1, 1);
            var bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.White);
            var data = bmp.LockBits(new Rectangle(0, 0, 1, 1), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            OnePixel.SetData(data.Scan0);

            bmp.UnlockBits(data);

        }
    }

    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Rectangle UV = Rectangle.Empty;
        public Vector2 Pivot = Vector2.Zero;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            UV = new Rectangle(0, 0, Texture.GetWidth(), Texture.GetHeight());
            Pivot = new Vector2(Texture.GetWidth() / 2, Texture.GetHeight() / 2);
        }

        public Sprite(Texture2D texture, Rectangle uv)
        {
            Texture = texture;
            UV = uv;
            Pivot = new Vector2(Texture.GetWidth() / 2, Texture.GetHeight() / 2);
        }
    }
}
