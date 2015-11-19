using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    public class BackgroundObject : AnimationSprite
    {
        public BackgroundObject() : base("tileset.png", 14, 19)
        {

        }
        //Delete collision with this kind of object
        protected override Collider createCollider()
        {
            return null;
        }
    }
}
