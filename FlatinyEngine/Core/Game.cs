using OpenTK;
using OpenTK.Graphics;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using MaximovInk.FlatinyEngine.Core.Graphics;

namespace MaximovInk.FlatinyEngine.Core
{
    public class Game : GameWindow
    {
        protected Scene Scene;
 
        public Game():base(800,600,GraphicsMode.Default, "Flatiny")
        {
        }

        public new void Run()
        {
            Resize += OnResize;

            Scene = new Scene();

            Input.Init(this);
            Screen.Init(this);
            SceneManager.Init(this);
            GUI.Init(this);
            RenderFrame += OnPostRender;

            VSync = VSyncMode.Off;

            base.Run();
        }

        private void OnPostRender(object sender, FrameEventArgs e)
        {
            SwapBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Multisample);

            Logger.Log("OpenGL version : " + GL.GetString(StringName.Version));
            Logger.Log("OS : " + Utilites.GetOsName( Environment.OSVersion));

            Logger.Log("Loading complete");
        }
        
        private void OnResize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }
    }
}
