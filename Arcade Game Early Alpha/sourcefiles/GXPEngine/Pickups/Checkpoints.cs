using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Checkpoints : AnimationSprite
    {
        private bool _thisIsCP;
        private Level _level;
        public Checkpoints(string pFileName, bool pThisIsCheckPoint, Level pLevel) : base(pFileName, 10, 10)
        {
            _level = pLevel;
            _thisIsCP = pThisIsCheckPoint;
        }
        void Update()
        {
            getCollisions();
        }
        private void getCollisions()
        {
            if (HitTest(_level.player))
            {
                if (_thisIsCP)
                {
                    _level.player.spawnX = x;
                    _level.player.spawnY = y;
                    //Play animation / Set frame to captured
                }
                else
                {
                    _level.thisgame.levelWon = true;
                    _level.thisgame.setGameState(GameStates.nameinput);   
                }
            }
        }
    }
}
