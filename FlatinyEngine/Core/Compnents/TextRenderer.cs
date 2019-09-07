﻿using MaximovInk.FlatinyEngine.Core.Graphics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public sealed class TextRenderer : Renderer
    {
        private TextureFont font;
        private string text;

        public TextRenderer()
        {
            mesh = Mesh.Empty;
        }

        public void SetFont(TextureFont font)
        {
            this.font = font;
        }

        public TextureFont GetFont() => font;

        public void SetText(string newText)
        {
            text = newText;

            mesh.vertices = new ColoredVertex[4 * text.Length];
            mesh.indices = new uint[6 * text.Length];

            var totalX = 0.0f;

            for (uint i = 0; i < text.Length; i++)
            {
                var ch = font.GetChar(text[(int)i]);

                /* mesh.vertices[4 * i] = new ColoredVertex(new Vector2(i, 0), new Vector2(uv.X, uv.Y), Color.White);
                 mesh.vertices[4 * i + 1] = new ColoredVertex(new Vector2(i+1, 0), new Vector2(uv.X + uv.Z, uv.Y), Color.White);
                 mesh.vertices[4 * i + 2] = new ColoredVertex(new Vector2(i+1, 1), new Vector2(uv.X + uv.Z, uv.Y + uv.W), Color.White);
                 mesh.vertices[4 * i + 3] = new ColoredVertex(new Vector2(i,1), new Vector2(uv.X, uv.Y + uv.W), Color.White);*/

                mesh.vertices[4 * i] = new ColoredVertex(new Vector2(totalX, 0), new Vector2(ch.Uv.X, ch.Uv.Y), Color.White);
                mesh.vertices[4 * i + 1] = new ColoredVertex(new Vector2(totalX + ch.PSize.X, 0), new Vector2(ch.Uv.X + ch.Uv.Z, ch.Uv.Y), Color.White);
                mesh.vertices[4 * i + 2] = new ColoredVertex(new Vector2(totalX + ch.PSize.X, ch.PSize.Y), new Vector2(ch.Uv.X + ch.Uv.Z, ch.Uv.Y + ch.Uv.W), Color.White);
                mesh.vertices[4 * i + 3] = new ColoredVertex(new Vector2(totalX, ch.PSize.Y), new Vector2(ch.Uv.X, ch.Uv.Y + ch.Uv.W), Color.White);

                mesh.indices[6 * i] = 4 * i;
                mesh.indices[6 * i + 1] = 4 * i + 1;
                mesh.indices[6 * i + 2] = 4 * i + 2;
                mesh.indices[6 * i + 3] = 4 * i;
                mesh.indices[6 * i + 4] = 4 * i + 2;
                mesh.indices[6 * i + 5] = 4 * i + 3;

                totalX += ch.PSize.X;
            }

            mesh.ApplyData();
            
        }

        public string GetText()
        {
            return text;
        }

        public override void OnRender(float deltaTime)
        {
            if (text == string.Empty || font == null)
                return;

            font.Bind();

            base.OnRender(deltaTime);

            font.Unbind();
        }
    }

}