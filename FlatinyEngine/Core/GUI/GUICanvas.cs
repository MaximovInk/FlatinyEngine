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

        public GUIRect Over { get; private set; }
        private GUIRect lastOver;
        public GUIRect Dragged { get; private set; }

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

        private void UpdateInput()
        {

            for (int i = 0; i < childrens.Count; i++)
            {
                Over = childrens[i].MouseIntersection();
            }
            Over?.OmMouseOver();
            if (Over != lastOver && Over != null)
            {
                Over.OnMouseEnter();
            }
            if (Over != lastOver && lastOver != null)
            {
                lastOver.OnMouseExit();
            }

            if (Input.GetMouseButtonDown(OpenTK.Input.MouseButton.Left))
            {
                Dragged = Over;
                Dragged?.OnDragStart();
            }

            Dragged?.OnDrag();

            if (Input.GetMouseButtonUp(OpenTK.Input.MouseButton.Left))
            {
                Dragged?.OnDragEnd();
                Dragged = null;
            }

            lastOver = Over;
        }

        public void Update()
        {
            
            UpdateInput();
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
