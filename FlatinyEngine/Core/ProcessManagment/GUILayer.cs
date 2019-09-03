using MaximovInk.FlatinyEngine.Core.GUI;
using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.ProcessManagment
{
    public static class GUILayer
    {
        private static List<GUICanvas> GUIs = new List<GUICanvas>();

        private static GameWindow window;

        //public static float guiUnitX => window.Width/100.0f;
        //public static float guiUnitY => window.Height/100.0f;

        public static int Height => window.Height;
        public static int Width => window.Width;


        public static void RegisterGUI(GUICanvas canvas)
        {
            if (GUIs.Contains(canvas))
                return;

            GUIs.Add(canvas);
        }

        public static void RemoveGUI(GUICanvas canvas)
        {
            GUIs.Remove(canvas);
        }

        public static void Init(GameWindow window, out RenderProcess renderHandler , out UpdateProcess updateHandler)
        {
            GUILayer.window = window;
            renderHandler = new RenderProcess();
            renderHandler.onRender += Render;
            updateHandler = new UpdateProcess();
            updateHandler.onUpdate += Update;
        }

        private static void Update(float deltaTime)
        {
            for (int i = 0; i < GUIs.Count; i++)
            {
                GUIs[i].Update();
            }
        }

        private static void Render(float deltaTime)
        {
            for (int i = 0; i < GUIs.Count; i++)
            {
                GUIs[i].Render();
            }
        }
    }
}
