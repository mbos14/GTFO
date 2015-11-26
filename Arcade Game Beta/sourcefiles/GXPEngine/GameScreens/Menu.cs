using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        private HeraGUN _game;
        private bool _coinInserted = false;
        public Menu(HeraGUN pGame)
        {
            _game = pGame;
            drawBackGround();
        }
        protected override Collider createCollider()
        {
            return null;
        }
        private void checkButtons()
        {
            if (Input.GetAnyKeyDown() && _coinInserted)
            {
                _game.setGameState(GameStates.part1);
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
            AddChild(menuBG);
        }

    }
}
