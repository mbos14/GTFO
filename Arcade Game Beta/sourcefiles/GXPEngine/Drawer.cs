﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Drawer : Canvas
    {
        Font font;
        Brush brush;
        PointF pos;

        private bool _keepDrawing = true;
        public Drawer(int pWidth, int pHeight) : base(pWidth, pHeight)
        {
            font = new Font("Minecraft", 25, FontStyle.Regular);
            brush = new SolidBrush(Color.LimeGreen);
            pos = new PointF(0, 0);
        }
        void Update()
        {
            if (!_keepDrawing) return;
            graphics.Clear(Color.Empty);
        }
        protected override Collider createCollider()
        {
            return null;
        }
        public override void Destroy()
        {
            _keepDrawing = false;
            base.Destroy();
        }
        public void DrawText(string pMessage, PointF pPos)
        {
            if (!_keepDrawing) return;
            graphics.DrawString(pMessage, font, brush, pPos);
        }
    }
}