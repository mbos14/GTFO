using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class InvisBlock : Sprite
    {
        private Level _level;
        public InvisBlock(Level pLevel) : base("tileset.png")
        {
            _level = pLevel;
        }
        void Update()
        {
            getCollisions();
        }
        private void getCollisions()
        {
            foreach (Sprite other in _level.enemyList)
            if (HitTest(other))
                {
                    //other.TurnAround();
                }
        }
    }
}
