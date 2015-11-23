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
        //animation death state
        protected bool _isDeath;
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
            _isDeath = false;
        }
        //general enemy movements
        protected virtual void Move()
        {
            x += _velocityX;
        }
        //Let the enemy turn around
        public virtual void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;
            scaleX *= -1;
        }

        protected void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;
        }
        //is called from level to parce the death state to the enemy
        public void HitByBullet(float pBulletDamage)
        {
            _isDeath = true;
        }
    }
}