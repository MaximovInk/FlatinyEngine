namespace MaximovInk.FlatinyEngine.Core.ProcessManagment
{
    public class UpdateProcess : IUpdateHandler
    {
        public delegate void OnUpdate(float deltaTime);
        public event OnUpdate onUpdate;

        public void Update(float deltaTime)
        {
            onUpdate?.Invoke(deltaTime);
        }
    }
}
