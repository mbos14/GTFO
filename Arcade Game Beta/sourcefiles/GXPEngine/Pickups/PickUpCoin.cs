using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpCoin : PickUp
    {
        private Level _level;
        public PickUpCoin(Level pLevel) : base("coinset.png", 4, 1)
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
            if (HitTest(_level.player))
            {
                _level.player.coins++;
                _level.player.addPoints(20);
                Destroy();
            }
        }
    }
}
