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
using OpenTK.Graphics.OpenGL;

namespace FlatinyEngine
{
    public class EditorWindow : Game
    {
        private GUICanvas GUICanvas;
        private GameObject go;

        private TextRenderer dbg;

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

            var imgRect = GUICanvas.AddRect<GUIButton>();
            //imgRect.SetTexture(Texture2D.OnePixel);
            imgRect.Rect = new Rectangle(0, 0, 20, 20);

            var child = new GUIRawImage(GUICanvas);
            imgRect.Graphics = child;
            child.SetParent(imgRect);
            child.Texture=Texture2D.OnePixel;
            child.Color = Color.Gray;
            child.Rect = new Rectangle(0, 0, 100, 100);

            go = new GameObject();
            var go1 = new GameObject();
            var go2 = new GameObject();
            var go3 = new GameObject();
            var go4 = new GameObject();
            sceneProcess.AddGameObject(go4);
            sceneProcess.AddGameObject(go);
            sceneProcess.AddGameObject(go1);
            sceneProcess.AddGameObject(go2);
            sceneProcess.AddGameObject(go3);
 
            go.transform.Scale = new Vector3(100);
            go1.transform.Scale = new Vector3(100);
            go2.transform.Scale = new Vector3(100);
            go3.transform.Scale = new Vector3(100);
            go4.transform.Scale = new Vector3(100);
            go.transform.Position = new Vector3(0,100,0);
            go1.transform.Position = new Vector3(100, 100, 0);
            go2.transform.Position = new Vector3(200, 0, 0);
            /*var tx = go.AddComponent<TextureRenderer>();
            var tx1 = go1.AddComponent<TextureRenderer>();
            var tx2 = go2.AddComponent<TextureRenderer>();
            var txtR = go3.AddComponent<TextRenderer>();*/
            var tmp = go4.AddComponent(new TilemapRenderer()) as TilemapRenderer;
            var tx = go.AddComponent(new TextureRenderer()) as TextureRenderer;
            var tx1 = go1.AddComponent(new TextureRenderer()) as TextureRenderer;
            var tx2 = go2.AddComponent(new TextureRenderer()) as TextureRenderer;
            var txtR = go3.AddComponent(new TextRenderer()) as TextRenderer;
            /*var mesh = go4.AddComponent<MeshRenderer>().Mesh = Mesh.Grid(10,10);

            mesh.vertices[0].color = Color4.Red.ToVector();
            mesh.vertices[1].color = Color4.Red.ToVector();
            mesh.vertices[2].color = Color4.Red.ToVector();
            mesh.vertices[3].color = Color4.Red.ToVector();

            mesh.ApplyData();*/
            //            var tmp = go4.AddComponent<TilemapRenderer>();
            
            tmp.textureAtlas = new Texture2D("Content/tiles.png");
            tmp.SliceAtlas();

            tmp.SetTile(1, 2, 0);
            tmp.SetTile(2, 2, 1);
            tmp.SetTile(3, 3, 2);
            tmp.SetTile(4, 3, 3);
            tmp.SetTile(5, 3, 4);
            tmp.SetTile(6, 3, 5);

            tmp.SetTile(1, 4, 6);
            tmp.SetTile(2, 5, 7);
            tmp.SetTile(3, 6, 8);
            tmp.SetTile(4, 7, 9);
            tmp.SetTile(5, 8, 10);
            tmp.SetTile(6, 9, 11);


            tmp.UpdateMesh();

            tx.Texture = new Texture2D("Content/16px.png");
            txtR.SetFont(new TextureFont("Content/good.ttf"));
            txtR.SetText("Flatiny | Hello world !");

            dbg = txtR;
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

            Title = 1 / deltaTime + "-fps " + deltaTime + "-ms";
            if(GUICanvas.Dragged != null || GUICanvas.Over != null)
                dbg.SetText("Dr:"+GUICanvas.Dragged + " Ov:" + GUICanvas.Over);

            Screen.Size += Input.MouseScrollDelta * deltaTime*10;
        }
        protected override void OnRender(float deltaTime)
        {
            base.OnRender(deltaTime);

        }
    }
}
