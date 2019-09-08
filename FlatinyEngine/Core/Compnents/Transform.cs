using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public class Transform : IComponent
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;
        public Vector3 Scale { get; set; } = Vector3.One;
        public Vector2 Origin { get; set; } = Vector2.Zero;

        public bool enabled { get; set; }
        public GameObject gameObject { get; set; }
        public string tag { get; set; }

        public Matrix4 GetMatrix()
        {
            return
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateTranslation(Position) *
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z) *
                Matrix4.CreateTranslation(Origin.X, Origin.Y, 0)
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
