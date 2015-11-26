using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    public class SolidObject : AnimationSprite
    {
        private Level _level;
        public SolidObject(Level pLevel) : base("tileset.png", 32, 66)
        {

            _level = pLevel;
        }
    }
}
