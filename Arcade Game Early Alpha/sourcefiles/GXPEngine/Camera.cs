using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Camera : GameObject
    {
        private Player _objectToFollow;
        private GameObject _map;

        const int LEFTBORDER = 400;
        const int RIGHTBORDER = 624;
        const int TOPBORDER = 200;
        const int BOTTOMBORDER = 568;
        public Camera(Player pObjectToFollow, GameObject pMap)
        {
            _objectToFollow = pObjectToFollow;
            _map = pMap;
        }
        void Update()
        {
            moveGame();
        }
        private void moveGame()
        {
            //Horizontal
            if (_map.x + _objectToFollow.x > RIGHTBORDER)
            {
                _map.x = RIGHTBORDER - _objectToFollow.x;
            }
            if (_map.x + _objectToFollow.x < LEFTBORDER)
            {
                if (_map.x < 0)
                {
                    _map.x = LEFTBORDER - _objectToFollow.x;
                }
            }
            //Vertical
            if (_map.y + _objectToFollow.y > BOTTOMBORDER)
            {
                _map.y = BOTTOMBORDER - _objectToFollow.y;
            }
            if (_map.y + _objectToFollow.y < TOPBORDER)
            {
                _map.y = TOPBORDER - _objectToFollow.y;
            }

        }
    }
}
