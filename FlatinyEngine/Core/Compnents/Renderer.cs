using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public abstract class Renderer : IRender
    {
        protected Mesh mesh;

        protected abstract Matrix4 GetMatrix();

        public virtual void Render(float deltaTime)
        {
            if (mesh == null || mesh.indices == null)
                return;

            mesh.Bind();

            GL.PushMatrix();

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.IndexArray);

            GL.VertexPointer(2, VertexPointerType.Float, ColoredVertex.SizeInBytes, 0);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, ColoredVertex.SizeInBytes, Vector2.SizeInBytes);
            GL.ColorPointer(4, ColorPointerType.Float, ColoredVertex.SizeInBytes, Vector2.SizeInBytes * 2);

            //var matrix = gameObject.transform.GetGlobalMatrix();
            var matrix = GetMatrix();
            GL.LoadMatrix(ref matrix);
            GL.DrawElements(PrimitiveType.Triangles, mesh.indices.Length, DrawElementsType.UnsignedInt, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.IndexArray);
            GL.DisableClientState(ArrayCap.ColorArray);

            GL.PopMatrix();

            mesh.Unbind();
        }
    }
}
