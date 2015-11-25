﻿using System;
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
            _health = (float)_healthmax;
        }

        void Update()
        {
            AnimationState();
            animation();

            Move();
            recoil();
        }
        //chanching the animation state
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
<<<<<<< HEAD
=======
                    /*case EnemyState.jump:
                        setAnimationRange((float)SpiderJump.firstFrame, (float)SpiderJump.lastFrame);
                        break;*/
>>>>>>> cd0dd5e6d215dad5b90ff7c06875869a53b101fa
            }
        }
    }
}
