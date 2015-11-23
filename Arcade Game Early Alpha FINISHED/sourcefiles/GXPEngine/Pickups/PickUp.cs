using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class PickUp : AnimationSprite
    {
        protected float _frame = 0.0f;
        public PickUp(string pFileName, int pColumns, int pRows) : base(pFileName, pColumns, pRows)
        {

        }
        virtual public void animation()
        {
            _frame += 0.1f;
            if (_frame >= frameCount) { _frame = 0.0f; }
            SetFrame((int)_frame);
        }
    }
}
