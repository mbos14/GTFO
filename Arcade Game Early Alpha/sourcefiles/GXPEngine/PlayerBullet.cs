using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PlayerBullet : Sprite
    {
        private string _direction;
        private float _distanceTraveled = 0.0f;
        private float _maxTravel = 50.0f;
        public float damage = 50.0f;

        private float _travelSpeed = 5.0f;
        public PlayerBullet(string pDirection) : base("playerbullet.png")
        {
            SetOrigin(width / 2, height / 2);
            _direction = pDirection;
        }
        void Update()
        {
            moveDirection();
        }
        private void moveDirection()
        {
            //Move in given direction
            if (_direction == "up") { doMove(0, -_travelSpeed); }
            else if (_direction == "down") { doMove(0, _travelSpeed); }
            else if (_direction == "left") { doMove(-_travelSpeed, 0); }
            else if (_direction == "right") { doMove(_travelSpeed, 0); }
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
    }
}
