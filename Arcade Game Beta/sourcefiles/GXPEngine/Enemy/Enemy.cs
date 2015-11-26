using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        protected Level _level;
        public int frameCounter = 0;

        //stores the animation state
        public EnemyState _state;

        //animation hit state
        public bool isHit;

        //stores direction
        protected EnemyDirection _enemyDirection;
        public PlayerDirection directionHit;

        //stores first and last frame
        protected float _firstFrame;
        protected float _lastFrame;

        //movement
        protected float _velocityX = 1.0f;

        //stores points and max health
        protected EnemyPoints _points;
        protected EnemyHealth _healthmax;

        //stores current health
        protected float _health;
        //Animation
        private float _frame = 0.0f;
        //TIMERS
        private float _hitTimer = 0f;
        private float _idleTimer = 0f;

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
            isHit = false;
            _state = EnemyState.idle;
        }

        //general enemy movements
        protected virtual void Move()
        {
                if (_state == EnemyState.walk)
                {
                    x += _velocityX;
                }
        }
        //make the enemy turn around
        public virtual void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;
            scaleX *= -1;
        }

        protected void StateSwitch()
        {
            switch (_state)
            {
                case EnemyState.idle:
                    _idleTimer += 0.1f;
                    if (_idleTimer >= 2f)
                    {
                        _idleTimer = 0f;
                        _state = EnemyState.walk;
                    }
                    break;
                case EnemyState.walk:                                        
                    break;
                case EnemyState.hit:
                    _hitTimer -= 1f;
                    if (_hitTimer <= 0f)
                    {
                        _hitTimer = 0f;
                        _state = EnemyState.idle;
                    }
                    break;
                case EnemyState.death:
                    
                    break;
            }
        }

        protected void animation()
        {
            _frame += 0.1f;
            
            if (_frame > _lastFrame || _frame < _firstFrame)
            {
                _frame = _firstFrame;
            }        
            SetFrame((int)_frame);
        }

        protected void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;            
        }

        //GET HIT
        public void HitByBullet(float pBulletDamage, PlayerDirection pDirection)
        {
            if (_state == EnemyState.death) return;

            if (_health <= 0f)
            {
                _state = EnemyState.death;
                _level.player.addPoints((int)_points);
            }
            else if (_health > 0f)
            {
                directionHit = pDirection;
                _health -= pBulletDamage;
                isHit = true;
                _hitTimer = pBulletDamage;
                _state = EnemyState.hit;
            }           
        }
        public virtual void recoil()
        {
            if (!isHit) return;
            if (_state == EnemyState.death) return;

            frameCounter++;

            if (frameCounter < 25)
            {
                switch (directionHit)
                {
                    case PlayerDirection.left:
                        {
                            x -= 2;
                            break;
                        }
                    case PlayerDirection.right:
                        {
                            x += 2;
                            break;
                        }
                    case PlayerDirection.up:
                        {
                            y -= 2;
                            break;
                        }
                    case PlayerDirection.down:
                        {
                            y += 2;
                            break;
                        }
                }
            }

            if (frameCounter >= 25)
            {
                frameCounter = 0;
                isHit = false;
            }
        }
    }
}