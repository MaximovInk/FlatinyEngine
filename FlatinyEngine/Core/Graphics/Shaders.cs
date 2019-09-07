using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public static class Shaders
    {

        public const string vert= @"#version 150
in vec3 vert;
in vec2 vertTexCoord;
out vec2 fragTexCoord;

void main() {
    // Pass the tex coord straight through to the fragment shader
    fragTexCoord = vertTexCoord;
    
    gl_Pos";

        public const string FRAG_XR = @"#version 330 core
out vec4 color;

uniform vec4 ourColor; // Мы устанавливаем значение этой переменной в коде OpenGL.

void main()
{
    color = ourColor;
}  ";

        public const string GRAYSCALE_TEXTURE_FRAGMENT = @"
#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    vec4 col = texture(texture0, texCoord);
    float gs = (col.r+col.b+col.r)/3.0f;
    outputColor = vec4(gs,gs,gs,gs);
}
";
           
        public const string TEXTURE_FRAGMENT = @"
#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
}
";

public const string TEXTURE_VERTEX = @"
#version 330 core

layout(location = 0) in vec3 aPos;

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform mat4 transform;

void main(void)
{
    gl_Position = transform * vec4(aPos, 1.0f);
    texCoord = vec2(aTexCoord.x, aTexCoord.y);
}
";


    }
}
