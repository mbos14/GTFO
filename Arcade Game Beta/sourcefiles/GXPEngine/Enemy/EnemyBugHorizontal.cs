using System;
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
        protected override void Move()
        {
            x += _velocityX;
        }
        public override void TurnAround()
        {
            _velocityX *= -1;
            scaleX *= -1;
        }
    }
}
