using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public class Effect : IDisposable
    {
        private int Handle;

        public static Effect Default = new Effect(Shaders.VERTEX,Shaders.FRAGMENT);

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

        public void SetUniform4(string name, Vector4 vector4)
        {
            GL.ProgramUniform4(Handle, GetUniformLocation(name), vector4);
        }

        public void SetUnfiormMatrix4(string name, bool transpose, ref Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4(Handle, GetUniformLocation(name), transpose, ref matrix);
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

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public void Dispose()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
