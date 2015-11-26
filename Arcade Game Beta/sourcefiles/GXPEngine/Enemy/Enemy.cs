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
        protected float _frame = 0;
        //TIMERS
        protected float _hitTimer;
        protected float _idleTimer;
        private float _deathHitAnimationTimer;
        protected float _bugDeathTimer;

        public Enemy(string pFileName, int pColumns, int pRows, Level pLevel, EnemyPoints pPoints, EnemyHealth pHealth, EnemyDirection pEnemyDirection = EnemyDirection.left, bool pMirrow = true) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
            _state = EnemyState.idle;
            _points = pPoints;
            _healthmax = pHealth;
            _health = (float)_healthmax;
            _enemyDirection = pEnemyDirection;
            Mirror(pMirrow, false);
            _hitTimer = 0f;
            _idleTimer = 0f;
            _deathHitAnimationTimer = 0;
        }
        //ANIMATION
        //animation for death and hit state
        protected void DeathHitAnimation()
        {
            _deathHitAnimationTimer += 1;
            if (_deathHitAnimationTimer == 10)
            {
                _frame += 1;
                if (_frame < _firstFrame)
                {
                    _frame = _firstFrame;
                }
                else if (_frame > _lastFrame)
                {
                    _frame = _lastFrame;
                }
                _deathHitAnimationTimer = 0;
                SetFrame((int)_frame);
            }
        }
        //set the animation range
        protected void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;
        }
        //animation for walking and idle state
        protected void WalkingIdleAnimation()
        {
            _frame += 0.1f;
            //checks if not bigger then last frame and not smaller then first frame
            if (_frame < _firstFrame || _frame > _lastFrame)
            {
                _frame = _firstFrame;
            }
            SetFrame((int)_frame);

        }

        //LOGIC
        //bug die
        protected void BugDie()
        {                        
            _bugDeathTimer -= 0.01f;
            if (_bugDeathTimer > 0)
            {
                alpha = _bugDeathTimer;
            }              
        }
        //gethit
        public void HitByBullet(float pBulletDamage, PlayerDirection pDirection)
        {
            SoundChannel soundChannel = new SoundChannel(2);
            Sound hit = new Sound("hurt.wav");
            hit.Play(false, 2);

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
                _hitTimer = pBulletDamage;
                _state = EnemyState.hit;
                _level.player.addPoints(10);
            }
        }
        //uses states to switch between wich could should be used.
        protected virtual void StateSwitch()
        {
            switch (_state)
            {
                case EnemyState.idle:
                    WalkingIdleAnimation();
                    _idleTimer += 0.1f;
                    if (_idleTimer >= 2f)
                    {
                        _idleTimer = 0f;
                        _state = EnemyState.walk;
                    }
                    break;
                case EnemyState.walk:
                    {
                        Move();
                        WalkingIdleAnimation();
                        break;
                    }
                case EnemyState.hit:
                    recoil();
                    DeathHitAnimation();
                    _hitTimer -= 0.5f;
                    if (_hitTimer <= 0f)
                    {
                        _hitTimer = 0f;
                        _state = EnemyState.idle;
                    }
                    break;
                case EnemyState.death:
                    DeathHitAnimation();
                    break;
            }
        }

        //MOVEMENT
        //general enemy movements
        protected virtual void Move()
        {
            x += _velocityX;
        }       
        //if hit get pushed back in the opisite direction
        public virtual void recoil()
        {
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
            }
        }
        //make the enemy turn around
        public virtual void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;
            //scaleX *= -1;
            if (_enemyDirection == EnemyDirection.left)
            {
                Mirror(false, false);
                _enemyDirection = EnemyDirection.right;
            }
            else
            {
                Mirror(true, false);
                _enemyDirection = EnemyDirection.left;
            }
        }
    }
}