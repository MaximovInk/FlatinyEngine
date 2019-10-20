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
        public int Width { get; }
        public int Height { get; }

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

        public void Bind(Effect effect)
        {
            Bind();
            effect.SetUniform("texture0", Handle);
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
        public Texture2D Texture { get; }
        public Rectangle Rect { get; } = Rectangle.Empty;
        public Vector4 Uv { get; } = Vector4.Zero;
        public Vector2 Pivot { get; } = Vector2.Zero;

        public Sprite(Texture2D texture) : this (texture, new Rectangle(0, 0, texture.Width, texture.Height))
        {
            Texture = texture;
        }

        public Sprite(Texture2D texture, Rectangle rect)
        {
            Texture = texture;
            Rect = rect;
            float w = texture.Width;
            float h = texture.Height;
            Uv = new Vector4(rect.X / w, rect.Y / h, rect.Width / w, rect.Height / h);
            Pivot = new Vector2(rect.Width / 2f, Texture.Height / 2f);
        }
    }
}
