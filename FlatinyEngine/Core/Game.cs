using OpenTK;
using OpenTK.Graphics;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using MaximovInk.FlatinyEngine.Core.Graphics;
using MaximovInk.FlatinyEngine.Core.GUI;

namespace MaximovInk.FlatinyEngine.Core
{
    public class Game : IDisposable
    {
        private static GameWindowWrapper Window;

        public string Title { get { return Window.Title; } set { Window.Title = value; } }

        public VSyncMode VSync { get { return Window.VSync; } set { Window.VSync = value; } }

        public Size ClientSize => Window.ClientSize;

        public static int Width { get { return Window.Width; } set { Window.Width = value; } }
        public static int Height { get { return Window.Height; } set { Window.Height = value; } }

        public float Time { get; private set; }

        protected SceneGraph sceneProcess;
 
        public Game()
        {
            Window = new GameWindowWrapper(800, 600, GraphicsMode.Default, string.Empty);
        }

        public void Run()
        {
            sceneProcess = new SceneGraph();
            var inputProcess = Input.Init(Window);
            var camProcess = Screen.Init(Window);

            GUILayer.Init(Window, out var guiRenderProcess, out var guiUpdateProcess);

            ProcessManager.RegisterUpdateHandler(sceneProcess, 1);
            ProcessManager.RegisterUpdateHandler(inputProcess, 0);
            ProcessManager.RegisterUpdateHandler(guiUpdateProcess, 2);

            ProcessManager.RegisterRenderHandler(sceneProcess, 1);
            ProcessManager.RegisterRenderHandler(camProcess, 2);
            ProcessManager.RegisterRenderHandler(guiRenderProcess, 0);
           

            Window.Closed += (object sender, EventArgs e) => OnWindowClosed();
            Window.Closing += (object sender, System.ComponentModel.CancelEventArgs e) => OnWindowClosing();
            Window.Load += (object sender, EventArgs e) => { Window.Icon = Properties.Resources.icon;  OnLoad(); };
            Window.Unload += (object sender, EventArgs e) => OnUnload();
            Window.UpdateFrame += (object sender, FrameEventArgs e) => { OnUpdate((float)e.Time); Time += (float)e.Time; };
            Window.RenderFrame += (object sender, FrameEventArgs e) => { OnRender((float)e.Time);  };
            Window.Resize += (object sender, EventArgs e) => OnResize();

            Window.Run();
        }

        protected virtual void OnResize()
        {
            GL.Viewport(0, 0, Width, Height);
        }


        protected virtual void OnRender(float deltaTime)
        {
            ProcessManager.Render(deltaTime);
            SwapBuffers();
        }

        protected virtual void OnUpdate(float deltaTime)
        {
            ProcessManager.Update(deltaTime);
        }

        protected virtual void OnUnload()
        {
            
        }

        protected virtual void OnLoad()
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

        protected virtual void OnWindowClosing()
        {
            
        }

        protected virtual void OnWindowClosed()
        {
            
        }



        private void SwapBuffers() => Window.SwapChain();

        public void Dispose() =>
            Window.Dispose();
    }
}
