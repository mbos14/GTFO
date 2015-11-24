using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemySpider : Enemy
    {
        public EnemySpider(Level pLevel) : base("robospider.png", 4, 3, pLevel)
        {
            scaleX *= -1;
            _points = EnemyPoints.spider;
            _healthmax = EnemyHealth.spider;
        }

        void Update()
        {
            AnimationState();
            Move();
            recoil();
        }
        //chanching the animation state
        private void AnimationState()
        {
            switch (_animState)
            {
                case AnimationStateEnemy.idle:
                    setAnimationRange((float)SpiderIdle.firstFrame, (float)SpiderIdle.lastFrame);
                    break;
                case AnimationStateEnemy.walk:
                    setAnimationRange((float)SpiderWalk.firstFrame, (float)SpiderWalk.lastFrame);
                    break;
                case AnimationStateEnemy.hit:
                    setAnimationRange((float)SpiderHit.firstFrame, (float)SpiderHit.lastFrame);
                    break;
                case AnimationStateEnemy.death:
                    setAnimationRange((float)SpiderDeath.firstFrame, (float)SpiderDeath.lastFrame);
                    break;
                /*case AnimationStateEnemy.jump:
                    setAnimationRange((float)SpiderJump.firstFrame, (float)SpiderJump.lastFrame);
                    break;*/
            }
        }
    }
}
