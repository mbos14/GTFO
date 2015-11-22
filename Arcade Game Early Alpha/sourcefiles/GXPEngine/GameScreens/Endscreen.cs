using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Endscreen : GameObject
    {
        private enum LevelTest { Won, Lost }
        private LevelTest CurrentWonTest;
        private MyGame _game;
        private Sprite _credits = new Sprite("credits.png");
        public Endscreen(MyGame pGame)
        {
            _game = pGame;
            checkLevel();
            drawScreen();
        }
        void Update()
        {
            _credits.y -= 1;
        }
        private void checkLevel()
        {
            if (_game.levelWon)
            {
                CurrentWonTest = LevelTest.Won;
            }
            else if (!_game.levelWon)
            {
                CurrentWonTest = LevelTest.Lost;
            }
        }
        private void drawScreen()
        {
            //These animationsprites contain 2 frames, one for won and one for lost
            AnimationSprite wonlost = new AnimationSprite("wonlost.png", 1, 2);
            wonlost.SetOrigin(wonlost.width / 2, wonlost.height / 2);
            wonlost.SetXY(_game.width / 2, 100);
            AnimationSprite background = new AnimationSprite("endscreenbg.png", 1, 2);

            const int WON = 0;
            const int LOST = 1;

            //Draw Bounds
            
            if (CurrentWonTest == LevelTest.Won)
            {
                //Draw background
                AddChild(background);
                background.SetFrame(WON);
                //YOU WON
                AddChild(wonlost);
                wonlost.SetFrame(WON);
            }
            else if (CurrentWonTest == LevelTest.Lost)
            {
                //Draw background
                AddChild(background);
                background.SetFrame(LOST);
                //YOU LOST
                AddChild(wonlost);
                wonlost.SetFrame(LOST);
            }

            //Draw "Throw in a coin to continue"

            //Draw credits
            AddChild(_credits);
            _credits.y = game.height;

        }

    }
}
