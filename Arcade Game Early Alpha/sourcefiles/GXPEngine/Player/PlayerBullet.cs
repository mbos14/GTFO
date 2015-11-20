using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PlayerBullet : Sprite
    {
        private PlayerDirection _direction;
        private float _distanceTraveled = 0.0f;
        private float _maxTravel = 200.0f;
        public float damage = 200.0f;
        private Level _level;

        private float _travelSpeed = 15.0f;
        public PlayerBullet(PlayerDirection pDirection, Level pLevel) : base("playerbullet.png")
        {
            _level = pLevel;
            SetOrigin(width / 2, height / 2);
            _direction = pDirection;
        }
        void Update()
        {
            moveDirection();
            getCollisions();
            shrinkBullet();
        }
        private void moveDirection()
        {
            //Move in given direction
            if (_direction == PlayerDirection.up)
            {
                doMove(0, -_travelSpeed);
                rotation = -90;
            }
            else if (_direction == PlayerDirection.down)
            {
                doMove(0, _travelSpeed);
                rotation = 90;
            }
            else if (_direction == PlayerDirection.left)
            {
                doMove(-_travelSpeed, 0);
                Mirror(true, false);
            }
            else if (_direction == PlayerDirection.right)
            {
                doMove(_travelSpeed, 0);
            }
        }
        private void doMove(float pX, float pY)
        {
            x += pX;
            y += pY;

            //Keep track of distance traveled
            _distanceTraveled += pX;
            _distanceTraveled += pY;

            if (_distanceTraveled >= 0)
            {
                damage = _maxTravel - _distanceTraveled;
            }
            else
            {
                damage = _maxTravel + _distanceTraveled;
            }

            //If the bullet has traveled his max distance
            if (_distanceTraveled > _maxTravel)
            {
                Destroy();
            }
            //Same with negative traveldistance
            else if (_distanceTraveled < -_maxTravel)
            {
                Destroy();
            }
        }
        private void shrinkBullet()
        {
            if (_distanceTraveled == 0) return;
            float shrinkvalue;

            if (_distanceTraveled >= 0) { shrinkvalue = (_maxTravel - _distanceTraveled) / 100.0f; }
            else { shrinkvalue = (_maxTravel + _distanceTraveled) / 100.0f; }
            //float shrinkvalue = _distanceTraveled / _maxTravel;
            if (shrinkvalue <= 1)
            {
                SetScaleXY(shrinkvalue, shrinkvalue);
            }
        }
        private void getCollisions()
        {
            if (_level.CheckCollision(this))
            {
                Destroy();
            }
        }
    }
}
