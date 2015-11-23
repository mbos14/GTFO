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

        /// <summary>
        /// Checkpoint class. Creates an sprite with collision with the player.
        /// A checkpoint can be a finish point as well.
        /// </summary>
        /// <param name="pFileName">
        /// This defines the animated image of the checkpoint
        /// </param>
        /// <param name="pThisIsCheckPoint">
        /// This defines wether this object is a checkpoint or a finishflag
        /// </param>
        /// <param name="pLevel">
        /// Passes through the leveldata
        /// </param>
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
                else if (!_thisIsCP)
                {
                    if (!_level.thisgame.levelWon)
                    {
                    _level.thisgame.levelWon = true;
                    _level.thisgame.setGameState(GameStates.bossarena);  
                    }
                    else if (_level.thisgame.levelWon)
                    {
                        _level.thisgame.setGameState(GameStates.nameinput);
                    }

 
                }
            }
        }
    }
}
