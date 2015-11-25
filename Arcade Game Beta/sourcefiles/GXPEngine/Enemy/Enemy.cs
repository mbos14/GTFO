using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        protected Level _level;
        protected int frameCounter = 0;

        //stores the animation state
        protected EnemyState _state;

        //animation death/hit state
        protected bool _isDeath;
        protected bool _isHit;

        //stores direction
        protected EnemyDirection _enemyDirection;
        protected PlayerDirection _directionHit;

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

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
            _isDeath = false;
            _isHit = false;
        }

        //MOVEMENT
        void Update()
        {
            animation();
        }
        //general enemy movements
        protected virtual void Move()
        {
            if (!_isHit)
            {
                x += _velocityX;
            }
        }
        public virtual void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;
            scaleX *= -1;
        }

        //ANIMATION
        private void setAnimState()
        {
            switch (_state)
            {
                case EnemyState.idle:

                    _state = EnemyState.walk;
                    break;
                case EnemyState.walk:
                    
                    Move();
                    break;
                case EnemyState.hit:

                    _state = EnemyState.idle;
                    break;
                case EnemyState.death:
                    break;
                    /*case EnemyState.jump:

                        break;*/
            }
        }
        protected void animation()
        {
            _frame += 0.2f;
            if (_frame >= 5.0f) { _frame = 2.0f; }
            if (_frame <= 2.0f) { _frame = 2.0f; }
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
            if (_isDeath) return;

            if (_health <= 0)
            {
                _isDeath = true;
                _level.player.addPoints((int)_points);
                _state = EnemyState.death;
            }
            else if (_health > 0)

            {
                _directionHit = pDirection;
                _health -= pBulletDamage;
                _isHit = true;
                _state = EnemyState.hit;
            }

            if (_health <= 0)
            {
                _isDeath = true;
                _level.player.addPoints((int)_points);
                _state = EnemyState.death;
            }

        }
        public virtual void recoil()
        {
            if (!_isHit) return;
            if (_isDeath) return;

            frameCounter++;

            if (frameCounter < 25)
            {
                switch (_directionHit)
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

            if (frameCounter >= 50)
            {
                frameCounter = 0;
                _isHit = false;
            }
        }
        protected virtual void die()
        {
            if (_isDeath)
            {
                //Destroyanimation?
                this.Destroy();
                _level.player.addPoints(10);
            }
        }
    }
}