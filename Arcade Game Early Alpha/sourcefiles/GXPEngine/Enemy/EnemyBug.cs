using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class EnemyBug : Enemy
    {
        //speed at wich the frames change
        //private float _frameSpeed = 0.2f;


        public EnemyBug(Level pLevel) : base("robobug.png", 2, 2, pLevel)
        {
            SetOrigin(width / 2, height / 2);
            scaleX *= -1;
        }

        void Update()
        {
            Move();
            Animation();
        }
        //the movement pattern
        protected override void Move()
        {
            //if (!Level.positionIsFree(x + direction * TILE, y) || (level.positionIsFree(x + direction * TILE, y + TILE)) turnAround();

        }
        //the animation
        private void Animation()
        {

        }
    }
}
