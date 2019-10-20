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

            var vert = new Shader(ShaderType.VertexShader, vertex_shader);
            var frag = new Shader(ShaderType.FragmentShader, fragment_shader);

            GL.AttachShader(Handle, vert.Handle);
            GL.AttachShader(Handle, frag.Handle);
            GL.LinkProgram(Handle);
            GL.GetProgramInfoLog(Handle,out string info);
            Logger.Log(info);

            GL.DetachShader(Handle, vert.Handle);
            GL.DetachShader(Handle, frag.Handle);
            vert.Dispose();
            frag.Dispose();
        }

        public void SetUniform(string name, double num)
        {
            GL.Uniform1(GetUniformLocation(name), num);
        }

        public void SetUniform(string name, int num)
        {
            GL.Uniform1(GetUniformLocation(name), num);
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

        public void Unuse()
        {
            GL.UseProgram(0);
        }

        public void Dispose()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
