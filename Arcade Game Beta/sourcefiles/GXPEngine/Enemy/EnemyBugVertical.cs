using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemyBugVertical : Enemy
    {
        private float _velocityY = 1.0f;
        private float _bulletTimer = 0.0f;
        private float _startX;
        public EnemyBugVertical(Level pLevel, float pStartX) : base("robobug.png", 2, 3, pLevel)
        {
            _level = pLevel;
            _startX = pStartX;
            SetOrigin(width / 2, height / 2);
            _points = EnemyPoints.bug;
            _healthmax = EnemyHealth.bug;
            _health = (float)_healthmax;
        }
        void Update()
        {
            recoil();
            Move();
            lookDirection();
            getBackInPos();

            animation();
            AnimationState();

            shootBullet();
            die();
        }
        protected override void Move()
        {
            if (_isHit) return;
            if (_isDeath) return;

            y += _velocityY;
            _state = EnemyState.walk;
        }
        private void getBackInPos()
        {
            if (x == _startX) return;
            if (_isHit) return;

            if (x > _startX) y -= 2;
            if (x < _startX) y += 2;
        }
        private void lookDirection()
        {
            if (_level.player.x > x) { Mirror(true, false); _enemyDirection = EnemyDirection.left; }
            else if (_level.player.x < x) { Mirror(false, false); _enemyDirection = EnemyDirection.right; }
        }
        private void shootBullet()
        {
            _bulletTimer += 0.01f;
            if (DistanceTo(_level.player) < 400)
            {
                if (_bulletTimer >= 1)
                {
                    EnemyBullet bullet = new EnemyBullet(_enemyDirection, _level);
                    bullet.SetXY(x, y);
                    _level.AddChild(bullet);
                    _bulletTimer = 0;
                }
            }
        }
        public override void TurnAround()
        {
            _velocityY *= -1;
        }
        //chanching the animation state
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
                    /*case EnemyState.jump:
                        setAnimationRange((float)BugJump.firstFrame, (float)BugJump.lastFrame)
                        break;*/
            }
        }
    }
}