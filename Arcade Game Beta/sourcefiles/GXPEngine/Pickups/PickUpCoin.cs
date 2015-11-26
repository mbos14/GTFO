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
                SoundChannel soundChannel = new SoundChannel(2);
                Sound pickup = new Sound("coin.wav");
                pickup.Play(false, 2);

                _level.thisgame.playerCoins++;
                _level.player.addPoints(40);
                Destroy();
            }
        }
    }
}
