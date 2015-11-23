using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        private Level _level;
        //stores the animation state
        protected AnimationStateEnemy _animState;
        //stores direction
        protected EnemyDirection _enemyDirection;
        //stores first and last frame
        protected float _firstFrame;
        protected float _lastFrame;

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
        }
        //general enemy movements
        protected virtual void Move()
        {

        }
        //Let the enemy turn around
        public virtual void TurnAround()
        {
            
        }
        protected void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;
        }
    }
}
