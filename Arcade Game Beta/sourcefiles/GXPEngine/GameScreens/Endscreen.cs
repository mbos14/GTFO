using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Endscreen : GameObject
    {
        private enum LevelTest { Won, Lost }
        private LevelTest CurrentWonTest;
        private HeraGUN _game;
        private Sprite _credits;
        private AnimationSprite _insertcoin;
        private float _frameCounter = 0.0f;
        private Drawer _drawer = new Drawer(300, 768);
        public Endscreen(HeraGUN pGame)
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
            drawHighScores();
        }
        protected override Collider createCollider()
        {
            return null;
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
            //Check if player has won or lost
            if (CurrentWonTest == LevelTest.Won)
            {
                Sprite sprite = new Sprite("youwin.png");
                AddChild(sprite);
            }
            else if (CurrentWonTest == LevelTest.Lost)
            {
                Sprite sprite = new Sprite("gameover.png");
                AddChild(sprite);
            }

            //Draw credits
            _credits = new Sprite("credits.png");
            AddChild(_credits);
            _credits.y = game.height;

            //Draw borders
            Sprite borders = new Sprite("endscreenborders.png");
            AddChild(borders);

            //Draw "Throw in a coin to play again"
            _insertcoin = new AnimationSprite("text.png", 1, 4);
            AddChild(_insertcoin);
            _insertcoin.SetFrame(3);
            _insertcoin.SetOrigin(_insertcoin.width / 2, _insertcoin.height / 2);
            _insertcoin.SetXY(game.width / 2, game.height - _insertcoin.height);

        }
        private void drawHighScores()
        {
            //Highscores
            HighScores high = new HighScores();
            AddChild(high);

            AddChild(_drawer);

            string[] highscores = high.ReturnHighScore(0);

            for (int i = 0; i < highscores.Length; i++)
            {
                PointF pos = new PointF(0, 500 - i * 50);
                _drawer.DrawText(highscores[i], pos);
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
                _game.setGameState(GameStates.part1);
            }
        }
    }
}
