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
            scaleX *= -1;
        }

        void Update()
        {
            
        }
        //the movement pattern
        protected override void Move()
        {
            //if (!Level.positionIsFree(x + direction * TILE, y) || (level.positionIsFree(x + direction * TILE, y + TILE)) turnAround();

        }
        //Turn around
        public override void TurnAround()
        {
            //Move the other way
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
