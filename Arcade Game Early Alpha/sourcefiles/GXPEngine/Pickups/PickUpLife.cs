using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpLife : PickUp
    {
        private Level _level;
        public PickUpLife(Level pLevel) : base("lifeupset.png", 4, 1)
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
                _level._player.lives++;
            }
        }
    }
}
