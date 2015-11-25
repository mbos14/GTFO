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
        protected AnimationStateEnemy _animState;

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
            switch (_animState)
            {
                case AnimationStateEnemy.idle:

                    _animState = AnimationStateEnemy.walk;
                    break;
                case AnimationStateEnemy.walk:
                    Move();
                    break;
                case AnimationStateEnemy.hit:

                    _animState = AnimationStateEnemy.idle;
                    break;
                case AnimationStateEnemy.death:
                    break;
                    /*case AnimationStateEnemy.jump:

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

            Console.WriteLine("Health: " + _health);
            Console.WriteLine("Damage: " + pBulletDamage);

            if (_health > 0)
            {
                _directionHit = pDirection;
                _health -= pBulletDamage;
                _isHit = true;
                _animState = AnimationStateEnemy.hit;
            }

            if (_health <= 0)
            {
                _isDeath = true;
                _level.player.addPoints((int)_points);
                _animState = AnimationStateEnemy.death;
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