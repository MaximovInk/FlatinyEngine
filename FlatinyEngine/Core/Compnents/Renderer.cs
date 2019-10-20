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
            if (mesh == null /*|| mesh.indices == null*/)
                return;

            mesh.Bind();

            GL.PushMatrix();

            // GL.EnableClientState(ArrayCap.VertexArray);
            //GL.EnableClientState(ArrayCap.TextureCoordArray);
            //GL.EnableClientState(ArrayCap.ColorArray);
            //GL.EnableClientState(ArrayCap.IndexArray);

            //GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, 0);
            //GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, Vector2.SizeInBytes);
            //GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, Vector2.SizeInBytes * 2);



            //GL.DrawElements(PrimitiveType.Triangles, mesh.indices.Length, DrawElementsType.UnsignedInt, 0);

            var matrix = GetMatrix();
            var projection = Screen.ProjectionMatrix;

            GL.ProgramUniformMatrix4(1, GL.GetUniformLocation(1, "m"), false, ref matrix);
            GL.ProgramUniformMatrix4(1, GL.GetUniformLocation(1, "p"), false, ref projection);

            GL.DrawArrays(PrimitiveType.Triangles, 0, mesh.vertices.Length);

            /*GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.IndexArray);
            GL.DisableClientState(ArrayCap.ColorArray);*/
            //GL.ProgramUniformMatrix4(1, GL.GetUniformLocation(1, "projection_view"), true, ref projection);

            //GL.ProgramUniformMatrix4(1, GL.GetUniformLocation(1, "mvp"), true, ref projection);

            GL.PopMatrix();

            mesh.Unbind();
        }
    }
}
