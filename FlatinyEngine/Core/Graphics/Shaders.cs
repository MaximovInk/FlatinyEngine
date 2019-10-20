using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public static class Shaders
    {

        public const string FRAGMENT = @"
#version 430
uniform sampler2D tex;
in vec2 fragTexCoord;
out vec4 finalColor;

void main() {
    finalColor = texture(tex, fragTexCoord);
}
";

        public const string VERTEX = @"
#version 430

in vec2 vert;
in vec2 vertTexCoord;

out vec2 fragTexCoord;

uniform mat4 ObjectMatrix;
uniform mat4 CameraProjection;


void main() 
    {
    fragTexCoord = vertTexCoord;
    
    gl_Position = CameraProjection*ObjectMatrix*vec4(vert,0,1);
    }
";

    }
}
