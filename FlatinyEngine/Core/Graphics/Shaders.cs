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

uniform mat4 m;
uniform mat4 p;


void main() 
    {
    fragTexCoord = vertTexCoord;
    
    gl_Position = p*m*vec4(vert,0,1);
    }
";




        /* public const string TEXTURE_FRAGMENT = @"
 #version 430

 out vec4 outputColor;

 in vec2 texCoord;

 uniform sampler2D texture0;

 void main()
 {
     outputColor = texture(texture0, texCoord);
 }
 ";

 public const string TEXTURE_VERTEX = @"
 #version 430 core

 layout(location = 1) in vec2 inPos;

 layout(location = 0) in vec2 inTexCoord;

 out vec2 texCoord;

 uniform mat4 transform;

 void main(void)
 {
     gl_Position = transform * vec4(inPos , 0.0f, 1.0f);
     texCoord = vec2(inTexCoord.x, inTexCoord.y);
 }
 ";*/


    }
}
