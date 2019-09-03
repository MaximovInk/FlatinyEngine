using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public sealed class GUICanvas 
    {
        public enum ScaleType
        {
            ByPixels,
            ByPercents
        }

        public ScaleType ScaleBy = ScaleType.ByPercents;

        private List<GUIRect> childrens = new List<GUIRect>();

        public Matrix4 Ortho =>
            Matrix4.CreateTranslation(-50 * UnitX, 50 * UnitY, 0) *
            Matrix4.CreateOrthographic(GUILayer.Width, GUILayer.Height, 0, 1000);

        public float UnitX => ScaleBy == ScaleType.ByPercents ? GUILayer.Width/100.0f : 1.0f;
        public float UnitY => ScaleBy == ScaleType.ByPercents ? GUILayer.Height/100.0f : 1.0f;

        public GUIRect Dragged;

        public T AddRect<T>()where T : GUIRect
        {
            T instance = (T)Activator.CreateInstance(typeof(T),this);
            childrens.Add(instance);
            return instance;
        }

        public void RemoveRect(GUIRect rectangle)
        {
            childrens.Remove(rectangle);
            rectangle.OnDestroy();
        }

        

        public GUICanvas()
        {
            GUILayer.RegisterGUI(this);
        }

        ~GUICanvas()
        {
            GUILayer.RemoveGUI(this);
        }

        public void Update()
        {

        }

        public void Render()
        {
            for (int i = 0; i < childrens.Count; i++)
            {
                if(childrens[i].Enabled)
                    childrens[i].OnRender();
            }
        }
    }
}
