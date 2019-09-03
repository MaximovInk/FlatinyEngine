using OpenTK.Graphics.OpenGL;
using System;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public class Effect : IDisposable
    {
        private int Handle;

        public Effect(string vertex_shader ,string fragment_shader)
        {
            Handle = GL.CreateProgram();
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertex_shader);
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragment_shader);
            GL.CompileShader(fragmentShader);

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public int GetAttributeLocation(string name)
        {
            return GL.GetAttribLocation(Handle, name);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(Handle, name);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void Dispose()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
