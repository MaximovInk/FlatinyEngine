using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using MaximovInk.FlatinyEngine.Core.Graphics;
using MaximovInk.FlatinyEngine.Core.ProcessManagment;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public abstract class GUIRect
    {
        public bool Enabled = true;

        public GUIRect Parent { get; private set; }
        protected List<GUIRect> childrens = new List<GUIRect>();

        public virtual void AddChildren(GUIRect newChild)
        {
            if (newChild == this || newChild.Parent == this)
                return;
            if (newChild.Parent != null)
                newChild.UnAttachParent();

            newChild.Parent = this;
            childrens.Add(newChild);
        }

        public void RemoveChildren(GUIRect child)
        {
            childrens.Remove(child);
        }

        public GUIRect GetParent()
        {
            return Parent;
        }

        public virtual void SetParent(GUIRect newParent)
        {
            if (newParent.childrens.Contains(this) || newParent == this)
                return;
            if (Parent != null)
                UnAttachParent();
            
            if(newParent != null)
            newParent.AddChildren(this);
        }

        private void UnAttachParent()
        {
            Parent.RemoveChildren(this);
            Parent = null;
        }

        public Rectangle Rect = new Rectangle(0,0,1,1);


        private float UnitX => Parent != null ? Parent.Rect.Width/100.0f*Canvas.UnitX : Canvas.UnitX;
        private float UnitY => Parent != null ? Parent.Rect.Height / 100.0f * Canvas.UnitY : Canvas.UnitY;

        protected Mesh mesh;

        public GUICanvas Canvas => Parent != null ? Parent.Canvas : canvas;

        private GUICanvas canvas;

        public GUIRect(GUICanvas canvas)
        {
            this.canvas = canvas;
            OnCreate();
        }

        protected Matrix4 GetMatrix() =>
            
            Matrix4.CreateTranslation(Rect.X * UnitX, Rect.Y * -UnitY, 0)/* *
            Matrix4.CreateRotationZ(Rotation) */
            ;

        protected Matrix4 GetGlobalMatrix()
        {
            if (Parent != null)
                return GetMatrix() * Parent.GetGlobalMatrix();
            return GetMatrix();
        }
        
        public virtual void OnRender()
        {
            if (mesh != null && mesh.indices != null)
            {
                mesh.Bind();
                GL.MatrixMode(MatrixMode.Projection);
                GL.PushMatrix();
                
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.EnableClientState(ArrayCap.ColorArray);
                GL.EnableClientState(ArrayCap.IndexArray);

                GL.VertexPointer(2, VertexPointerType.Float, ColoredVertex.SizeInBytes, 0);
                GL.TexCoordPointer(2, TexCoordPointerType.Float, ColoredVertex.SizeInBytes, Vector2.SizeInBytes);
                GL.ColorPointer(4, ColorPointerType.Float, ColoredVertex.SizeInBytes, Vector2.SizeInBytes * 2);

                GL.LoadIdentity();
                var matrix = Matrix4.CreateScale(Rect.Width * UnitX, Rect.Height * UnitY, 1) * GetGlobalMatrix()*Canvas.Ortho;
                GL.LoadMatrix(ref matrix);
                GL.Scale(1, -1, 1);

                GL.DrawElements(PrimitiveType.Triangles, mesh.indices.Length, DrawElementsType.UnsignedInt, 0);

                GL.DisableClientState(ArrayCap.VertexArray);
                GL.DisableClientState(ArrayCap.TextureCoordArray);
                GL.DisableClientState(ArrayCap.IndexArray);
                GL.DisableClientState(ArrayCap.ColorArray);
                
                GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                mesh.Unbind();

            }

            for (int i = 0; i < childrens.Count; i++)
            {
                if(childrens[i].Enabled)
                    childrens[i].OnRender();
            }
        }

        protected virtual void OnCreate() { }
        public virtual void OnDestroy() { mesh.Dispose(); }

        public GUIRect MouseIntersection()
        {
            var x = Input.MouseX;
            var y = Input.MouseY;

            GUIRect intersected = null;
            var trns = GetGlobalMatrix().ExtractTranslation();

            intersected = new Rectangle((int)trns.X, (int)trns.Y, (int)(Rect.Width * UnitX),(int)(Rect.Height * UnitY)).IntersectsWith(new Rectangle(x, y, 1, 1)) ? this : null ;
            for (int i = 0; i < childrens.Count; i++)
            {
                intersected = childrens[i].MouseIntersection() ?? intersected;
            }

            return intersected;
        }
        public virtual void OnDragStart()
        {
        }
        public virtual void OnDrag()
        {
        }
        public virtual void OnDragEnd()
        {
        }
        public virtual void OnMouseEnter()
        {
        }
        public virtual void OmMouseOver()
        {
        }
        public virtual void OnMouseExit()
        {

        }
    }
}
