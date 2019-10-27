using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core
{
    public class GUIRect : ScreenRenderer
    {
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;

        public virtual float ZDepth { get; set; } = 0;
        public virtual Vector2 Position { get; private set; }
        public virtual Vector2 LocalPosition { get; private set; }
        public virtual Vector2 Size { get; private set; }
        public virtual Vector2 Pivot { get; set; }
            = new Vector2(0f, 0f);

        public GUIRect Parent { get; private set; }
        private List<GUIRect> Childrens = new List<GUIRect>();

        public int ChildCount { get { return Childrens.Count; } }

        private GUIConstraint XConstraint = new GUIPixelConstraint(0);
        private GUIConstraint YConstraint = new GUIPixelConstraint(0);
        private GUIConstraint WidthConstraint = new GUIPixelConstraint(1);
        private GUIConstraint HeightConstraint = new GUIPixelConstraint(1);

        public GUIRect TopGUIElement => Parent != null ? Parent.TopGUIElement : this;

        public bool RaycastTarget = true;

        public virtual GUIRect GetIntersection()
        {
            GUIRect intersected =
                RaycastTarget && new RectangleF(Position.X - Pivot.X * Size.X, Position.Y - Pivot.Y * Size.Y, Size.X, Size.Y).
                IntersectsWith(new RectangleF(Input.MouseX, Input.MouseY, 1f, 1f)) ? this : null;

            for (int i = 0; i < Childrens.Count; i++)
            {
                intersected = Childrens[i].GetIntersection() ?? intersected;
            }

            return intersected;
        }

        public void UpdateLayout()
        {
            CalculateConstraints();

            for (int i = 0; i < Childrens.Count; i++)
            {
                Childrens[i].UpdateLayout();
            }
        }

        private void UpdateThisGUIBranch()
        {
            TopGUIElement.UpdateLayout();
        }

        private void CalculateConstraints()
        {
            LocalPosition = new Vector2(XConstraint.GetValue(),YConstraint.GetValue());
            Size = new Vector2(WidthConstraint.GetValue(), HeightConstraint.GetValue());

            Position = Parent != null ? LocalPosition + Parent.Position : LocalPosition;

            CalculatedMatrix = Matrix4.CreateScale(Size.X , Size.Y, 1) *
              Matrix4.CreateTranslation(Position.X, Position.Y, ZDepth) *
              Matrix4.CreateTranslation(-Pivot.X * Size.X, -Pivot.Y * Size.Y, 0);
        }

        public void SetXConstraint(GUIConstraint constraint)
        {
            if (constraint == null || constraint.rect != null)
                return;

            constraint.rect = this;
            constraint.axis = GUIConstraint.Axis.X;
            XConstraint = constraint;

            UpdateThisGUIBranch();
        }

        public void SetYConstraint(GUIConstraint constraint)
        {
            if (constraint == null || constraint.rect != null)
                return;

            constraint.rect = this;
            constraint.axis = GUIConstraint.Axis.Y;
            YConstraint = constraint;

            UpdateThisGUIBranch();
        }

        public void SetWidthConstraint(GUIConstraint constraint)
        {
            if (constraint == null || constraint.rect != null)
                return;

            constraint.rect = this;
            constraint.axis = GUIConstraint.Axis.X;
            WidthConstraint = constraint;

            UpdateThisGUIBranch();
        }

        public void SetHeightConstraint(GUIConstraint constraint)
        {
            if (constraint == null || constraint.rect != null)
                return;

            constraint.rect = this;
            constraint.axis = GUIConstraint.Axis.Y;
            HeightConstraint = constraint;

            UpdateThisGUIBranch();
        }

        protected override Matrix4 GetMatrix()
        => CalculatedMatrix;

        private Matrix4 CalculatedMatrix = Matrix4.Identity;

        public void AddChild(GUIRect rect)
        {
            if (rect.Parent == null && rect != this)
            {
                rect.Parent = this;
                Childrens.Add(rect);

                UpdateThisGUIBranch();
            }
        }

        public void RemoveChild(GUIRect rect)
        {
            if (rect.Parent != this)
                return;

            rect.Parent = null;
            Childrens.Remove(rect);

            UpdateThisGUIBranch();
        }

        public void RemoveAt(int index)
        {
            Childrens[index].Parent = null;
            Childrens.RemoveAt(index);
        }

        public GUIRect GetChild(int index)
        {
            return Childrens[index];
        }

        public virtual void Update(float deltaTime)
        {
            if (!Enabled)
                return;

            for (int i = 0; i < Childrens.Count; i++)
            {
                if (Childrens != null)
                    Childrens[i].Update(deltaTime);
            }
        }

        public override void Render(float deltaTime)
        {
            if (!Visible)
                return;

            base.Render(deltaTime);

            for (int i = 0; i < Childrens.Count; i++)
            {
                if (Childrens != null)
                    Childrens[i].Render(deltaTime);
            }
        }

        public virtual void OnMouseEnter()
        {

        }

        public virtual void OnMouseOver()
        {

        }

        public virtual void OnMouseExit()
        {

        }

        public virtual void OnMouseDown()
        {

        }

        public virtual void OnMouseUp()
        {

        }

        public virtual void OnMousePress()
        {

        }
    }
}
