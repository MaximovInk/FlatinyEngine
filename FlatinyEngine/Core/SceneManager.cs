using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public static class SceneManager
    {
        private static List<Scene> Scenes = new List<Scene>();

        public static void LoadScene(Scene scene)
        {
            Scenes.Clear();

            Scenes.Add(scene);
        }

        public static void Init(GameWindow window)
        {
            window.UpdateFrame += Update;
            window.RenderFrame += Render;
        }

        private static void Update(object sender, FrameEventArgs e)
        {
            for (int i = 0; i < Scenes.Count; i++)
            {
                Scenes[i].Update((float)e.Time);
            }
        }

        private static void Render(object sender, FrameEventArgs e)
        {
            for (int i = 0; i < Scenes.Count; i++)
            {
                Scenes[i].Render((float)e.Time);
            }
        }
    }
}
