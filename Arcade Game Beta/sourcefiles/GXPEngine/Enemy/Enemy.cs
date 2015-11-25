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

        //animation death/hit state
        protected bool _isDeath;
        public bool isHIt;

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

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
            _isDeath = false;
            isHIt = false;
            _state = EnemyState.idle;
        }

        //general enemy movements
        protected virtual void Move()
        {
            if (!isHIt)
            {
                if (_state == EnemyState.walk)
                {
                    x += _velocityX;
                }
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
                    if (_frame >= _lastFrame)
                    {
                        _state = EnemyState.walk;
                    }
                    break;
                case EnemyState.walk:
                                        
                    break;
                case EnemyState.hit:
                    if (_frame >= _lastFrame)
                    {
                        isHIt = false;
                        _state = EnemyState.idle;
                    }
                    break;
                case EnemyState.death:
                    if (_frame > _lastFrame)
                    {
                        this.Destroy();
                    }
                    break;
                    /*case EnemyState.jump:

                        break;*/
            }
        }

        protected void animation()
        {
            if (_frame <= _lastFrame)
            {
                _frame += 0.2f;
            }
            else
            {
                _frame = _firstFrame;
            }            
            //if (_frame >= 5.0f) { _frame = 2.0f; }
            //if (_frame <= 2.0f) { _frame = 2.0f; }
            SetFrame((int)_frame);
        }

        protected void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;
            _frame = pFirstFrame;
        }

        //GET HIT
        public void HitByBullet(float pBulletDamage, PlayerDirection pDirection)
        {
            if (_isDeath) return;

            if (_health <= 0)
            {
                _isDeath = true;
                _state = EnemyState.death;
                _level.player.addPoints((int)_points);
            }
            else if (_health > 0)
            {
                directionHit = pDirection;
                _health -= pBulletDamage;
                isHIt = true;
                _state = EnemyState.hit;
            }           
        }
        public virtual void recoil()
        {
            if (!isHIt) return;
            if (_isDeath) return;

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

            if (frameCounter >= 50)
            {
                frameCounter = 0;
                isHIt = false;
            }
        }
        protected virtual void die()
        {
            if (_isDeath)
            {
                //Destroyanimation?
                this.Destroy();
                this.SetXY(-200, 0); //Put outside of the screen to end collisions
                _level.player.addPoints(10);
            }
        }
    }
}