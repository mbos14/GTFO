using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemyBugHorizontal : Enemy
    {
        public float startY;
        private float _bulletTimer = 0.0f;

        public EnemyBugHorizontal(Level pLevel, float pStartY) : base("robobug.png", 2, 3, pLevel, EnemyPoints.floater, EnemyHealth.floater)
        {
            startY = pStartY;
            SetOrigin(width / 2, height / 2);
            Mirror(true, false);
        }
        void Update()
        {
            //related to movement
            getBackInPos();            
            //related to states
            StateSwitch();
            AnimationState();
        }       
        //ANIMATION
        //chanching the animation range
        private void AnimationState()
        {
            switch (_state)
            {
                case EnemyState.idle:
                    setAnimationRange((float)BugIdle.firstFrame, (float)BugIdle.lastFrame);
                    break;
                case EnemyState.walk:
                    setAnimationRange((float)BugWalk.firstFrame, (float)BugWalk.lastFrame);
                    break;
                case EnemyState.hit:
                    setAnimationRange((float)BugHit.firstFrame, (float)BugHit.lastFrame);
                    break;
                case EnemyState.death:
                    setAnimationRange((float)BugDeath.firstFrame, (float)BugDeath.lastFrame);
                    break;
            }
        }        

        //LOGIC
        //shoots a bullet at player
        private void shootBullet()
        {
            _bulletTimer += 0.01f;
            if (DistanceTo(_level.player) < 400)
            {
                if (_bulletTimer >= 1)
                {
                    EnemyBullet bullet = new EnemyBullet(_enemyDirection, _level);
                    if (_enemyDirection == EnemyDirection.left)
                    {
                        bullet.SetXY(x + 20, y);
                        bullet.Mirror(true, false);
                    }
                    else if (_enemyDirection == EnemyDirection.right)
                    {
                        bullet.SetXY(x - width, y);
                        bullet.Mirror(false, false);
                    }
                    _level.AddChild(bullet);
                    _bulletTimer = 0;
                }
            }
        }
        //uses states to switch between wich could should be used.
        protected override void StateSwitch()
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
                        shootBullet();
                        WalkingIdleAnimation();
                        break;
                    }
                case EnemyState.hit:
                    recoil();
                    DeathHitAnimation();
                    _hitTimer -= 1f;
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
        //moves it back in the right location
        private void getBackInPos()
        {
            if (y == startY) return;
            if (_state == EnemyState.hit) return;

            if (y > startY) y -= 2;
            if (y < startY) y += 2;
        }
        //turns the enemy around
        public override void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;

            if (_mirrorX)
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
