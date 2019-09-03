using OpenTK.Graphics.OpenGL;
using System;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public class Shader : IDisposable
    {
        public int Handle { get; private set; }
        public ShaderType Type { get; private set; }

        public Shader(ShaderType type, string code)
        {
            Type = type;
            Handle = GL.CreateShader(type);
            GL.ShaderSource(Handle, code);
            GL.CompileShader(Handle);
        }

        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }
    }
}
