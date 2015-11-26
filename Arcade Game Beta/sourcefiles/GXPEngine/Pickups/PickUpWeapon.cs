using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpWeapon : PickUp
    {
        private Level _level;
        public PickUpWeapon(Level pLevel) : base("weaponset.png", 1, 1)
        {
            _level = pLevel;
        }
        void Update()
        {
            getCollisionPlayer();
        }
        private void getCollisionPlayer()
        {
            if (HitTest(_level.player))
            {
                this.Destroy();
                _level.player.hasWeapon = true;
                _level.player.addPoints(50);
            }
        }

    }
}
