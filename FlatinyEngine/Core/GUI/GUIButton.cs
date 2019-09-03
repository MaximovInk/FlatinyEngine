using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public sealed class GUIButton
    {
        public bool Interactable { get; set; }

        public bool IsPressed { get; private set; }

        public void Up()
        {
            IsPressed = false;
        }

        public void Down()
        {
            IsPressed = true;
        }
        
    }
}
