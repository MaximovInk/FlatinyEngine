using MaximovInk.FlatinyEngine.Core;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using MaximovInk.FlatinyEngine.Core.SceneManagment;
using MaximovInk.FlatinyEngine.Core.Compnents;
using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.IO;

using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection;

namespace MaximovInk.FlatinyEngine
{
    public class TestGame : Game
    {

        public static float min = 100;
        public static float max = 0;
        public static float time = 0;
        public static float average = 0;

        private int ticks;

        private List<IPlugin> plugins = new List<IPlugin>();

        public void Load(String file)
        {
            if (!File.Exists(file) || !file.EndsWith(".dll", true, null))
                return;

            Assembly asm = null;

            try
            {
                asm = Assembly.LoadFile(file);
            }
            catch (Exception)
            {
                // unable to load
                return;
            }

            Type pluginInfo = null;
            try
            {
                Type[] types = asm.GetTypes();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                assemblies.ToList().ForEach(a => { Console.WriteLine(a.GetName().Name); });

                Assembly core = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name.Equals("FlatinyEngine"));
                var type = typeof(IPlugin);
                foreach (var t in types)
                {
                    Console.WriteLine(t);
                    if (type.IsAssignableFrom(t))
                    {
                        pluginInfo = t;
                        break;
                    }
                }
                if (pluginInfo != null)
                {
                    Object o = Activator.CreateInstance(pluginInfo);
                    IPlugin plugin = (IPlugin)o;
                    plugins.Add(plugin);
                }
            }
            catch (Exception)
            {
            }
        }

        public void LoadAll()
        {
            String[] files = Directory.GetFiles("./Plugins/", "*.dll");
            foreach (var s in files)
                Load(Path.Combine(Environment.CurrentDirectory, s));
        }

        protected override void OnLoad()
        {
          
            var tex = new Texture2D("Content/16px.png");

            var font = new TextureFont("Content/good.ttf");
            font.SaveTexture("Content/good.png");

            var go = new GameObject();
            var go2 = new GameObject();

            sceneProcess.AddGameObject(go);
            sceneProcess.AddGameObject(go2);

            var tr = go.AddComponent<TextRenderer>();
            tr.SetFont(font);
            tr.SetText("О-оптимизация");

            var t2dr = go2.AddComponent<TextureRenderer>();
            t2dr.Texture = tex;
            go.transform.Scale = new Vector3(100, 100, 100);
            go2.transform.Scale = new Vector3(100, 100, 100);
           // go2.visible = false;

            plugins.ForEach(a => { a.Init(); });

            base.OnLoad();




        }



        protected override void OnUpdate(float deltaTime)
        {
            

         

            if (Input.GetKeyDown(OpenTK.Input.Key.F1))
            {
                Console.WriteLine("Min: " + min);
                Console.WriteLine("Av: " + average);
                Console.WriteLine("Max: " + max);
            }

            base.OnUpdate(deltaTime);
        }

        protected override void OnRender(float deltaTime)
        {
            base.OnRender(deltaTime);

            plugins.ForEach(a => { a.Render(); });

            Title = $"Flatiny engine 1.0.0: (Vsync: {VSync}) FPS: {1f / deltaTime:0}";

            time += deltaTime;
            ticks++;
            average = time / ticks;

            max = Math.Max(max, deltaTime);
            min = Math.Min(min, deltaTime);
        }


        protected override void OnUnload()
        {
            base.OnUnload();
        }

    }
}
