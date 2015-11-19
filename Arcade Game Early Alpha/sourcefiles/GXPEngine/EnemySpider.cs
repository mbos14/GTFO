using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemySpider : Enemy
    {
        private Level _level;
        private int _moveCounter = 0;
        private float _velocityX = 1.0f;

        //Animation
        private float _frame = 0.0f;
        private float _firstFrame;
        private float _lastFrame;

        private int _animState;

        const int IDLE = 0;
        const int WALK = 1;
        const int BLOWED = 2;
        const int DEAD = 3;
        public EnemySpider(Level pLevel) : base("robospider.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2);
            _level = pLevel;
            scaleX *= -1;
        }
        void Update()
        {
            animation();
            moveSpider();
        }
        private void moveSpider()
        {
            if (_moveCounter >= 100)
            {
                _moveCounter = 0;
                _velocityX *= -1;
                _scaleX *= -1;
            }
            else { _moveCounter++; }
            x += _velocityX;
        }
        private void animation()
        {
            _frame += 0.2f;
            if (_frame >= 5.0f) { _frame = 2.0f; }
            if (_frame <= 2.0f) { _frame = 2.0f; }
            SetFrame((int)_frame);
        }
    }
}
