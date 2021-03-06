﻿namespace GXPEngine
{
    //GAME
    public enum GameStates { menu, part1, part2, part3, wonlostscreen, nameinput, endscreen, @default };

    //ENEMY
    public enum EnemyState { idle, walk, hit, death, jump };
    public enum EnemyDirection { left, right, down, up };
    public enum EnemyPoints {floater = 100, spider = 25, bug = 50};
    public enum EnemyHealth { floater = 150, spider = 50, bug = 75};

    //ENEMYFRAMES
    //SPIDER
    public enum SpiderIdle { firstFrame = 0, lastFrame = 1 };
    public enum SpiderWalk { firstFrame = 2, lastFrame = 5 };
    public enum SpiderHit { firstFrame = 6, lastFrame = 9 };
    public enum SpiderDeath { firstFrame = 10, lastFrame = 11 };
    //FLOATER
    public enum FLoaterIdle { firstFrame = 0, lastFrame = 0 };
    public enum FLoaterWalk { firstFrame = 1, lastFrame = 3 };
    public enum FLoaterHit { firstFrame = 4, lastFrame = 8 };
    public enum FLoaterDeath { firstFrame = 9, lastFrame = 11 };
    //BUG
    public enum BugIdle { firstFrame = 2, lastFrame = 3 };
    public enum BugWalk { firstFrame = 0, lastFrame = 2 };
    public enum BugHit { firstFrame = 2, lastFrame = 3 };
    public enum BugDeath { firstFrame = 4, lastFrame = 5 };


    //PLAYER
    public enum PlayerDirection { left, right, up, down };
    public enum AnimationStatePlayer { idle, walk, jump, shoot };
    public enum PlayerButtons { left = Key.A, right = Key.D, up = Key.W, down = Key.S, shoot = Key.F, insert = Key.Z };
}