namespace GXPEngine
{
    class EnemyFloater : Enemy
    {
        public EnemyFloater(Level pLevel) : base("robofloater.png", 4, 3, pLevel, EnemyPoints.floater, EnemyHealth.floater)
        {
            SetOrigin(width / 2, height / 2 - 10);
        }
        void Update()
        {      
            //related to states
            StateSwitch();
            playerDistance();
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
            if (_state == EnemyState.death || _state == EnemyState.hit) return;
            //turn of when distance is to big
            if (DistanceTo(_level.player) > 500)
            {
                _state = EnemyState.idle;
            }//turn on if distance to player is to close
            else if (DistanceTo(_level.player) <= 500)
            {
                _state = EnemyState.walk;
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
    }
}
