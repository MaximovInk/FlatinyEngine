using System.Collections.Generic;
using System.Linq;

namespace MaximovInk.FlatinyEngine.Core
{
    public class HierarchyObject<T> where T : HierarchyObject<T>
    {
        public T Parent { get; private set; }
        protected List<T> childrens = new List<T>();

        public virtual void AddChildren(T newChild)
        {
            if (newChild == this || newChild.Parent == this)
                return;
            if (newChild.Parent != null)
                newChild.UnAttachParent();

            newChild.Parent = (T)this;
            childrens.Add(newChild);
        }

        public void RemoveChildren(T child)
        {
            childrens.Remove(child);   
        }

        public virtual void AttachParent(T newParent)
        {
            if (newParent.childrens.Contains(this) || newParent == this)
                return;
            if (Parent != null)
                UnAttachParent();

            if(newParent != null)
            newParent.AddChildren((T)this);
        }

        private void UnAttachParent()
        {
            Parent.RemoveChildren((T)this);
            Parent = null;
        }
    }
}
