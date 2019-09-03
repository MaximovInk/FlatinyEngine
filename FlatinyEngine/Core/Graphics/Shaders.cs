using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public static class Shaders
    {
        public const string TEXTURE_FRAGMENT =@"
version 330

out vec4 outColor;

in vec4 inColor;
in vec2 texCoord;

uniform sampler2D texture;

";

        public const string texturedFrag = @"
#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
}
";
public const string texturedVert = @"
#version 330 core

layout(location = 0) in vec3 aPos;

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform mat4 transform;

void main(void)
{
    gl_Position = transform * vec4(aPos, 1.0f);
      TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}
";


    }
}
