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

        public Drawer(int pWidth, int pHeight) : base(pWidth, pHeight)
        {
            font = new Font("Arial", 25, FontStyle.Regular);
            brush = new SolidBrush(Color.LimeGreen);
            pos = new PointF(0, 0);
        }
        void Update()
        {
        }
        public void DrawText(string pMessage, PointF pPos)
        {
            //graphics.Clear(Color.Empty);
            graphics.DrawString(pMessage, font, brush, pPos);
        }
    }
}
