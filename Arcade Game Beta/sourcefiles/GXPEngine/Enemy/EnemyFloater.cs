namespace GXPEngine
{
    class EnemyFloater : Enemy
    {
        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel, EnemyPoints.floater, EnemyHealth.floater,EnemyDirection.left, false)
        {
            SetOrigin(width / 2, height / 2 - 10);
        }
        void Update()
        {      
            //related to states
            StateSwitch();            
            AnimationState();            
        }
        //ANIMATION
        //chanching the animation range
        private void AnimationState()
        {
            switch (_state)
            {
                case EnemyState.idle:
                    setAnimationRange((float)FLoaterIdle.firstFrame, (float)FLoaterIdle.lastFrame);
                    break;
                case EnemyState.walk:
                    setAnimationRange((float)FLoaterWalk.firstFrame, (float)FLoaterWalk.lastFrame);
                    break;
                case EnemyState.hit:
                    setAnimationRange((float)FLoaterHit.firstFrame, (float)FLoaterHit.lastFrame);
                    break;
                case EnemyState.death:
                    setAnimationRange((float)FLoaterDeath.firstFrame, (float)FLoaterDeath.lastFrame);
                    break;
            }
        }

        //LOGIC
        //checks distance to player
        private void playerDistance()
        {
            //turn of when distance is to big
            if (DistanceTo(_level.player) > 500)
            {
                _state = EnemyState.idle;
                _idleTimer = 0f;
                if (_enemyDirection == EnemyDirection.down) { Mirror(true, false); }
            }//turn on if distance to player is to close
            else if (DistanceTo(_level.player) <= 500)
            {
                _state = EnemyState.walk;
            }
        }
        //uses states to switch between wich could should be used.
        protected override void StateSwitch()
        {
            switch (_state)
            {
                case EnemyState.idle:
                    playerDistance();
                    WalkingIdleAnimation();
                    _idleTimer += 0.1f;
                    if (_idleTimer >= 2f)
                    {
                        _idleTimer = 0f;
                        _state = EnemyState.walk;
                    }
                    break;
                case EnemyState.walk:
                    {
                        playerDistance();
                        Move();
                        WalkingIdleAnimation();
                        break;
                    }
                case EnemyState.hit:
                    recoil();
                    DeathHitAnimation();
                    _hitTimer -= 0.5f;
                    if (_hitTimer <= 0f)
                    {
                        _hitTimer = 0f;
                        _state = EnemyState.idle;
                    }
                    break;
                case EnemyState.death:
                    DeathHitAnimation();
                    break;
            }
        }

        //MOVEMENT
        //if hit get pushed back in the opisite direction
        public override void recoil()
        {
            frameCounter++;

            if (frameCounter < 25)
            {
                switch (directionHit)
                {
                    case PlayerDirection.left:
                        {
                            x -= 2;
                            break;
                        }
                    case PlayerDirection.right:
                        {
                            x += 2;
                            break;
                        }
                }
            }

            if (frameCounter >= 25)
            {
                frameCounter = 0;
            }
        }
        //turns the enemy around
        public override void TurnAround()
        {
            x -= _velocityX;
            _velocityX *= -1;

            if (_enemyDirection == EnemyDirection.left)
            {
                Mirror(true, false);
                _enemyDirection = EnemyDirection.right;
            }
            else
            {
                Mirror(false, false);
                _enemyDirection = EnemyDirection.left;
            }
        }
    }
}
