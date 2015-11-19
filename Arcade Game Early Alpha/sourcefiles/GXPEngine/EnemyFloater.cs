using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class EnemyFloater : Enemy
    {
        //speed at wich the frames change
        private float _frameSpeed = 0.2f;
        

        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2);
            scaleX *= -1;
        }

        void Update()
        {
            
        }

        private void Move()
        {

        }

        private void Animation()
        {

        }
    }
}
