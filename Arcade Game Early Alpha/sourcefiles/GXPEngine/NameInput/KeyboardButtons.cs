using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class KeyboardButtons : AnimationSprite
    {
        public bool selected = false;
        private int thisFrame;
        private int _buttonValue;
        private string[] value = {"1","2","3","4","5","6","7","8","9","0","Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M",".",",","!","?","="};

        public KeyboardButtons(int pFrame, int pButtonValue) : base("KeyboardButtons.png",10,8)
        {
            thisFrame = pFrame;
            _buttonValue = pButtonValue;
        }

        void Update()
        {
            updateFrame();
        }
        private void updateFrame()
        {
            if (selected) { currentFrame = thisFrame + 1; }
            else { currentFrame = thisFrame; }
        }

        public string GetButtonValue()
        {
            return value[_buttonValue];
        }
    }
}
