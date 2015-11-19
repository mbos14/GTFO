using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpLife : PickUp
    {
        public PickUpLife() : base("lifeupset.png", 4, 1)
        {

        }
        void Update()
        {
            animation();
        }
    }
}
