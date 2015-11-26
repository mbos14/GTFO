﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpReload : PickUp
    {
        private Level _level;
        public PickUpReload(Level pLevel) : base("reloadset.png", 4, 1)
        {
            _level = pLevel;
        }
        void Update()
        {
            animation();
            getCollisionPlayer();
        }
        private void getCollisionPlayer()
        {
            if (HitTest(_level.player) && _level.player.bulletCounter < 3f)
            {               
                _level.player.bulletCounter = 3f;
                this.Destroy();
            }
        }

    }
}
