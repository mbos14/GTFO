using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        private Level001 _level;
        public Enemy(string pFileName, int pColumns, int pRows, Level001 pLevel) : base(pFileName, pColumns, pRows)
        {
            _level = pLevel;
        }

    }
}
