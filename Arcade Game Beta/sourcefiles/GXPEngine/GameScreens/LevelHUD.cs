using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class LevelHUD : AnimationSprite
    {
        private Level _level;
        private HeraGUN _game;
        Drawer drawer;
        public LevelHUD(Level pLevel) : base("hud.png", 1, 4)
        {
            _level = pLevel;
            _game = pLevel.thisgame;

            drawer = new Drawer(1024, 100);
            AddChild(drawer);

            game.Remove(this);
            game.Add(this);
        }
        void Update()
        {
            drawText();
            updateFrame();
        }
        private void drawText()
        {
            string message = _level.thisgame.playerLives + "";
            string message2 = _game.playerScore + "";
            string message3 = _level.thisgame.playerCoins + "";

            PointF pos1 = new PointF(160, 35);
            PointF pos2 = new PointF(_game.width - 220, 5);
            PointF pos3 = new PointF(_game.width - 220, 65);

            drawer.DrawText(message, pos1);
            drawer.DrawText(message2, pos2);
            drawer.DrawText(message3, pos3);
        }
        private void updateFrame()
        {
            if (_level.player.hasWeapon)
            {
                SetFrame((int)_level.player.bulletCounter);
            }
            else SetFrame(0);
        }
        protected override Collider createCollider()
        {
            return null;
        }
    }
}
