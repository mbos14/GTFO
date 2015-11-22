using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class NameInput : GameObject
    {
        public List<KeyboardButtons> buttonList = new List<KeyboardButtons>();
        private const int _buttonSize = 96;
        private int _buttonStartPointX = 32;
        private int _buttonStartPointY = 352;
        private int _frameIndicator = 0;
        private string[] _name = new string[3];
        private int buttonValue = 0;
        int stringposition = 0;
        Mouse mouse;
        string finalName;
        private Drawer _drawer = new Drawer();
        public NameInput()
        {
            ButtonCreator();
            mouse = new Mouse();
            AddChild(mouse);
        }

        void Update()
        {
            ButtonSelectTest();
            MakeName();
            updateName();
        }

        private void ButtonCreator()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    KeyboardButtons keyboardButton = new KeyboardButtons(_frameIndicator, buttonValue);
                    buttonList.Add(keyboardButton);
                    AddChild(keyboardButton);
                    keyboardButton.SetXY(_buttonStartPointX + j * _buttonSize, _buttonStartPointY + i * _buttonSize);
                    keyboardButton.SetFrame(_frameIndicator);
                    _frameIndicator += 2;
                    buttonValue++;
                }
            }
        }

        private void ButtonSelectTest()
        {
            foreach (KeyboardButtons button in buttonList)
            {
                if (button.HitTestPoint(mouse.x, mouse.y))
                {
                    button.selected = true;
                    if (Input.GetKeyDown(Key.ENTER))
                    {
                        int buttonNumber = button.GetButtonNumber();
                        if (buttonNumber < 38 && stringposition < 3)
                        {
                            _name[stringposition] = button.GetButtonValue();
                            stringposition++;
                        }
                        else if (buttonNumber == 38)
                        {
                            stringposition--;

                            if (stringposition >= 2) { stringposition = 2; }
                            if (stringposition <= 0) { stringposition = 0; }

                            _name[stringposition] = null;
                        }
                        else if (buttonNumber == 39)
                        {
                            //send finalName to highscorelist
                        }
                    }
                }
                else { button.selected = false; }
            }
        }

        private void MakeName()
        {
            finalName = "" + _name[0] + _name[1] + _name[2];
        }
        private void updateName()
        {
            AddChild(_drawer);
            _drawer.SetXY(0, 0);
            string message = "Name: " + finalName;
            PointF pos1 = new PointF(0, 0);

            _drawer.DrawText(message, pos1);
        }
    }
}
