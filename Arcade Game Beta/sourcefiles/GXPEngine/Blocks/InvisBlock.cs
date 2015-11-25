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
                    if (other.isHit)
                    {
                        if (other.directionHit == PlayerDirection.left)
                        {
                            x += 20;
                        }
                        else if (other.directionHit == PlayerDirection.right)
                        {
                            x -= 20;
                        }
                        else if (other.directionHit == PlayerDirection.up)
                        {
                            y += 20;
                        }
                        else if (other.directionHit == PlayerDirection.down)
                        {
                            y -= 20;
                        }
                        other.isHit = false;
                        other.frameCounter = 0;
                    }
                    else { other.TurnAround(); }
                }
        }
    }
}
