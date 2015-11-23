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


        private float _velocityX = 2.0f;

        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2);
        }

        void Update()
        {
            Move();
        }
        //the movement pattern
        protected override void Move()
        {
            x += _velocityX;
        }
        //Turn around
        public override void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;
            scaleX *= -1;
        }
        //the animation
        private void Animation()
        {
            switch(_animState)
            {
                case AnimationStateEnemy.idle:
                    //setAnimationRange((float)SpiderIdle);
                    break;
                case AnimationStateEnemy.walk:

                    break;
                case AnimationStateEnemy.hit:

                    break;
                case AnimationStateEnemy.death:

                    break;
                case AnimationStateEnemy.jump:

                    break;
            }
        }
    }
}
