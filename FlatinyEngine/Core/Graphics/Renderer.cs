using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public abstract class Renderer : IRender
    {
        protected Mesh mesh;

        public virtual Effect Effect { get; set; } = Effect.Default;

        protected abstract Matrix4 GetMatrix();

        public virtual void Render(float deltaTime)
        {
            if (mesh == null || Effect == null)
                return;

            Effect.Use();

            mesh.Bind();

            var matrix = GetMatrix();
            var projection = Screen.WorldProjectionMatrix;

            Effect.SetUnfiormMatrix4("ObjectMatrix", false, ref matrix);

            Effect.SetUnfiormMatrix4("CameraProjection", false, ref projection);

            GL.DrawArrays(PrimitiveType.Triangles, 0, mesh.vertices.Length);

            mesh.Unbind();

            Effect.Unbind();
        }
    }
}
