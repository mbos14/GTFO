using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GXPEngine
{
    public class Menu : GameObject
    {

        private MyGame _game;
        private bool _coinInserted = false;
        HighScores _highscore = new HighScores();

        public Menu(MyGame pGame)
        {
            AddChild(_highscore);
            _game = pGame;
            drawBackGround();
        }
        private void checkButtons()
        {
            if (Input.GetAnyKeyDown() && _coinInserted)
            {
                _game.setGameState(GameStates.level);
            }
            if (Input.GetKeyDown((int)PlayerButtons.insert))
            {
                _coinInserted = true;
            }
        }
        void Update()
        {
            checkButtons();
        }
        private void drawText()
        {
            //Title
            //Insert coin
            //Press any button to start the game
        }
        private void drawBackGround()
        {
            Canvas menuBG = new Canvas("menubackground.png");
            menuBG.SetScaleXY(4, 4);
            AddChild(menuBG);
        }

    }
}
