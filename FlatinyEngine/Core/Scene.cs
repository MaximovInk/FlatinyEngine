using System.Collections.Generic;

namespace MaximovInk.FlatinyEngine.Core
{
    public class Scene
    {
        public List<GameObject> gameObjects = new List<GameObject>();

        public void Start()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            gameObject.Start();
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObject.End();
            gameObjects.Remove(gameObject);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if(gameObjects[i].enabled)
                    gameObjects[i].Update(deltaTime);
            }
        }

        public void Render(float deltaTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if(gameObjects[i].visible)
                    gameObjects[i].Render(deltaTime);
            }
        }

        public void End()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].End();
            }
        }
    }
}
