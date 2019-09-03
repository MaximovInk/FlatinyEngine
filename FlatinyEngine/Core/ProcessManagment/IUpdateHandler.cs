namespace MaximovInk.FlatinyEngine.Core.ProcessManagment
{
    public interface IUpdateHandler : IHandler
    {
        void Update(float deltaTime);
    }
}
