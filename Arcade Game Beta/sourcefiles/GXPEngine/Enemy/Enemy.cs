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
        //animation death/hit state
        protected bool _isDeath;
        protected bool _isHit;
        //stores direction
        protected EnemyDirection _enemyDirection;
        //stores first and last frame
        protected float _firstFrame;
        protected float _lastFrame;
        //movement
        protected float _velocityX = 1.0f;
        //stores points and max health
        protected EnemyPoints _points;
        protected EnemyHealth _healthmax;
        //stores current health
        private float _health;

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
            _isDeath = false;
            _isHit = false;
            _health = (float)_healthmax;
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
            _health -= pBulletDamage;
            _isHit = true;
            if (_health <= 0)
            {
                _isDeath = true;
                _level.player.addPoints((int)_points);
            }           
        }
    }
}