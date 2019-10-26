using OpenTK;
using OpenTK.Graphics;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using MaximovInk.FlatinyEngine.Core.Graphics;

namespace MaximovInk.FlatinyEngine.Core
{
    public class Game : GameWindow
    {
        protected SceneGraph SceneProcess;
 
        public Game():base(800,600,GraphicsMode.Default, "Flatiny")
        {
        }

        public new void Run()
        {
            SceneProcess = new SceneGraph();
            var inputProcess = Input.Init(this);
            var camProcess = Screen.Init(this);

            ProcessManager.RegisterUpdateHandler(SceneProcess, 1);
            ProcessManager.RegisterUpdateHandler(inputProcess, 0);

            ProcessManager.RegisterRenderHandler(SceneProcess, 1);
            ProcessManager.RegisterRenderHandler(camProcess, 2);

            VSync = VSyncMode.Off;

            base.Run();
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
        
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            ProcessManager.Render((float)e.Time);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            ProcessManager.Update((float)e.Time);
        }

    }
}
