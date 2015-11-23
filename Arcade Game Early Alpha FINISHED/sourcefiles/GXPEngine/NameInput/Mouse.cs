using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Mouse : Sprite
    {
        public Mouse() : base("Mouse2.png")
        {
            SetOrigin(width / 2, height + 5);
            SetXY(80, 360);
        }

        void Update()
        {
            MoveMouse();
            StayOnButtons();
        }

        private void MoveMouse()
        {
            int _mouseSpeed = 96;

            if (Input.GetKeyDown((int)PlayerButtons.left)){x -= _mouseSpeed; }
            if (Input.GetKeyDown((int)PlayerButtons.right)) { x += _mouseSpeed; }
            if (Input.GetKeyDown((int)PlayerButtons.up)) { y -= _mouseSpeed; }
            if (Input.GetKeyDown((int)PlayerButtons.down)) { y += _mouseSpeed; }
        }
        private void StayOnButtons()
        {
            int border = 80;
            int buttonsTop = 360;
            int buttonsBot = 648;

            if (x < border) {x = game.width -border; }
            if (x > game.width) { x = border; }
            if (y < buttonsTop) { y = buttonsBot; }
            if (y > buttonsBot) { y = buttonsTop; }
        }
    }
}
