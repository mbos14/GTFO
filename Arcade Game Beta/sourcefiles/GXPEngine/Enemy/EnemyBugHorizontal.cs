using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemyBugHorizontal : Enemy
    {
        
        public EnemyBugHorizontal(Level pLevel) : base("robobug.png", 2, 3, pLevel)
        {
            _level = pLevel;
            SetOrigin(width / 2, height / 2);
            scaleX *= -1;
            _points = EnemyPoints.bug;
            _healthmax = EnemyHealth.bug;
        }
        void Update()
        {
            AnimationState();
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
                        setAnimationRange((float)BugJump.firstFrame, (float)BugJump.lastFrame);
                        break;*/
            }
        }
    }
}
