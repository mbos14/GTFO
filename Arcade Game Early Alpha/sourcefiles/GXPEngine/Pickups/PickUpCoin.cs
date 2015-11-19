using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpCoin : PickUp
    {
        public PickUpCoin() : base("coinset.png", 4, 1)
        {

        }
        void Update()
        {
            animation();
        }
    }
}
