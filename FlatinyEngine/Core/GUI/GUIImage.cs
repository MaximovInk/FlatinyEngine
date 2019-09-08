﻿using MaximovInk.FlatinyEngine.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;

namespace MaximovInk.FlatinyEngine.Core.GUI
{
    public class GUIImage : Graphics
    {
        public Sprite Sprite {
            get {
                return _sprite;
            }

            set {
                _sprite = value;
                mesh.vertices[0].texCoord = _sprite.Uv.Xy;
                mesh.vertices[1].texCoord = _sprite.Uv.Zy;
                mesh.vertices[2].texCoord = _sprite.Uv.Zw;
                mesh.vertices[3].texCoord = _sprite.Uv.Xw;
                mesh.ApplyData();
            }
        }
        public override Color Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
                mesh.vertices[0].Color = Color;
                mesh.vertices[1].Color = Color;
                mesh.vertices[2].Color = Color;
                mesh.vertices[3].Color = Color;
                mesh.ApplyData();
            }
        }

        private Sprite _sprite;
        private Color _color;

        public GUIImage(GUICanvas canvas) : base(canvas)
        {
            
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            mesh = Mesh.Quad;
        }

        public override void OnRender()
        {
            if (Sprite == null)
                return;

            Sprite.Texture.Bind();

            base.OnRender();

            Sprite.Texture.Unbind();
        }
    }
}
