using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemyBullet : Sprite
    {
        public EnemyDirection direction;
        private float _speed = 5.0f;
        private Level _level;
        public bool bulletTurnedAround = false;
        public float distanceTraveled = 0.0f;
        public EnemyBullet(EnemyDirection pDirection, Level pLevel) : base("enemybullet.png")
        {
            _level = pLevel;
            direction = pDirection;
            _level.enemyBulletList.Add(this);
        }
        void Update()
        {
            moveBullet();
            checkCollisions();
            checkDistance();
        }
        private void moveBullet()
        {
            if (direction == EnemyDirection.left)
            {
                x += _speed;
            }
            else if (direction == EnemyDirection.right)
            {
                x -= _speed;
            }
            else if (direction == EnemyDirection.up)
            {
                y -= _speed;
            }
            else if (direction == EnemyDirection.down)
            {
                y += _speed;
            }
            distanceTraveled += _speed;
        }
        public void TurnBullet(PlayerDirection pDirection)
        {
            if (pDirection == PlayerDirection.left) { direction = EnemyDirection.right; }
            if (pDirection == PlayerDirection.right) { direction = EnemyDirection.left; }
            if (pDirection == PlayerDirection.up) { direction = EnemyDirection.up; }
            if (pDirection == PlayerDirection.down) { direction = EnemyDirection.down; }
        }
        private void checkCollisions()
        {
            if (HitTest(_level.player))
            {
                _level.player.playerDIE();
            }

            if (bulletTurnedAround)
            {
                foreach (Sprite other in _level.enemyList)
                {
                    if (HitTest(other))
                    {
                        other.Destroy();
                        _level.player.addPoints(20);
                        Destroy();
                    }
                }
            }
        }
        private void checkDistance()
        {
            if (distanceTraveled > 300)
            {
                this.Destroy();
            }
        }
    }
}
