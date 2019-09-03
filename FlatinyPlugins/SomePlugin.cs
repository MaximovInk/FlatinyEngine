using MaximovInk.FlatinyEngine.Core;
using System;

namespace FlatinyPlugins
{
    public class SomePlugin : IPlugin
    {
        public string name { get => "Some plugin"; }
        public string version { get => "1.0.3"; }

        public void Init()
        {
            Console.WriteLine("Plugin " + name + "init. Version : " + version);
        }

        public bool Render()
        {
            return false;
        }

        public void Update()
        {
            Console.WriteLine("Update plugin");
        }
    }
}
