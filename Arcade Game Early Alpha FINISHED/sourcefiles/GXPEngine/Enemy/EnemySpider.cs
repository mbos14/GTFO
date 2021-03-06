﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EnemySpider : Enemy
    {
        private int _moveCounter = 0;
        private float _velocityX = 1.0f;

        //Animation
        private float _frame = 0.0f;
        //private float _firstFrame;
        //private float _lastFrame;

        //private int _animState;

        public EnemySpider(Level pLevel) : base("robospider.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2);
            scaleX *= -1;
        }
        void Update()
        {
            animation();
            Move();
        }
        protected override void Move()
        {
            x += _velocityX; //Move
        }
        //Turn around
        public override void TurnAround()
        {
            _velocityX *= -1; //Move the other way
            scaleX *= -1; //Rescale
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
