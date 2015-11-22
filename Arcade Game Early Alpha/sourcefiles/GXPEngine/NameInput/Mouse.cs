using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Mouse : Sprite
    {
        private float _mouseSpeed = 32.0f;

        public Mouse() : base("Mouse.png")
        {
            SetOrigin(width / 2, height + 5);
            SetXY(16, 16);
        }

        void Update()
        {
            MoveMouse();
        }

        private void MoveMouse()
        {
            if (Input.GetKeyDown((int)PlayerButtons.left)){x -= _mouseSpeed; }
            if (Input.GetKeyDown((int)PlayerButtons.right)) { x += _mouseSpeed; }
            if (Input.GetKeyDown((int)PlayerButtons.up)) { y -= _mouseSpeed; }
            if (Input.GetKeyDown((int)PlayerButtons.down)) { y += _mouseSpeed; }
        }
    }
}
