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
            if (HitTest(_level._player))
            {
                this.Destroy();
                _level._player.addPoints(20);
            }
        }
    }
}
