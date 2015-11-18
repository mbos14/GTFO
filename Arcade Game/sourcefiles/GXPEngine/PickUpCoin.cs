﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpCoin : PickUp
    {
        private float _frame = 0.0f;
        public PickUpCoin() : base("coinset.png", 4, 1)
        {

        }
        void Update()
        {
            animation();
        }
        private void animation()
        {
            _frame += 0.2f;
            if (_frame >= frameCount) { _frame = 0.0f; }
            SetFrame((int)_frame);
        }
    }
}
