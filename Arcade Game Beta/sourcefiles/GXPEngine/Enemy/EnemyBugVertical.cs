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
        public EnemyBugVertical(Level pLevel) : base("robobug.png", 2, 3, pLevel)
        {
            _level = pLevel;
            SetOrigin(width / 2, height / 2);
            _points = EnemyPoints.bug;
            _healthmax = EnemyHealth.bug;
        }
        void Update()
        {
            recoil();
            AnimationState();
            Move();
            lookDirection();
            shootBullet();
            die();
        }
        protected override void Move()
        {
            if (_isHit) return;
            if (_isDeath) return;

            y += _velocityY;
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
            //y -= _velocityY;
            _velocityY *= -1;
        }
        //chanching the animation state
        private void AnimationState()
        {
            switch (_animState)
            {
                case AnimationStateEnemy.idle:
                    setAnimationRange((float)BugIdle.firstFrame, (float)BugIdle.lastFrame);
                    break;
                case AnimationStateEnemy.walk:
                    setAnimationRange((float)BugWalk.firstFrame, (float)BugWalk.lastFrame);
                    break;
                case AnimationStateEnemy.hit:
                    setAnimationRange((float)BugHit.firstFrame, (float)BugHit.lastFrame);
                    break;
                case AnimationStateEnemy.death:
                    setAnimationRange((float)BugDeath.firstFrame, (float)BugDeath.lastFrame);
                    break;
                    /*case AnimationStateEnemy.jump:
                        setAnimationRange((float)BugJump.firstFrame, (float)BugJump.lastFrame)
                        break;*/
            }
        }
    }
}