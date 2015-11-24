using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class InvisBlock : AnimationSprite
    {
        private Level _level;
        public InvisBlock(Level pLevel) : base("tileset.png", 23, 45)
        {
            _level = pLevel;
        }
        void Update()
        {
            getCollisions();
        }
        private void getCollisions()
        {
            foreach (Enemy other in _level.enemyList)
            if (HitTest(other))
                {
                    other.TurnAround();
                }
        }
    }
}
