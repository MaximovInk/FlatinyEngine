using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximovInk.FlatinyEngine.Core.Graphics
{
    public sealed class TextLayout 
    {
        private Mesh mesh;

        private string text;

        public void Bind()
        {
            mesh.Bind();
        }

        public void Unbind()
        {
            mesh.Unbind();
        }

        public void SetText(string text)
        {

        }

        public string GetText() => text;

    }
}
