using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.SceneManagment
{
    public static class SceneManager
    {
        private static Scene loadedScene;

        public static Scene GetActiveScene() => loadedScene;

        public static void LoadScene(Scene scene) => loadedScene = scene;

    }
}
