using MaximovInk.FlatinyEngine.Core.Compnents;
using System;
using System.Collections.Generic;

namespace MaximovInk.FlatinyEngine.Core
{
    public sealed class GameObject : HierarchyObject<GameObject>, ICloneable
    {
        public bool enabled = true;
        public bool visible = true;

        public Transform transform;

        private List<IComponent> components = new List<IComponent>();
        
        public GameObject()
        {
            transform = new Transform();
            components.Add(transform);
            transform.gameObject = this;
        }

        public IComponent AddComponent(IComponent component)
        {
            if (component.Equals(typeof(Transform)))
                throw new Exception("Can't add to gameObject Transform component...");
            if (component.gameObject != null || components.Contains(component))
                throw new Exception("Why are you doing this???");

            component.gameObject = this;
            component.enabled = true;
            components.Add(component);
            if (component is IStart)
                (component as IStart).Start();
            return component;
        }
        /*
        public T AddComponent<T>() where T : IComponent
        {
            var component = (T)Activator.CreateInstance(typeof(T));
            if (component.Equals(typeof(Transform)))
                throw new Exception("Can't add to gameObject Transform component...");

            component.gameObject = this;
            Logger.Log(component.gameObject == null);
            components.Add(component);
            if(component is IStart)
                (component as IStart).Start();
            return component;
        }*/

        public void RemoveComponent(IComponent component)
        {
            (component as IEnd)?.End();
            components.Remove(component);
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                (components[i] as IStart)?.Start();
            }
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].enabled)
                    (components[i] as IUpdate)?.Update(deltaTime);
            }

            for (int i = 0; i < childrens.Count; i++)
            {
                if (childrens[i].enabled)
                    childrens[i].Update(deltaTime);
            }
        }

        public void Render(float deltaTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].enabled)
                    (components[i] as IRender)?.Render(deltaTime);
            }

            for (int i = 0; i < childrens.Count; i++)
            {
                if (childrens[i].visible)
                    childrens[i].Render(deltaTime);
            }
        }

        public void End()
        {
            for (int i = 0; i < components.Count; i++)
            {
                (components[i] as IEnd)?.End();
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
