namespace MaximovInk.FlatinyEngine.Core.ProcessManagment
{
    public class RenderProcess : IRenderHandler
    {
        public delegate void OnRender(float deltaTime);
        public event OnRender onRender;

        public void Render(float deltaTime)
        {
            onRender?.Invoke(deltaTime);
        }
    }
}
