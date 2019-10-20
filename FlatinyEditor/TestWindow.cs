using MaximovInk.FlatinyEngine;
using OpenTK;
using MaximovInk.FlatinyEngine.Core.Graphics;
using MaximovInk.FlatinyEngine.Core;
using MaximovInk.FlatinyEngine.Core.Compnents;

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

        protected override void OnLoad()
        {
            var go = new GameObject();
            go1 = new GameObject();

            sceneProcess.AddGameObject(go);
            sceneProcess.AddGameObject(go1);

            go.transform.Scale = new Vector3(100);
            go1.transform.Scale = new Vector3(100);

            go.transform.Position = new Vector3(0,1,0);
            go1.transform.Position = new Vector3(1, 0, 0);
            
            var tx = go.AddComponent(new TextureRenderer()) as TextureRenderer;

            var txtR = go1.AddComponent(new TextRenderer()) as TextRenderer;

            tx.Effect = new Effect(Shaders.VERTEX, Shaders.FRAGMENT);

            txtR.Effect = new Effect(Shaders.VERTEX,Shaders.FRAGMENT);

            txtR.SetFont(new TextureFont("Content/good.ttf"));
            txtR.SetText("Hello world");

            tx.Texture = new Texture2D("Content/16px.png");

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
                go1.transform.Position += Vector3.UnitX * deltaTime*2;
            }
            if (Input.GetKey(OpenTK.Input.Key.G))
            {
                go1.transform.Position -= Vector3.UnitX * deltaTime*2;
            }

            Title = 1 / deltaTime + "-fps " + deltaTime + "-ms";

            Screen.Size += Input.MouseScrollDelta * deltaTime*10;
        }
        protected override void OnRender(float deltaTime)
        {
            base.OnRender(deltaTime);

        }
    }
}
