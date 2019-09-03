namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public static class StandartShaders
    {
        public const string colored_vert =
@"
#version 450 core

layout (location = 0) in vec4 position;
layout(location = 1) in vec4 color;
out vec4 vs_color;

void main(void)
{
 gl_Position = position;
 vs_color = color;
}
";
        public const string colored_frag =
@"
#version 450 core
in vec4 vs_color;
out vec4 color;

void main(void)
{
 color = vs_color;
}
";


        public const string textured_frag =
@"
#version 450 core
in vec2 vs_textureCoordinate;
uniform sampler2D textureObject;
out vec4 color;

void main(void)
{
 color = texelFetch(textureObject, ivec2(vs_textureCoordinate.x, vs_textureCoordinate.y), 0);
}
";

        public const string vert =
@"
#version 330 core

layout(location = 0) in vec3 vertexPos;

void main() {
  gl_Position.xyz = vertexPos;
  gl_Position.w = 1.0;
}
";

        public const string frag =
@"
#version 330 core

out vec3 color;

void main() {
  color = vec3(0,0,1);
}
";


    }
}
