using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpReload : PickUp
    {
        public PickUpReload() : base("reloadset.png", 4, 1)
        {

        }
        void Update()
        {
            animation();
        }
    }
}
