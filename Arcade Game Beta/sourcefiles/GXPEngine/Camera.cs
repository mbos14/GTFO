using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Camera : GameObject
    {
        private Player _objectToFollow;
        private Level _map;

        const int LEFTBORDER = 400;
        const int RIGHTBORDER = 624;
        const int TOPBORDER = 200;
        const int BOTTOMBORDER = 568;
        public Camera(Player pObjectToFollow, Level pMap)
        {
            _objectToFollow = pObjectToFollow;
            _map = pMap;
        }
        void Update()
        {
            moveGame();
        }
        protected override Collider createCollider()
        {
            return null;
        }
        private void moveGame()
        {
            //Horizontal
            if (_map.x + _objectToFollow.x > RIGHTBORDER)
            {
                _map.x = RIGHTBORDER - _objectToFollow.x;
                _map.hudLayer.x = -_map.x;
            }
            if (_map.x + _objectToFollow.x < LEFTBORDER)
            {
                if (_map.x < 0)
                {
                    _map.x = LEFTBORDER - _objectToFollow.x;
                    _map.hudLayer.x = -_map.x;
                }
            }
            //Vertical
            if (_map.y + _objectToFollow.y > BOTTOMBORDER)
            {
                _map.y = BOTTOMBORDER - _objectToFollow.y;
                _map.hudLayer.y = -_map.y;
            }
            if (_map.y + _objectToFollow.y < TOPBORDER)
            {
                _map.y = TOPBORDER - _objectToFollow.y;
                _map.hudLayer.y = -_map.y;
            }

        }
    }
}
