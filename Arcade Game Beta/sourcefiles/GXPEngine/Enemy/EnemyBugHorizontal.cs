﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemyBugHorizontal : Enemy
    {
        private Level _level;
        private float _velocityX = 1.0f;
        public EnemyBugHorizontal(Level pLevel) : base("robobug.png", 2, 2, pLevel)
        {
            _level = pLevel;
            SetOrigin(width / 2, height / 2);
            scaleX *= -1;
        }
        void Update()
        {
            Move();
        }
        protected override void Move()
        {
            x += _velocityX;
        }
        public override void TurnAround()
        {
            _velocityX *= -1;
            scaleX *= -1;
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
                /*case AnimationStateEnemy.hit:
                    setAnimationRange((float)BugHit.firstFrame, (float)BugHit.lastFrame);
                    break;
                case AnimationStateEnemy.death:
                    setAnimationRange((float)BugDeath.firstFrame, (float)BugDeath.lastFrame);
                    break;
                case AnimationStateEnemy.jump:
                    setAnimationRange((float)BugJump.firstFrame, (float)BugJump.lastFrame);
                    break;*/
            }
        }
    }
}