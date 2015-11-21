using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public class Drawer : Canvas
    {
        Font font;
        Brush brush;
        PointF pos;

        public Drawer() : base(1024, 60)
        {
            font = new Font("Arial", 25, FontStyle.Regular);
            brush = new SolidBrush(Color.LimeGreen);
            pos = new PointF(0, 0);
        }
        void Update()
        {
            graphics.Clear(Color.Empty);
        }
        public void DrawText(string pMessage, PointF pPos)
        {
            graphics.DrawString(pMessage, font, brush, pPos);
        }
        public void DrawSprite(Sprite pSprite, PointF pPos)
        {
            DrawSprite(pSprite);
            pSprite.SetXY(pPos.X, pPos.Y);
        }
    }
}
