using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    class NameInput : GameObject
    {
        public List<KeyboardButtons> buttonList = new List<KeyboardButtons>();
        private const int BUTTONSIZE = 96;
        private int _buttonStartPointX = 32;
        private int _buttonStartPointY = 352;
        private int _frameIndicator = 0;
        private string[] _name = new string[3] { "_", "_", "_" };
        private int _buttonValue = 0;
        private int _stringpos = 0;
        private Mouse _mouse;
        private Level _level;
        private HeraGUN _game;
        private Drawer _drawer = new Drawer(1024, 100);
        private string _finalName = "___";
        public NameInput(Level pLevel)
        {
            _level = pLevel;
            _game = _level.thisgame;

            drawInput();

            AddChild(_drawer);
            _drawer.SetXY(413, 120);
        }
        void Update()
        {
            ButtonSelectTest();
            MakeName();
            updateName();
        }
        protected override Collider createCollider()
        {
            return null;
        }
        private void drawInput()
        {
            Sprite background = new Sprite("keyboardbg.png");
            AddChild(background);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    KeyboardButtons keyboardButton = new KeyboardButtons(_frameIndicator, _buttonValue);
                    buttonList.Add(keyboardButton);
                    AddChild(keyboardButton);
                    keyboardButton.SetXY(_buttonStartPointX + j * BUTTONSIZE, _buttonStartPointY + i * BUTTONSIZE);
                    keyboardButton.SetFrame(_frameIndicator);
                    _frameIndicator += 2;
                    _buttonValue++;
                }
            }

            _mouse = new Mouse();
            AddChild(_mouse);

        }
        private void ButtonSelectTest()
        {
            foreach (KeyboardButtons button in buttonList)
            {
                //If the tip of the mouse is on the letter
                if (button.HitTestPoint(_mouse.x, _mouse.y))
                {
                    //Button is selected
                    button.selected = true;
                    if (Input.GetKeyDown((int)PlayerButtons.shoot))
                    {
                        int buttonNumber = button.GetButtonNumber();
                        switch (buttonNumber)
                        {
                            case 38:
                                {
                                    //Delete 1 Letter
                                    _stringpos--;
                                    if (_stringpos >= 2) { _stringpos = 2; }
                                    if (_stringpos <= 0) { _stringpos = 0; }
                                    _name[_stringpos] = "_";
                                    break;
                                }
                            case 39:
                                {
                                    if (_stringpos == 3)
                                    {
                                        //Submit name
                                        HighScores highscores = new HighScores();
                                        highscores.AddScore(_finalName, _level.thisgame.playerScore);

                                        _level.thisgame.setGameState(GameStates.endscreen);
                                    }
                                    break;
                                }
                            default:
                                {
                                    if (_stringpos < 3)
                                    {
                                        //Save letter
                                        _name[_stringpos] = button.GetButtonValue();
                                        _stringpos++;
                                    }
                                    break;
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
            _finalName = "" + _name[0] + _name[1] + _name[2];
        }
        private void updateName()
        {
            string message = "Name: " + _finalName;
            PointF pos1 = new PointF(0, 0);
            _drawer.DrawText(message, pos1);
        }
    }
}