using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class InvisBlock : AnimationSprite
    {
        private Level _level;
        List<GameObject> previousFrameCollisions = new List<GameObject>();
        public InvisBlock(Level pLevel) : base("tileset.png", 23, 45)
        {
            _level = pLevel;
        }
        void Update()
        {
            HandleCollisions();
        }
        //tests and handle all collisions for this object
        private void HandleCollisions()
        {
            List<GameObject> currentFrameCollisions = new List<GameObject>();
            foreach (Enemy other in _level.enemyList)
            {
                if (HitTest(other))
                {
                    if (!previousFrameCollisions.Contains(other))
                    {
                        OnCollisionEnter(other);
                    }
                    currentFrameCollisions.Add(other);
                }
            }
            previousFrameCollisions = currentFrameCollisions;
        }
        //is called only for new collisions
        private void OnCollisionEnter(Enemy other)
        {
            if (other._state == EnemyState.hit)
            {
                other._state = EnemyState.walk;
                if (other.directionHit == PlayerDirection.left)
                {
                    while (HitTest(other))
                    {
                        x -= 10;
                    }
                }
                else if (other.directionHit == PlayerDirection.right)
                {
                    while (HitTest(other))
                    {
                        x += 10;
                    }
                }
                else if (other.directionHit == PlayerDirection.up)
                {
                    while (HitTest(other))
                    {
                        y -= 10;
                    }
                }
                else if (other.directionHit == PlayerDirection.down)
                {
                    while (HitTest(other))
                    {
                        y += 10;
                    }
                }
            }
            other._state = EnemyState.walk;
            other.TurnAround();
        }
    }
}
