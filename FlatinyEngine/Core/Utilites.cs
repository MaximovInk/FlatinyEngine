using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace MaximovInk.FlatinyEngine.Core
{
    public static class Utilites
    {

        public class Win32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct SIZE
            {
                public int cx;
                public int cy;
            }

            [DllImport("Gdi32.dll")]
            public static extern bool GetTextExtentPoint32(IntPtr hdc, string lpString, int cbString, out SIZE lpSize);

            [DllImport("Gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        }

        public const string CharSheet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-=_+[]{}\\|;:'\".,<>/?`~ ";

        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static Color ToColor(this Color4 color)
        {
            return Color.FromArgb((int)(color.A*255f), (int)(color.R * 255f), (int)(color.G * 255f), (int)(color.B * 255f));
        }

        public static Color4 ToColor4(this Color color)
        {
            return new Color4(color.R, color.G, color.B, color.A);
        }

        public static Vector4 ToVector(this Color color)
        {
            return new Vector4(color.R/255f,color.G/255f,color.B/255f,color.A/255f);
        }

        public static Vector4 ToVector(this Color4 color)
        {
            return new Vector4(color.R, color.G, color.B, color.A);
        }

        public static Color4 ToColor(this Vector4 color)
        {
            return new Color4(color.X, color.Y, color.Z, color.W);
        }


        public static T Copy<T>(this T obj) where T : class, ICloneable
        {
            return obj.Clone() as T;
        }

        public static string GetOsName(OperatingSystem os_info)
        {
            string version =
                os_info.Version.Major.ToString() + "." +
                os_info.Version.Minor.ToString();
            switch (version)
            {
                case "10.0": return "10/Server 2016";
                case "6.3": return "8.1/Server 2012 R2";
                case "6.2": return "8/Server 2012";
                case "6.1": return "7/Server 2008 R2";
                case "6.0": return "Server 2008/Vista";
                case "5.2": return "Server 2003 R2/Server 2003/XP 64-Bit Edition";
                case "5.1": return "XP";
                case "5.0": return "2000";
            }
            return "Unknown";
        }

        public static T[] Combine<T>(params IEnumerable<T>[] items) =>
                   items.SelectMany(i => i).Distinct().ToArray();

        public static T[] ConcatArrays<T>(params T[][] args)
        {
            if (args == null)
                throw new ArgumentNullException();

            var offset = 0;
            var newLength = args.Sum(arr => arr.Length);
            var newArray = new T[newLength];

            foreach (var arr in args)
            {
                Buffer.BlockCopy(arr, 0, newArray, offset, arr.Length);
                offset += arr.Length;
            }

            return newArray;
        }

        public static void Fill<T>(this T[] source, T value)
        {
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = value;
            }
        }

        public static int Push<T>(this T[] source, T value)
        {
            var index = Array.IndexOf(source, default(T));

            if (index != -1)
            {
                source[index] = value;
            }

            return index;
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        public static uint ToUInt(this Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        public static Color ToColor(this uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }

        public static Bitmap FontToTexture(Font font,out List<Vector4> texCoords, out List<Vector2> pSizes)
        {
            var backgroundColor = Color.FromArgb(0,0,0,0);
            var textColor = Color.FromArgb(255,0, 0, 0);
            var img = new Bitmap(1, 1);
            System.Drawing.Graphics drawing = System.Drawing.Graphics.FromImage(img);

            SizeF textSize = SizeF.Empty;

            for (int i = 0; i < CharSheet.Length; i++)
            {
                var s = drawing.MeasureString(CharSheet[i].ToString(), font);
                textSize = new SizeF(s.Width + textSize.Width, Math.Max(textSize.Height, s.Height));
            }

            img.Dispose();
            drawing.Dispose();

            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = System.Drawing.Graphics.FromImage(img);

            drawing.Clear(backgroundColor);

            Brush textBrush = new SolidBrush(textColor);

            texCoords = new List<Vector4>();
            pSizes = new List<Vector2>();
            var chars = CharSheet.ToCharArray();

            float total = 0;

            for (int p = 0; p < CharSheet.Length; p++)
            {


                char c = chars[p];
                drawing.DrawString(c.ToString(), font, textBrush,
                    total, 0);

                var sizeF = drawing.MeasureString(c.ToString(), font);

                var x = total / img.Width;
                var width = sizeF.Width / img.Width;
                var height = sizeF.Height / img.Height;
                
                texCoords.Add(new Vector4(x+width*0.1f, 0, width*0.9f, height));
                //pSizes.Add(new Vector2(1, 1));
                pSizes.Add(new Vector2((1-1/sizeF.Width)*0.8f,1-1/sizeF.Height));
                total += sizeF.Width;
            }

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}
