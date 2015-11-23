using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    //GAME
    public enum GameStates { menu, level, bossarena, nameinput, endscreen, @default };

    //ENEMY
    public enum AnimationStateEnemy { idle, walk, hit, death, jump };
    public enum EnemyDirection { left, right, down };
    public enum EnemyPoints {floater = 100, spider = 25, bug = 50};
    public enum EnemyHealth { floater = 150, spider = 50, bug = 75};

    //ENEMYFRAMES
    //SPIDER
    public enum SpiderIdle { firstFrame = 0, lastFrame = 1 };
    public enum SpiderWalk { firstFrame = 2, lastFrame = 6 };
    public enum SpiderHit { firstFrame = 8, lastFrame = 9 };
    public enum SpiderDeath { firstFrame = 10, lastFrame = 11 };
    public enum SpiderJump { firstFrame = 7, lastFrame = 9 };
    //FLOATER
    public enum FLoaterIdle { firstFrame = 0, lastFrame = 0 };
    public enum FLoaterWalk { firstFrame = 1, lastFrame = 3 };
    public enum FLoaterHit { firstFrame = 4, lastFrame = 8 };
    public enum FLoaterDeath { firstFrame = 9, lastFrame = 11 };
    public enum FLoaterJump { firstFrame = 4, lastFrame = 8 };
    //BUG
    public enum BugIdle { firstFrame = 3, lastFrame = 3 };
    public enum BugWalk { firstFrame = 0, lastFrame = 2 };
    //public enum BugHit { firstFrame = 8, lastFrame = 9 };
    //public enum BugDeath { firstFrame = 10, lastFrame = 11 };


    //PLAYER
    public enum PlayerDirection { left, right, up, down };
    public enum AnimationStatePlayer { idle, walk, jump, recoil };
    public enum PlayerButtons { left = Key.A, right = Key.D, up = Key.W, down = Key.S, shoot = Key.F, insert = Key.Z };
    public enum ChargeWeapon { p0, p10, p20, p30, p40, p50, p60, p70, p80, p90, p100 }
}