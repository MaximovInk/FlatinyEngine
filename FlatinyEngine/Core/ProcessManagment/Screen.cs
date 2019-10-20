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

        public static float Size = 1;
        public static float Near = 0;
        public static float Far = 1000;
        public static Color backgroundColor = Color.FromArgb(255, 100, 149, 237);

        public static Vector3 Position;
        public static float Rotation;

        public static Matrix4 ProjectionMatrix { get; private set; }

        public static RenderProcess Init(GameWindow window)
        {
            Screen.window = window;
            var rp = new RenderProcess();
            rp.onRender += OnRender;
            return rp;
        }   
        
        /*var proj = ProjectionMatrix =
                        Matrix4.CreateTranslation(new Vector3(-Position.X * window.Width, -Position.Y * window.Height, -Position.Z)) *
                        Matrix4.CreateScale(Size, -Size, 1) * Matrix4.CreateRotationZ(Rotation) *
                        Matrix4.CreateOrthographic(window.Width, window.Height, Near, Far)
        ;*/

        private static void OnRender(float deltaTime)
        {
            GL.ClearColor(backgroundColor);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);

            var proj = ProjectionMatrix =
                        Matrix4.CreateTranslation(new Vector3(-Position.X * window.Width, -Position.Y * window.Height, -Position.Z)) *
                        Matrix4.CreateScale(Size, -Size, 1) * Matrix4.CreateRotationZ(Rotation) *
                        Matrix4.CreateOrthographic(window.Width, window.Height, Near, Far)
        ;

            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);

        }
    }
}
