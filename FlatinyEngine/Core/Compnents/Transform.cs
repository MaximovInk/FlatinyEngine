using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public class Transform : Component
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;
        public Vector2 Origin = Vector2.Zero;

        public Matrix4 GetMatrix()
        {
            return
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateTranslation(Position) *
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z) *
                Matrix4.CreateTranslation(Origin.X,Origin.Y,0)
                ;
        }

        public Matrix4 GetGlobalMatrix()
        {
            if (gameObject.Parent != null)
            {
                return GetMatrix() * gameObject.Parent.transform.GetGlobalMatrix();
            }
            return GetMatrix();
        }
    }
}
