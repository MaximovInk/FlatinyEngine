using MaximovInk.FlatinyEngine.Core.Compnents;
using MaximovInk.FlatinyEngine.Core.ProcessManagment;
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

        public static float Size { get { return size; } set { size = value; OnResize(); } }
        private static float size = 1;
        public static float Near { get { return near; } set { near = value; OnResize(); } }
        private static float near = 0;
        public static float Far { get { return far; } set { far = value; OnResize(); } }
        private static float far = 1000;
        public static Color BackgroundColor = Color.FromArgb(255, 100, 149, 237);

        public static Vector3 Position { get; set; }
        public static float Rotation { get; set; }

        public static Matrix4 WorldProjectionMatrix { get; private set; }

        public static Matrix4 ScreenProjectionMatrix { get; private set; }

        public static RenderProcess Init(GameWindow window)
        {
            Screen.window = window;
            var rp = new RenderProcess();
            rp.onRender += OnRender;
            OnResize();
            return rp;
        }

        private static void OnResize()
        {
            ScreenProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, Near, Far);

            var proj = WorldProjectionMatrix =
                        Matrix4.CreateTranslation(new Vector3(-Position.X * window.Width, -Position.Y * window.Height, -Position.Z)) *
                        Matrix4.CreateScale(Size, -Size, 1) * Matrix4.CreateRotationZ(Rotation) *
                        Matrix4.CreateOrthographic(window.Width, window.Height, Near, Far);

        }

        private static void OnRender(float deltaTime)
        {
            GL.ClearColor(BackgroundColor);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
