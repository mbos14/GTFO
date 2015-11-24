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
        private Level _level;
        private MyGame _game;
        private Drawer _drawer = new Drawer(1024, 100);
        string finalName;
        public NameInput(Level pLevel)
        {
            _level = pLevel;
            _game = _level.thisgame;
            ButtonCreator();
            mouse = new Mouse();
            AddChild(mouse);
            AddChild(_drawer);
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
                //If the tip of the mouse is on the letter
                if (button.HitTestPoint(mouse.x, mouse.y))
                {
                    //Button is selected
                    button.selected = true;
                    if (Input.GetKeyDown(Key.ENTER))
                    {
                        int buttonNumber = button.GetButtonNumber();

                        if (buttonNumber < 38 && stringposition < 3)
                        {
                            //Save letter
                            _name[stringposition] = button.GetButtonValue();
                            stringposition++;
                        }
                        else if (buttonNumber == 38)
                        {
                            //Delete 1 Letter
                            stringposition--;

                            if (stringposition >= 2) { stringposition = 2; }
                            if (stringposition <= 0) { stringposition = 0; }

                            _name[stringposition] = null;
                        }
                        else if (buttonNumber == 39)
                        {
                            if (stringposition == 3)
                            {
                                //Submit name
                                HighScores highscores = new HighScores();
                                highscores.AddScore(finalName, _level.thisgame.playerScore);

                                _level.thisgame.setGameState(GameStates.endscreen);
                            }
                        }
                    }
                }
                //Otherwise button is not selected
                else { button.selected = false; }
            }
        }
        private void MakeName()
        {
            //Save final name
            finalName = "" + _name[0] + _name[1] + _name[2];
        }
        private void updateName()
        {
            string message = "Name: " + finalName;
            PointF pos1 = new PointF(0, 0);
            _game.SetChildIndex(_drawer, -1);
            _drawer.DrawText(message, pos1);
        }
    }
}