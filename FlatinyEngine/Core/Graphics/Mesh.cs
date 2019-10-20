using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public class Mesh : IDisposable
    {
        private int VBO = 0;
        private int VAO = 0;

        public Vertex[] vertices;

        private int lastVertsLenght = 0;

        public Mesh()
        {
            GenBuffers();
        }

        public void FillVertices(Vertex vertex)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = vertex;
            }
        }

        public void FillColor(Color4 Color)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].color = Color.ToVector();
            }
        }

        public void ApplyData()
        {
            BindBuffers();
            if (lastVertsLenght == vertices.Length)
            {
                UpdateBufferSubData();
            }
            else
            {
                UpdateBufferData();
            }
        }

        public void Bind()
        {
            BindBuffers();
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, true, Vertex.SizeInBytes, 0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, true, Vertex.SizeInBytes, Vector2.SizeInBytes);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, true, Vertex.SizeInBytes, Vector2.SizeInBytes * 2);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
        }

        private void GenBuffers()
        {
            VBO = GL.GenBuffer();
            VAO = GL.GenVertexArray();
        }

        private void BindBuffers()
        {
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        }

        private void UpdateBufferSubData()
        {
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, (IntPtr)(Vertex.SizeInBytes * vertices.Length), vertices);
        }

        private void UpdateBufferData()
        {
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.SizeInBytes * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
        }

        public void Dispose()
        {
            Unbind();
            GL.DeleteBuffer(VBO);
            GL.DeleteVertexArray(VAO);
        }

        public static Mesh Quad
        {
            get
            {
                var mesh = new Mesh
                {
                    vertices = new Vertex[] {
                        new Vertex(new Vector2(0,0), new Vector2(0,0), System.Drawing.Color.White),
                        new Vertex(new Vector2(1, 0), new Vector2(1, 0), System.Drawing.Color.White),
                        new Vertex(new Vector2(1,1), new Vector2(1,1),  System.Drawing.Color.White),
                        new Vertex(new Vector2(0,0), new Vector2(0,0), System.Drawing.Color.White),
                        new Vertex(new Vector2(1,1), new Vector2(1,1),  System.Drawing.Color.White),
                        new Vertex(new Vector2(0,1), new Vector2(0,1),  System.Drawing.Color.White)
                    }
                };

                mesh.ApplyData();

                return mesh;
            }
        }

        public static Mesh Grid(int x, int y)
        {
            Mesh mesh = Mesh.Empty;
            mesh.vertices = new Vertex[x * y * 6];
            int offset = 0;

            for (uint ix = 0; ix < x; ix++)
            {
                for (uint iy = 0; iy < y; iy++)
                {
                    var i0 = ix * 6 + iy * x * 6;
                    var i1 = ix * 6 + iy * x * 6 + 1;
                    var i2 = ix * 6 + iy * x * 6 + 2;
                    var i3 = ix * 6 + iy * x * 6 + 3;
                    var i4 = ix * 6 + iy * x * 6 + 4;
                    var i5 = ix * 6 + iy * x * 6 + 5;

                    mesh.vertices[i0] = new Vertex(new Vector2(ix, iy), new Vector2(0, 0), System.Drawing.Color.White);
                    mesh.vertices[i1] = new Vertex(new Vector2(ix + 1, iy), new Vector2(1, 0), System.Drawing.Color.White);
                    mesh.vertices[i2] = new Vertex(new Vector2(ix + 1, iy + 1), new Vector2(1, 1), System.Drawing.Color.White);
                    mesh.vertices[i3] = new Vertex(new Vector2(ix, iy), new Vector2(0, 0), System.Drawing.Color.White);
                    mesh.vertices[i4] = new Vertex(new Vector2(ix+1, iy+1), new Vector2(1, 1), System.Drawing.Color.White);
                    mesh.vertices[i5] = new Vertex(new Vector2(ix, iy+1), new Vector2(0, 1), System.Drawing.Color.White);
                    offset += x * 6;
                }
            }

            mesh.ApplyData();

            return mesh;

        }

        public static Mesh Empty { get { return new Mesh(); } }
    }
}

    
