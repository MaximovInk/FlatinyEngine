using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public class Mesh : IDisposable
    {
        private int VBO = -1;
        private int IBO = -1;

        public uint[] indices;

        public ColoredVertex[] vertices;

        private int lastVertsLenght = 0;
        private int lastIndicesLenght = 0;

        public Mesh()
        {
            GenBuffers();
        }
        
        public void ApplyData()
        {
            BindBuffers();
            if (lastIndicesLenght == indices.Length && lastVertsLenght == vertices.Length)
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
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        private void GenBuffers()
        {
            VBO = GL.GenBuffer();
            IBO = GL.GenBuffer();
        }

        private void BindBuffers()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IBO);
        }

        private void UpdateBufferSubData()
        {
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, (IntPtr)(ColoredVertex.SizeInBytes * vertices.Length), vertices);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)0, (IntPtr)(sizeof(uint) * indices.Length), indices);
        }

        private void UpdateBufferData()
        {
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(ColoredVertex.SizeInBytes * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(uint) * indices.Length), indices, BufferUsageHint.DynamicDraw);

        }

        public void Dispose()
        {
            Unbind();
            GL.DeleteBuffer(VBO);
            GL.DeleteBuffer(IBO);
        }

        public static Mesh Quad { get {
                var mesh = new Mesh
                {
                    vertices = new ColoredVertex[4]
{
                new ColoredVertex(new Vector2(0,0), new Vector2(0,0), System.Drawing.Color.White),
                new ColoredVertex(new Vector2(1,0), new Vector2(1,0),  System.Drawing.Color.White),
                new ColoredVertex(new Vector2(1,1), new Vector2(1,1),  System.Drawing.Color.White),
                new ColoredVertex(new Vector2(0,1), new Vector2(0,1),  System.Drawing.Color.White)
},
                    indices = new uint[6]
{
            0, 1, 2,
            0, 2, 3
}
                };

                mesh.ApplyData();

                return mesh;
            } }

        public static Mesh Empty { get { return new Mesh(); } }
    }
}

    
