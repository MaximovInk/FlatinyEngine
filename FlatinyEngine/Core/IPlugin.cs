using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public interface IPlugin
    {
        string name { get; }
        string version { get; }

        void Init();

        void Update();

        bool Render();
    }
}
