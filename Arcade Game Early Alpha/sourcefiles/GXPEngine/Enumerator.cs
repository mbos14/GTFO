using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public enum GameStates { menu, endscreen, level1, highscore }
    public enum AnimationStateSpider {idle, walk, hit, death};
    public enum PlayerDirection { left, right, up, down}
    public enum AnimationStatePlayer { idle, walk, jump}
    public enum PlayerButtons { left = Key.LEFT, right = Key.RIGHT, up = Key.UP, down = Key.DOWN, shoot = Key.LEFT_CTRL }
}