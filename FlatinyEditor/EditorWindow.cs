using System;
using System.ComponentModel;
using MaximovInk.FlatinyEngine;
using OpenTK;
using OpenTK.Graphics;
using MaximovInk.FlatinyEngine.Core.GUI;
using MaximovInk.FlatinyEngine.Core.Graphics;
using MaximovInk.FlatinyEngine.Core;
using MaximovInk.FlatinyEngine.Core.Compnents;
using System.Drawing;

namespace FlatinyEngine
{
    public class EditorWindow : Game
    {
        private GUICanvas GUICanvas;
        private GameObject go;

        public static void Main()
        {
            Logger.Log("Initialization window...");
            using (var editorWindow = new EditorWindow())
            {
                editorWindow.Title = "Flatiny editor";
                Logger.Log("Running...");
                editorWindow.Run();
            }

        }

        protected override void OnLoad()
        {
            GUICanvas = new GUICanvas();
           
            var imgRect = GUICanvas.AddRect<GUIImage>();
            imgRect.SetTexture(Texture2D.OnePixel);
            imgRect.Rect = new Rectangle(0, 0, 20, 20);

            var child = new GUIImage(GUICanvas);
            child.SetParent(imgRect);
            child.SetTexture(Texture2D.OnePixel);
            child.SetColor(Color.Black);
            child.Rect = new Rectangle(0, 0, 50, 50);

            go = new GameObject();
            var go1 = new GameObject();
            var go2 = new GameObject();
            var go3 = new GameObject();
            sceneProcess.AddGameObject(go);
            sceneProcess.AddGameObject(go1);
            sceneProcess.AddGameObject(go2);
            sceneProcess.AddGameObject(go3);
            go.transform.Scale = new Vector3(100);
            go1.transform.Scale = new Vector3(100);
            go2.transform.Scale = new Vector3(100);
            go3.transform.Scale = new Vector3(100);
            go.transform.Position = new Vector3(0,100,0);
            go1.transform.Position = new Vector3(100, 100, 0);
            go2.transform.Position = new Vector3(200, 0, 0);
            var tx = go.AddComponent<TextureRenderer>();
            var tx1 = go1.AddComponent<TextureRenderer>();
            var tx2 = go2.AddComponent<TextureRenderer>();
            var txtR = go3.AddComponent<TextRenderer>();
            tx.Texture = new Texture2D("Content/16px.png");
            txtR.SetFont(new TextureFont("Content/good.ttf"));
            txtR.SetText("Flatiny | Hello world !");
            tx2.Texture = tx1.Texture = tx.Texture;
            tx2.SetColor(Color.Red);

  
          
            base.OnLoad();
        }

        protected override void OnWindowClosing()
        {
            Logger.Log("Quitting...");
        }

        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (Input.GetKey(OpenTK.Input.Key.A))
            {
                Screen.Position += new Vector3(-1, 0, 0) * deltaTime;
            }
            if (Input.GetKey(OpenTK.Input.Key.D))
            {
                Screen.Position += new Vector3(1, 0, 0) * deltaTime;
            }
            if (Input.GetKey(OpenTK.Input.Key.W))
            {
                Screen.Position += new Vector3(0, -1, 0) * deltaTime;
            }
            if (Input.GetKey(OpenTK.Input.Key.S))
            {
                Screen.Position += new Vector3(0, 1, 0) * deltaTime;
            }

            if (Input.GetKey(OpenTK.Input.Key.F))
            {
                go.transform.Scale += Vector3.One*deltaTime;
            }
            if (Input.GetKey(OpenTK.Input.Key.G))
            {
                go.transform.Scale -= Vector3.One * deltaTime;
            }

            Screen.Size += Input.MouseScrollDelta * deltaTime*10;
        }
        protected override void OnRender(float deltaTime)
        {
            base.OnRender(deltaTime);
        }
    }
}
