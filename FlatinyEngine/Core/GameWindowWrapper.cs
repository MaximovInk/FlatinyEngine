using OpenTK;
using OpenTK.Graphics;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GameWindowWrapper : GameWindow
    {
        public void SwapChain() => SwapBuffers();

        public GameWindowWrapper(int w ,int h , GraphicsMode mode , string title) : base ( w,h, mode, title)
        {
            
        }
    }
}
