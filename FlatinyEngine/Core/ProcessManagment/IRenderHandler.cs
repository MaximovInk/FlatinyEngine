namespace MaximovInk.FlatinyEngine.Core.ProcessManagment
{
    public interface IRenderHandler : IHandler
    {
        void Render(float deltaTime);
    }
}
