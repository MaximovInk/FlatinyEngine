using System;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.Adaptive;

            GUICanvas canvas = new GUICanvas();
            canvas.Visible = false;
            GUIRawImage image = new GUIRawImage();
            GUIButton button = new GUIButton();
            button.AddChild(image);
            canvas.AddChild(button);

            var go = new GameObject();
            go1 = new GameObject();

            Scene.AddGameObject(go);
            Scene.AddGameObject(go1);

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

            button.SetXConstraint(new GUIRelativeConstraint(0.2f));
            button.SetYConstraint(new GUIRelativeConstraint(0.2f));
            button.SetWidthConstraint(new GUIRelativeConstraint(0.5f));
            button.SetHeightConstraint(new GUIRelativeConstraint(0.5f));

            image.Texture = tx.Texture;
            image.RaycastTarget = false;
            button.Graphics = image;

            image.SetXConstraint(new GUIRelativeConstraint(0f));
            image.SetYConstraint(new GUIRelativeConstraint(0f));
            image.SetHeightConstraint(new GUIRelativeConstraint(1f));
            image.SetWidthConstraint(new GUIRelativeConstraint(1f));

            SceneManager.LoadScene(Scene);

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
                go1.transform.Position += Vector3.UnitX * (float)e.Time*5;
            }
            if (Input.GetKey(OpenTK.Input.Key.G))
            {
                go1.transform.Position -= Vector3.UnitX * (float)e.Time*5;
            }

            Title = 1 / (float)e.Time + "-fps " + (float)e.Time + "-ms";

            Screen.Scale -= Input.MouseScrollDelta * (float)e.Time*10;
            
        }
    }
}
