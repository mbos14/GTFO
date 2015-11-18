using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        //Game data
        private Level001 _level;

        //Player
        private float _curFrame = 0.0f;
        private string _aimDirection = "right";
        public float spawnX;
        public float spawnY;
        private bool _hasWeapon = false;

        //Keys
        const int LEFT = Key.LEFT;
        const int RIGHT = Key.RIGHT;
        const int UP = Key.UP;
        const int DOWN = Key.DOWN;
        const int SHOOT = Key.LEFT_CTRL;

        //Speed
        private float _velocityX = 0.0f;
        private float _velocityY = 0.0f;
        private float _walkSpeed = 5.0f;
        private float _jumpSpeed = -15.0f;
        private float maxVelocityY = 20.0f;
        private float _pushBackSpeed = 4.0f;
        private float _gravity = 0.5f;
        private float _bounce = -0.5f;

        //Animationstate
        private int _currentAnimState = 0;
        private float _lastFrame;
        private float _firstFrame;

        const int WALKING = 0;
        const int IDLING = 1;
        const int JUMPING = 2;

        //Air check
        private bool _inAir;

        //Weapon
        private float _maxBullets = 2;
        private float _bulletCounter = 2;
        private float _bulletCharge = 0.015f;

        public Player(Level001 pLevel) : base("player.png", 4, 4)
        {
            SetOrigin(width / 2, height);
            _level = pLevel;
        }
        void Update()
        {
            animation();
            checkShooting();
            movePlayer();
            chargeWeapon();
        }
        private void respawn()
        {
            x = spawnX;
            y = spawnY;
        }
        //-------------MOVEMENT-----------------
        private void checkShooting()
        {
            getInput();

            //Apply aimdirection
            switch (_aimDirection)
            {
                case "left":
                    {
                        //TODO Animation => left
                        if (Input.GetKeyDown(SHOOT) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            createBullet("left");
                            _bulletCounter -= 1.0f;
                            if (_inAir)
                            {
                                _velocityX = _pushBackSpeed * 3;
                            }
                            else if (!_inAir)
                            {
                                _velocityX = _pushBackSpeed;
                            }
                        }

                        break;
                    }
                case "right":
                    {
                        //TODO Animation => aim right
                        if (Input.GetKeyDown(SHOOT) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            createBullet("right");
                            _bulletCounter -= 1.0f;
                            if (_inAir)
                            {
                                _velocityX = -_pushBackSpeed * 3;
                            }
                            else if (!_inAir)
                            {
                                _velocityX = -_pushBackSpeed;
                            }
                        }
                        break;
                    }
                case "up":
                    {
                        //TODO Animation => aim up
                        if (Input.GetKeyDown(SHOOT) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            createBullet("up");                            
                            _bulletCounter -= 1.0f;
                            if (_inAir)
                            {
                                _velocityY = -_jumpSpeed;

                            }
                        }
                        break;
                    }
                case "down":
                    {
                        //TODO Animation => aim down
                        if (Input.GetKeyDown(SHOOT) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            _inAir = true;

                            createBullet("down");
                            _bulletCounter -= 1.0f;
                            _velocityY = _jumpSpeed;
                        }
                        break;
                    }
            }
        }
        private void getInput()
        {
            //Horizontal aiming/movement
            if (Input.GetKey(LEFT))
            {
                Mirror(true, false);
                if (!_inAir)
                {
                    _velocityX = -_walkSpeed;
                    _currentAnimState = WALKING;
                }

                _aimDirection = "left";
            }
            else if (Input.GetKey(RIGHT))
            {
                Mirror(false, false);
                if (!_inAir)
                {
                    _velocityX = _walkSpeed;
                    _currentAnimState = WALKING;
                }
                _aimDirection = "right";
            }
            //Vertical aiming
            else if (Input.GetKey(UP)) { _aimDirection = "up"; }
            else if (Input.GetKey(DOWN)) { _aimDirection = "down"; }

            //No horizontal movement
            if (!Input.GetKey(LEFT) && !Input.GetKey(RIGHT))
            {
                if (!_inAir)
                {
                    _velocityX = 0.0f;
                }
            }

            if (_velocityY > maxVelocityY) { _velocityY = maxVelocityY; }
            if (_velocityX == 0.0f) { _currentAnimState = IDLING; }
            if (_inAir == true) { _currentAnimState = JUMPING; }
        }
        private void movePlayer()
        {
            //Horizontal movement
            x += _velocityX;
            //Move outside the block if collision
            while (checkCollisions())
            {
                if (_velocityX > 0) { x -= 1.0f; }
                else if (_velocityX < 0) { x += 1.0f; }
            }

            //Vertical Movement
            _velocityY += _gravity;
            y += _velocityY;
            //If collision
            if (checkCollisions())
            {
                //Move outside the block
                while (checkCollisions())
                {
                    if (_velocityY > 0) { y -= 1.0f; }
                    else if (_velocityY < 0) { y += 1.0f; }
                }

                //If falling down
                if (_velocityY > 0) { _velocityY = 0.0f; _inAir = false; }
                //If jumping up
                else if (_velocityY < 0) { _velocityY *= _bounce; }
            }

        }
        private bool checkCollisions()
        {
            foreach (Sprite other in _level.objectList)
            {
                if (this.HitTest(other))
                {
                    //Blocks
                    if (other is DamageBlock)
                    {
                        respawn();
                    }
                    else if (other is SolidObject)
                    {
                        return true;
                    }
                    //Pickups
                    else if (other is PickUpWeapon)
                    {
                        other.Destroy();
                        _hasWeapon = true;
                    }
                    else if (other is PickUpCoin)
                    {
                        other.Destroy();
                        addPoints();
                    }
                    else if (other is PickUpLife)
                    {
                        other.Destroy();
                        addLife();
                    }
                    else if (other is PickUpReload)
                    {
                        if (!(_bulletCounter >= _maxBullets))
                        {
                            other.Destroy();
                            _bulletCounter = _maxBullets;
                        }
                    }
                }
            }
            return false;
        }
        //------------PICKUPS-------------------
        private void addLife()
        {

        }
        private void addPoints()
        {

        }
        //------------ANIMATION-----------------
        private void animation()
        {
            animationState();

            _curFrame += 0.2f;

            if (_curFrame > _lastFrame) { _curFrame = _firstFrame; }
            else if (_curFrame < _firstFrame) { _curFrame = _lastFrame; }

            this.SetFrame((int)_curFrame);
        }
        private void setAnimationRange(float pFirstFrame, float pLastFrame)
        {
            _firstFrame = pFirstFrame;
            _lastFrame = pLastFrame;
        }
        private void animationState()
        {
            switch (_currentAnimState)
            {
                case IDLING:
                    {
                        if (!_hasWeapon)
                        {
                            setAnimationRange(0, 2);
                        }
                        else if (_hasWeapon)
                        {
                            setAnimationRange(8, 10);
                        }
                        break;
                    }
                case WALKING:
                    {
                        if (!_hasWeapon)
                        {
                            setAnimationRange(3, 7);
                        }
                        else if (_hasWeapon)
                        {
                            setAnimationRange(11, 15);
                        }
                        break;
                    }
                case JUMPING:
                    {
                        if (!_hasWeapon)
                        {
                            //Set anim range
                        }
                        else if (_hasWeapon)
                        {
                            //Set anim range
                        }
                        break;
                    }
            }
        }
        //------------WEAPON--------------------
        private void chargeWeapon()
        {
            if (_bulletCounter < _maxBullets)
            {
                _bulletCounter += _bulletCharge;
            }
            else if (_bulletCounter > _maxBullets)
            {
                _bulletCounter = _maxBullets;
            }
        }
        private void createBullet(string pDirection)
        {
            PlayerBullet bullet = new PlayerBullet(pDirection);
            _level.AddChild(bullet);
            bullet.SetXY(x, y - (height / 2));
        }
    }
}
