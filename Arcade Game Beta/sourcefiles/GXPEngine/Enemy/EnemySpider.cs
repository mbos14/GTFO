using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemySpider : Enemy
    {
        public EnemySpider(Level pLevel) : base("robospider.png", 4, 3, pLevel, EnemyPoints.floater, EnemyHealth.floater)
        {
            scaleX *= -1;
            SetOrigin(width / 2, 0);
        }

        void Update()
        {
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
                    setAnimationRange((float)SpiderIdle.firstFrame, (float)SpiderIdle.lastFrame);
                    break;
                case EnemyState.walk:
                    setAnimationRange((float)SpiderWalk.firstFrame, (float)SpiderWalk.lastFrame);
                    break;
                case EnemyState.hit:
                    setAnimationRange((float)SpiderHit.firstFrame, (float)SpiderHit.lastFrame);
                    break;
                case EnemyState.death:
                    setAnimationRange((float)SpiderDeath.firstFrame, (float)SpiderDeath.lastFrame);
                    break;
            }
        }

        //MOVEMENT
        //if hit get pushed back in the opisite direction
        public override void recoil()
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
                }
            }

            if (frameCounter >= 25)
            {
                frameCounter = 0;
            }
        }
    }
}