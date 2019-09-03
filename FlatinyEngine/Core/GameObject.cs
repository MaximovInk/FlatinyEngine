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

        private List<Component> components = new List<Component>();
        
        public GameObject()
        {
            transform = new Transform();
            components.Add(transform);
            transform.Start();
            transform.gameObject = this;
        }

        public T AddComponent<T>() where T : Component
        {
            var component = (T)Activator.CreateInstance(typeof(T));
            if (component.Equals(typeof(Transform)))
                return transform as T;

            components.Add(component);
            component.gameObject = this;
            component.Start();
            return component;
        }

        public void RemoveComponent(Component component)
        {
            component.End();
            components.Remove(component);
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        public void OnUpdate(float deltaTime)
        {


            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].enabled)
                    components[i].OnUpdate(deltaTime);
            }

            for (int i = 0; i < childrens.Count; i++)
            {
                if (childrens[i].enabled)
                    childrens[i].OnUpdate(deltaTime);
            }
        }

        public void OnRender(float deltaTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].enabled)
                    components[i].OnRender(deltaTime);
            }
            for (int i = 0; i < childrens.Count; i++)
            {
                if (childrens[i].visible)
                    childrens[i].OnRender(deltaTime);

            }
        }

        public void End()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].End();
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
