using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class EnemyFloater : Enemy
    {
        //speed at wich the frames change
        //private float _frameSpeed = 0.2f;

        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2);
            _points = EnemyPoints.floater;
            _healthmax = EnemyHealth.floater;
        }

        void Update()
        {
            recoil();
            AnimationState();
            Move();
        }
        //chanching the animation state
        private void AnimationState()
        {
            switch(_animState)
            {
                case AnimationStateEnemy.idle:
                    setAnimationRange((float)FLoaterIdle.firstFrame,(float)FLoaterIdle.lastFrame);
                    break;
                case AnimationStateEnemy.walk:
                    setAnimationRange((float)FLoaterWalk.firstFrame, (float)FLoaterWalk.lastFrame);
                    break;
                case AnimationStateEnemy.hit:
                    setAnimationRange((float)FLoaterHit.firstFrame, (float)FLoaterHit.lastFrame);
                    break;
                case AnimationStateEnemy.death:
                    setAnimationRange((float)FLoaterDeath.firstFrame, (float)FLoaterDeath.lastFrame);
                    break;
                /*case AnimationStateEnemy.jump:
                    setAnimationRange((float)FLoaterJump.firstFrame, (float)FLoaterJump.lastFrame);
                    break;*/
            }
        }
    }
}
