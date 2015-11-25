namespace GXPEngine
{
    class EnemyFloater : Enemy
    {
        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel)
        {
            SetOrigin(width / 2, height / 2 - 10);
            _points = EnemyPoints.floater;
            _healthmax = EnemyHealth.floater;
            _health = (float)_healthmax;
        }
        void Update()
        {
            //related to movement
            Move();
            recoil();
            //related to animation
            animation();
            //related to states
            StateSwitch();
            playerDistance();
            AnimationState();
        }
        
        private void playerDistance()
        {
            if (DistanceTo(_level.player) > 500)
            {
                _state= EnemyState.idle;
            }
            else if (DistanceTo(_level.player) <= 500)
            {
                _state = EnemyState.walk;
            }
        }
        //chanching the animation state
        private void AnimationState()
        {
            switch(_state)
            {
                case EnemyState.idle:
                    setAnimationRange((float)FLoaterIdle.firstFrame,(float)FLoaterIdle.lastFrame);
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
        public override void recoil()
        {
            if (!isHit) return;
            if (_isDeath) return;

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
            else
            {
                isHit = false;
                frameCounter = 0;
            }
        }
    }
}
