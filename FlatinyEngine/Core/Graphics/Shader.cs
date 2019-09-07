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
            int status_code;
            string info;
            Type = type;
            Handle = GL.CreateShader(type);
            GL.ShaderSource(Handle, code);
            GL.CompileShader(Handle);
            GL.GetShaderInfoLog(Handle, out info);
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out status_code);
            if (status_code != 1)
                throw new ApplicationException(info);
        }

        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }
    }
}
