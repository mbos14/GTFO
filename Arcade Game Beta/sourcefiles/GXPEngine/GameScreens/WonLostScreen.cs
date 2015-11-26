using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class WonLostScreen : GameObject
    {
        private HeraGUN _game;
        private float _frameCounter = 0.0f;

        AnimationSprite pressButton = new AnimationSprite("text.png", 1, 4);
        public WonLostScreen(HeraGUN pGame, bool pLevelWon)
        {
            _game = pGame;
            getEnding(pLevelWon);

            AddChild(pressButton);
            pressButton.SetOrigin(pressButton.width / 2, pressButton.height);
            pressButton.SetXY(game.width / 2, game.height);
            pressButton.SetFrame(0);
        }
        void Update()
        {
            pressButtonAnim();
            getButton();
        }
        private void getEnding(bool pLevelWon)
        {
            if (pLevelWon)
            {
                Sprite background = new Sprite("youwon.png");
                AddChild(background);
            }
            else
            {
                Sprite background = new Sprite("gameover.png");
                AddChild(background);
            }
        }
        private void pressButtonAnim()
        {
            _frameCounter += 0.02f;

            if (_frameCounter >= 1)
            {
                _frameCounter = 0;
                visible = visible ? false : true;
            }
        }
        private void getButton()
        {
            if (Input.GetKeyDown((int)PlayerButtons.shoot))
            {
                _game.setGameState(GameStates.nameinput);
            }
        }
    }
}
