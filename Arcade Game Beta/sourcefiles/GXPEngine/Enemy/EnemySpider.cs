using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemySpider : Enemy
    {
        //Animation
        private float _frame = 0.0f;

        public EnemySpider(Level pLevel) : base("robospider.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, 0);
            scaleX *= -1;
            _points = EnemyPoints.spider;
            _healthmax = EnemyHealth.spider;
        }

        void Update()
        {
            AnimationState();
        }

        private void animation()
        {
            _frame += 0.2f;
            if (_frame >= 5.0f) { _frame = 2.0f; }
            if (_frame <= 2.0f) { _frame = 2.0f; }
            SetFrame((int)_frame);
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
