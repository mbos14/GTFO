using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        protected Level _level;
        //stores the animation state
        protected AnimationStateEnemy _animState;
        //stores direction
        protected EnemyDirection _enemyDirection;
        //stores first and last frame
        protected float _firstFrame;
        protected float _lastFrame;
        //movement
        protected float _velocityX = 1.0f;

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
        }
        //general enemy movements
        protected virtual void Move()
        {
            x += _velocityX;
        }
        //Let the enemy turn around
        public virtual void TurnAround()
        {
            _velocityX *= -1;
            scaleX *= -1;
        }

        protected void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;
        }
    }
}