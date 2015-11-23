using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public class Endscreen : GameObject
    {
        private enum LevelTest { Won, Lost }
        private LevelTest CurrentWonTest;
        private MyGame _game;
        private Sprite _credits;
        private Sprite _insertcoin;
        private float _frameCounter = 0.0f;
        public Endscreen(MyGame pGame)
        {
            _game = pGame;
            checkLevel();
            drawScreen();
        }
        void Update()
        {
            _credits.y -= 1;
            insertCoinAnim();
            endCredits();
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
        private void insertCoinAnim()
        {
            _frameCounter += 0.05f;

            if (_frameCounter >= 1)
            {
                _frameCounter = 0;
                if (!_insertcoin.visible)
                {
                    _insertcoin.visible = true;
                }
                else
                {
                    _insertcoin.visible = false;
                }

            }
        }
        private void drawScreen()
        {
            //Background
            AnimationSprite background = new AnimationSprite("endscreenbg.png", 1, 2);
            AddChild(background);

            //Draw credits
            _credits = new Sprite("credits.png");
            //AddChild(_credits);
            _credits.y = game.height;

            //Draw borders
            Sprite borders = new Sprite("endscreenborders.png");
            AddChild(borders);

            //Draw YOUWON or YOULOST
            AnimationSprite wonlost = new AnimationSprite("wonlost.png", 1, 2);
            wonlost.SetOrigin(wonlost.width / 2, wonlost.height / 2);
            wonlost.SetXY(_game.width / 2, 100);
            AddChild(wonlost);

            //Check if player has won or lost
            if (CurrentWonTest == LevelTest.Won)
            {
                background.SetFrame((int)CurrentWonTest);
                wonlost.SetFrame((int)CurrentWonTest);
            }
            else if (CurrentWonTest == LevelTest.Lost)
            {
                background.SetFrame((int)CurrentWonTest);
                wonlost.SetFrame((int)CurrentWonTest);
            }

            //Draw "Throw in a coin to play again"
            _insertcoin = new Sprite("insertcoin.png");
            AddChild(_insertcoin);
            _insertcoin.SetOrigin(_insertcoin.width / 2, _insertcoin.height / 2);
            _insertcoin.SetXY(game.width / 2, game.height - _insertcoin.height);

            //Highscores
            HighScores high = new HighScores();
            AddChild(high);
            Drawer drawer = new Drawer(300, 768);
            AddChild(drawer);
            drawer.SetXY(game.width / 2, game.height / 2);
            for (int i = 0; i < 9; i++)
            {
                PointF pos1 = new PointF(10, i * 30);
                drawer.DrawText(high.ReturnHighScore(i), pos1);
            }
        }
        private void endCredits()
        {
            //Go back to menu once credits are done
            if (_credits.y + _credits.height < 0)
            {
                _game.levelWon = false;
                _game.setGameState(GameStates.menu);
            }

            //Go to level if coin is inserted
            if (Input.GetKey((int)PlayerButtons.insert))
            {
                _game.levelWon = false;
                _game.setGameState(GameStates.level);
            }
        }
    }
}
