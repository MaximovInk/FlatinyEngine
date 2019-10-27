using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace MaximovInk.FlatinyEngine.Core
{
    public static class GUI
    {
        private static List<GUICanvas> canvases = new List<GUICanvas>();

        public static GUIRect Drag { get; private set; }
        public static GUIRect Over { get; private set; }
        private static GUIRect lastOver;

        public static void Init(GameWindow gameWindow)
        {
            gameWindow.UpdateFrame += Update;
            gameWindow.RenderFrame += Render;
            gameWindow.Resize += Resize;
        }

        private static void Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < canvases.Count; i++)
            {
                canvases[i].UpdateLayout();
            }
        }

        public static void RegisterCanvas(GUICanvas canvas)
        {
            canvases.Add(canvas);
        }

        public static void RemoveCanvas(GUICanvas canvas)
        {
            canvases.Remove(canvas);
        }

        private static void Update(object sender, FrameEventArgs e)
        {

            for (int i = 0; i < canvases.Count; i++)
            {
                canvases[i].Update((float)e.Time);

                Over = canvases[i].GetIntersection() ?? Over;
            }

            Over?.OnMouseOver();

            if (Over != lastOver)
                Over?.OnMouseEnter();

            if (Over != lastOver)
                lastOver?.OnMouseExit();

            if (Input.GetMouseButtonDown(MouseButton.Left))
            {
                Drag = Over;
                Drag?.OnMouseDown();
            }

            Drag?.OnMousePress();

            if (Input.GetMouseButtonUp(MouseButton.Left))
            {
                Drag?.OnMouseUp();
                Drag = null;
            }

            lastOver = Over;
            Over = null;
        }

        private static void Render(object sender, FrameEventArgs e)
        {
            for (int i = 0; i < canvases.Count; i++)
            {
                canvases[i].Render((float)e.Time);
            }
        }
    }


}
