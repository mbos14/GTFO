using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemyBugHorizontal : Enemy
    {
        public float startY;
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
            Move();
            recoil();
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
    }
}
