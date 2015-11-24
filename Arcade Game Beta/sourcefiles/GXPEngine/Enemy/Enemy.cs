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
        private float _health;
        //Animation
        private float _frame = 0.0f;

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
            _isDeath = false;
            _isHit = false;
            _health = (float)_healthmax;
        }
<<<<<<< HEAD

        //MOVEMENT
=======
        void Update()
        {
            Animation();
        }
        //general enemy movements
>>>>>>> b588a38c577b70267e4bdcd486ed030cd6ca6c6d
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
<<<<<<< HEAD

=======
                    Move();
>>>>>>> b588a38c577b70267e4bdcd486ed030cd6ca6c6d
                    break;
                case AnimationStateEnemy.hit:

                    _animState = AnimationStateEnemy.idle;
                    break;
                case AnimationStateEnemy.death:

                    break;
<<<<<<< HEAD
                    /*case AnimationStateEnemy.jump:

                        break;*/
            }
        }
=======
                /*case AnimationStateEnemy.jump:                    
                    break;*/
            }
        }


        private void animation()
        {
            _frame += 0.2f;
            if (_frame >= 5.0f) { _frame = 2.0f; }
            if (_frame <= 2.0f) { _frame = 2.0f; }
            SetFrame((int)_frame);
        }

>>>>>>> b588a38c577b70267e4bdcd486ed030cd6ca6c6d
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
                _animState = AnimationStateEnemy.death;
            }
            else if (_health > 0)
            {
                _directionHit = pDirection;
                _health -= pBulletDamage;
                _isHit = true;
                _animState = AnimationStateEnemy.hit;
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
                            x -= _velocityX;
                            break;
                        }
                    case PlayerDirection.right:
                        {
                            x += _velocityX;
                            break;
                        }
                    case PlayerDirection.up:
                        {
                            y -= _velocityX;
                            break;
                        }
                    case PlayerDirection.down:
                        {
                            y += _velocityX;
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