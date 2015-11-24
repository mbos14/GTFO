using System;

namespace GXPEngine.Core
{
	public class BoxCollider : Collider
	{
		private Sprite _owner;
		
		//------------------------------------------------------------------------------------------------------------------------
		//														BoxCollider()
		//------------------------------------------------------------------------------------------------------------------------		
		public BoxCollider(Sprite owner) {
			_owner = owner;
		}

        //------------------------------------------------------------------------------------------------------------------------
        //														HitTest()
        //------------------------------------------------------------------------------------------------------------------------		
        public override bool HitTest(Collider other)
        {
            if (other is BoxCollider)
            {
                BoxCollider boxCollider = other as BoxCollider;
                Sprite sprite1 = this._owner;
                Sprite sprite2 = boxCollider._owner;

                float sprite1Left = sprite1.x - sprite1.originX;
                float sprite1Right = sprite1.x + sprite1.width - sprite1.originX;
                float sprite2Left = sprite2.x - sprite2.originX;
                float sprite2Right = sprite2.x + sprite2.width - sprite2.originX;
                if (sprite1Left > sprite2Right) return false;
                if (sprite2Left > sprite1Right) return false;

                float sprite1Top = sprite1.y - sprite1.originY;
                float sprite1Bottom = sprite1.y + sprite1.height - sprite1.originY;
                float sprite2Top = sprite2.y - sprite2.originY;
                float sprite2Bottom = sprite2.y + sprite2.height - sprite2.originY;
                if (sprite1Top > sprite2Bottom) return false;
                if (sprite2Top > sprite1Bottom) return false;

                return true;
            }
            else
            {
                return false;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //														HitTest()
        //------------------------------------------------------------------------------------------------------------------------		
        public override bool HitTestPoint (float x, float y) {
			Vector2[] c = _owner.GetExtents();
			if (c == null) return false;
			Vector2 p = new Vector2(x, y);
			return pointOverlapsArea(p, c);
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														areaOverlap()
		//------------------------------------------------------------------------------------------------------------------------
		private bool areaOverlap(Vector2[] c, Vector2[] d) {
			
			float dx = c[1].x - c[0].x;
			float dy = c[1].y - c[0].y;
			float lengthSQ = (dy * dy + dx * dx);
			
			if (lengthSQ == 0.0f) lengthSQ = 1.0f;
			
			float t, minT, maxT;
			
			t = ((d[0].x - c[0].x) * dx + (d[0].y - c[0].y) * dy) / lengthSQ;
			maxT = t; minT = t;
			
			t = ((d[1].x - c[0].x) * dx + (d[1].y - c[0].y) * dy) / lengthSQ;
			minT = Math.Min(minT,t); maxT = Math.Max(maxT,t);
			
			t = ((d[2].x - c[0].x) * dx + (d[2].y - c[0].y) * dy) / lengthSQ;
			minT = Math.Min(minT,t); maxT = Math.Max(maxT,t);
			
			t = ((d[3].x - c[0].x) * dx + (d[3].y - c[0].y) * dy) / lengthSQ;
			minT = Math.Min(minT,t); maxT = Math.Max(maxT,t);
			
			if ((minT >= 1) || (maxT < 0)) return false;
			
			dx = c[3].x - c[0].x;
			dy = c[3].y - c[0].y;
			lengthSQ = (dy*dy + dx*dx);
			
			if (lengthSQ == 0.0f) lengthSQ = 1.0f;
			
			t = ((d[0].x - c[0].x) * dx + (d[0].y - c[0].y) * dy) / lengthSQ;
			maxT = t; minT = t;
			
			t = ((d[1].x - c[0].x) * dx + (d[1].y - c[0].y) * dy) / lengthSQ;
			minT = Math.Min(minT,t); maxT = Math.Max(maxT,t);
			
			t = ((d[2].x - c[0].x) * dx + (d[2].y - c[0].y) * dy) / lengthSQ;
			minT = Math.Min(minT,t); maxT = Math.Max(maxT,t);
			
			t = ((d[3].x - c[0].x) * dx + (d[3].y - c[0].y) * dy) / lengthSQ;
			minT = Math.Min(minT,t); maxT = Math.Max(maxT,t);
			
			if ((minT >= 1) || (maxT < 0)) return false;
			
			return true;			
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														pointOverlapsArea()
		//------------------------------------------------------------------------------------------------------------------------
		//ie. for hittestpoint and mousedown/up/out/over
		private bool pointOverlapsArea(Vector2 p, Vector2[] c) {
			
			float dx = c[1].x - c[0].x;
			float dy = c[1].y - c[0].y;
			float lengthSQ = (dy * dy + dx * dx);
			
			float t;
			
			t = ((p.x - c[0].x) * dx + (p.y - c[0].y) * dy) / lengthSQ;
			
			if ((t > 1) || (t < 0)) return false;
			
			dx = c[3].x - c[0].x;
			dy = c[3].y - c[0].y;
			lengthSQ = (dy*dy + dx*dx);
			
			t = ((p.x - c[0].x) * dx + (p.y - c[0].y) * dy) / lengthSQ;
			
			if ((t > 1) || (t < 0)) return false;
			
			return true;			
		}	
	}
}


