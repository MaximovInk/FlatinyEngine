using MaximovInk.FlatinyEngine.Core.Compnents;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public static class Screen
    {
        private static GameWindow window;

        public static Vector2 Size { get { return new Vector2(window.Width,window.Height); } }

        public static float Scale { get { return scale; } set { scale = value; OnResize(); } }
        private static float scale = 1;
        public static float Near { get { return near; } set { near = value; OnResize(); } }
        private static float near = 0;
        public static float Far { get { return far; } set { far = value; OnResize(); } }
        private static float far = 1000;
        public static Color BackgroundColor = Color.FromArgb(255, 100, 149, 237);

        public static float aspect { get; private set; }

        public static Vector3 Position { get; set; }
        public static float Rotation { get; set; }

        public static Matrix4 WorldProjectionMatrix { get; private set; }

        public static Matrix4 ScreenProjectionMatrix { get; private set; }

        public static void Init(GameWindow window)
        {
            Screen.window = window;
            window.RenderFrame += Render;
        }

        private static void Render(object sender, FrameEventArgs e)
        {
            GL.ClearColor(BackgroundColor);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private static void OnResize()
        {
            aspect = (float)window.Width / window.Height;

            ScreenProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, Near, Far);

            var proj = WorldProjectionMatrix =
                        Matrix4.CreateTranslation(new Vector3(-Position.X, -Position.Y, -Position.Z)) *
                        Matrix4.CreateScale(1, -1, 1) * Matrix4.CreateRotationZ(Rotation) *
                        Matrix4.CreateOrthographic(aspect*Scale, 1*Scale, Near, Far);
        }
    }
}
