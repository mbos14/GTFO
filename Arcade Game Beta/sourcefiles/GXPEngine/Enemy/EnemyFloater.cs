using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class EnemyFloater : Enemy
    {
        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2 - 10);
            _points = EnemyPoints.floater;
            _healthmax = EnemyHealth.floater;
            _health = (float)_healthmax;
        }
        void Update()
        {
            recoil();
            playerDistance();
            Move();

            AnimationState();
            animation();
        }
        protected override void Move()
        {
            if (_animState == AnimationStateEnemy.walk)
            {
                base.Move();
            }
        }
        private void playerDistance()
        {
            if (DistanceTo(_level.player) > 400)
            {
                _animState = AnimationStateEnemy.idle;
            }
            else if (DistanceTo(_level.player) <= 400)
            {
                _animState = AnimationStateEnemy.walk;
            }
        }
        //chanching the animation state
        private void AnimationState()
        {
            switch(_state)
            {
                case EnemyState.idle:
                    setAnimationRange((float)FLoaterIdle.firstFrame,(float)FLoaterIdle.lastFrame);
                    break;
                case EnemyState.walk:
                    setAnimationRange((float)FLoaterWalk.firstFrame, (float)FLoaterWalk.lastFrame);
                    break;
                case EnemyState.hit:
                    setAnimationRange((float)FLoaterHit.firstFrame, (float)FLoaterHit.lastFrame);
                    break;
                case EnemyState.death:
                    setAnimationRange((float)FLoaterDeath.firstFrame, (float)FLoaterDeath.lastFrame);
                    break;
<<<<<<< HEAD
=======
                    /*case EnemyState.jump:
                        setAnimationRange((float)FLoaterJump.firstFrame, (float)FLoaterJump.lastFrame);
                        break;*/
>>>>>>> fe5dd89189d6215925353291b1281672f352d3dd
            }
        }
    }
}
