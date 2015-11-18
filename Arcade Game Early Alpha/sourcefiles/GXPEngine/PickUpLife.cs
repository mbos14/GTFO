using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUpLife : PickUp
    {
        private float _frame = 0.0f;
        public PickUpLife() : base("lifeupset.png", 4, 1)
        {

        }
        void Update()
        {
            animation();
        }
        private void animation()
        {
            _frame += 0.3f;
            if (_frame == frameCount) { _frame = 0.0f; }
            SetFrame((int)_frame);
        }
    }
}
