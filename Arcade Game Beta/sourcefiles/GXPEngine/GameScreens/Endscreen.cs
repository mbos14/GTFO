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
        private HeraGUN _game;
        private Sprite _credits;
        public Endscreen(HeraGUN pGame, bool pLevelWon)
        {
            _game = pGame;
            drawScreen();
        }
        void Update()
        {
            _credits.y -= 2;
            endCredits();
            drawHighScores();
        }
        protected override Collider createCollider()
        {
            return null;
        }
        private void drawScreen()
        {
            //Background
            Sprite background = new Sprite("endscreenbg.png");
            AddChild(background);

            //Draw credits
            _credits = new Sprite("credits.png");
            AddChild(_credits);
            _credits.y = game.height;

            //Draw borders
            Sprite borders = new Sprite("endscreenborders.png");
            AddChild(borders);
        }
        private void drawHighScores()
        {
            //Highscores
            HighScores high = new HighScores();
            Drawer drawer = new Drawer(300, 768);
            AddChild(drawer);
            drawer.SetXY(650, 140);

            Font font = new Font("Minecraft", 25, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.LimeGreen);

            string[] highscores = high.ReturnHighScore(0);
            for (int i = 0; i < highscores.Length; i++)
            {
                PointF pos = new PointF(0, 500 - i * 40);
                drawer.graphics.DrawString(highscores[i], font, brush, pos);
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
