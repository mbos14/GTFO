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
            if (HitTest(_level.player))
            {
                SoundChannel soundChannel = new SoundChannel(2);
                Sound pickup = new Sound("lifeup.wav");
                pickup.Play(false, 2);

                this.Destroy();
                _level.thisgame.playerLives++;
                _level.player.addPoints(20);
            }
        }
    }
}
