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

        public EnemyBugHorizontal(Level pLevel, float pStartY) : base("robobug.png", 2, 3, pLevel)
        {
            startY = pStartY;
            _level = pLevel;
            SetOrigin(width / 2, height / 2);
            Mirror(true, false);
            _points = EnemyPoints.bug;
            _healthmax = EnemyHealth.bug;
            _health = (float)_healthmax;
        }
        void Update()
        {
            //related to movement
            getBackInPos();
            recoil();
            shootBullet();
            //related to animation
            animation();
            //related to states
            StateSwitch();
            AnimationState();
        }
        //ANIMATE
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
        //MOVEMENT
        private void getBackInPos()
        {
            if (y == startY) return;
            if (isHit) return;

            if (y > startY) y -= 2;
            if (y < startY) y += 2;
        }
        private void shootBullet()
        {
            if (_state == EnemyState.walk)
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
        }
        public override void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;

            if (_mirrorX)
            {
                Mirror(false, false);
                _enemyDirection = EnemyDirection.right;
            }
            else if (!_mirrorX)
            {
                Mirror(true, false);
                _enemyDirection = EnemyDirection.left;
            }
        }
    }
}
