using System;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public abstract class Component : ICloneable
    {
        public bool enabled
        {
            get {
                return _enabled;
            }
            set
            {
                _enabled = value;

                if (enabled)
                    OnEnable();
                else
                    OnDisable();
            }
        }
        private bool _enabled = true;

        public GameObject gameObject;
        public string tag;

        public virtual void Start() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void End() { }
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnRender(float deltaTime) { }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}