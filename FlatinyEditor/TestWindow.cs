﻿using System;
using MaximovInk.FlatinyEngine;
using OpenTK;
using MaximovInk.FlatinyEngine.Core.Graphics;
using MaximovInk.FlatinyEngine.Core;
using MaximovInk.FlatinyEngine.Core.Compnents;
using OpenTK.Graphics.OpenGL;

namespace FlatinyEngine
{
    public class TestWindow : Game
    {
        private GameObject go1;
        public static void Main()
        {
            Logger.Log("Initialization window...");
            using (var editorWindow = new TestWindow())
            {
                editorWindow.Title = "Flatiny editor";
                Logger.Log("Running...");
                editorWindow.Run();
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.Adaptive;
            

            var go = new GameObject();
            go1 = new GameObject();

            SceneProcess.AddGameObject(go);
            SceneProcess.AddGameObject(go1);

            go.transform.Scale = new Vector3(1);
            go1.transform.Scale = new Vector3(1);

            go.transform.Position = new Vector3(0,0,0);
            go1.transform.Position = new Vector3(0, 0, 0);
            
            var tx = go.AddComponent(new TextureRenderer()) as TextureRenderer;

            var txtR = go1.AddComponent(new TextRenderer()) as TextRenderer;

            tx.Effect = new Effect(Shaders.VERTEX, Shaders.FRAGMENT);

            txtR.Effect = new Effect(Shaders.VERTEX,Shaders.FRAGMENT);

            txtR.SetFont(new TextureFont("Content/good.ttf"));
            txtR.SetText("Hello world");

            tx.Texture = new Texture2D("Content/16px.png");
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (Input.GetKey(OpenTK.Input.Key.A))
            {
                Screen.Position += new Vector3(-1, 0, 0) * (float)e.Time;
            }
            if (Input.GetKey(OpenTK.Input.Key.D))
            {
                Screen.Position += new Vector3(1, 0, 0) * (float)e.Time;
            }
            if (Input.GetKey(OpenTK.Input.Key.W))
            {
                Screen.Position += new Vector3(0, -1, 0) * (float)e.Time;
            }
            if (Input.GetKey(OpenTK.Input.Key.S))
            {
                Screen.Position += new Vector3(0, 1, 0) * (float)e.Time;
            }

            if (Input.GetKey(OpenTK.Input.Key.F))
            {
                go1.transform.Position += Vector3.UnitX * (float)e.Time*2;
            }
            if (Input.GetKey(OpenTK.Input.Key.G))
            {
                go1.transform.Position -= Vector3.UnitX * (float)e.Time*2;
            }

            Title = 1 / (float)e.Time + "-fps " + (float)e.Time + "-ms" + Screen.Size;

            Screen.Size -= Input.MouseScrollDelta * (float)e.Time*10;
            
        }
    }
}
