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
        private float _frame = 0.0f;
        private bool _checked = false;

        public Checkpoints(bool pThisIsCheckPoint, Level pLevel) : base("checkpoint.png", 6, 0)
        {
            _level = pLevel;
            _thisIsCP = pThisIsCheckPoint;
        }
        void Update()
        {
            getCollisions();
            animate();
        }
        private void getCollisions()
        {
            if (HitTest(_level.player))
            {
                if (_thisIsCP)
                {
                    _checked = true;
                    _level.player.spawnX = x;
                    _level.player.spawnY = y;
                }

                else if (!_thisIsCP)
                {
                    //Go to part 2
                    if (_level.levelPart == 1) { _level.thisgame.setGameState(GameStates.part2); } 
                    //Go to part 3
                    else if (_level.levelPart == 2) { _level.thisgame.setGameState(GameStates.part3); }
                    //Finish game
                    else if (_level.levelPart == 3) { _level.thisgame.setGameState(GameStates.nameinput); }
                }
            }
        }
        private void animate()
        {
            if (_checked)
            {
                _frame += 0.05f;
                if (_frame > frameCount) { _frame = frameCount; }
                SetFrame((int)_frame);
            }
        }
    }
}