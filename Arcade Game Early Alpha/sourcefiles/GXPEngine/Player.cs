using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        //Game data
        private Level _level;

        //Player
        private float _curFrame = 0.0f;
        private PlayerDirection _aimDirection = PlayerDirection.right;
        public float spawnX;
        public float spawnY;
        private bool _hasWeapon = false;

        //Keys


        //Speed
        private float _velocityX = 0.0f;
        private float _velocityY = 0.0f;
        private float _walkSpeed = 5.0f;
        private float _jumpSpeed = -15.0f;
        private float maxVelocityY = 20.0f;
        private float _pushBackSpeed = 3.0f;
        private float _gravity = 0.65f;
        private float _bounce = -0.25f;
        private bool _recoil = false;
        private int _recoilFrames = 0;
        private int _maxrecoilFrames = 150;

        //Animationstate
        private AnimationStatePlayer _currentAnimState = 0;
        private float _animSpeed = 0.1f;
        private float _lastFrame;
        private float _firstFrame;

        //Air check
        private bool _inAir;

        //Weapon
        private float _maxBullets = 2;
        private float _bulletCounter = 2;
        private float _bulletCharge = 0.03f;

        public Player(Level pLevel) : base("player.png", 4, 9)
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
            recoilCounter();
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
                case PlayerDirection.left:
                    {
                        //TODO Animation => left
                        if (Input.GetKeyDown((int)PlayerButtons.shoot) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            createBullet(PlayerDirection.left);
                            _bulletCounter -= 1.0f;
                            if (_inAir)
                            {
                                _velocityX = _pushBackSpeed * 3;
                            }
                            else if (!_inAir)
                            {
                                _recoil = true;
                                _velocityX = _pushBackSpeed * 3;
                            }
                        }

                        break;
                    }
                case PlayerDirection.right:
                    {
                        //TODO Animation => aim right
                        if (Input.GetKeyDown((int)PlayerButtons.shoot) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            createBullet(PlayerDirection.right);
                            _bulletCounter -= 1.0f;
                            if (_inAir)
                            {
                                _velocityX = -_pushBackSpeed * 3;
                            }
                            else if (!_inAir)
                            {
                                _recoil = true;
                                _velocityX = -_pushBackSpeed * 3;
                            }
                        }
                        break;
                    }
                case PlayerDirection.up:
                    {
                        //TODO Animation => aim up
                        if (Input.GetKeyDown((int)PlayerButtons.shoot) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            createBullet(PlayerDirection.up);
                            _bulletCounter -= 1.0f;
                            if (_inAir)
                            {
                                _velocityY = -_jumpSpeed;

                            }
                        }
                        break;
                    }
                case PlayerDirection.down:
                    {
                        //TODO Animation => aim down
                        if (Input.GetKeyDown((int)PlayerButtons.shoot) && _bulletCounter >= 1 && _hasWeapon)
                        {
                            _inAir = true;

                            createBullet(PlayerDirection.down);
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
            if (Input.GetKey((int)PlayerButtons.left))
            {
                Mirror(true, false);
                if (!_inAir)
                {
                    _velocityX = -_walkSpeed;
                    _currentAnimState = AnimationStatePlayer.walk;
                }

                _aimDirection = PlayerDirection.left;
            }
            else if (Input.GetKey((int)PlayerButtons.right))
            {
                Mirror(false, false);
                if (!_inAir)
                {
                    _velocityX = _walkSpeed;
                    _currentAnimState = AnimationStatePlayer.walk;
                }
                _aimDirection = PlayerDirection.right;
            }
            //Vertical aiming
            else if (Input.GetKey((int)PlayerButtons.up)) { _aimDirection = PlayerDirection.up; }
            else if (Input.GetKey((int)PlayerButtons.down)) { _aimDirection = PlayerDirection.down; }

            //No horizontal movement
            if (_recoil)
            {
                if (_velocityX > 0)
                {
                    _velocityX -= 0.5f;
                }
                else if (_velocityX < 0)
                {
                    _velocityX += 0.5f;
                }
            }
            else if (!Input.GetKey((int)PlayerButtons.left) && !Input.GetKey((int)PlayerButtons.right))
            {
                if (!_inAir)
                {
                    _velocityX = 0.0f;
                }
            }

            if (_velocityY > maxVelocityY) { _velocityY = maxVelocityY; }
            if (_velocityX == 0.0f) { _currentAnimState = AnimationStatePlayer.idle; }
            if (_inAir == true) { _currentAnimState = AnimationStatePlayer.jump; }
        }
        private void movePlayer()
        {
            //Horizontal movement
            x += _velocityX;
            //Move outside the block if collision

            if (checkCollisions())
            {
                while (checkCollisions())
                {
                    if (_velocityX > 0) { x -= 1.0f; }
                    else if (_velocityX < 0) { x += 1.0f; }
                }
                if (_velocityX > 3.0f || _velocityX < -3.0f)
                {
                    _velocityX *= _bounce;
                }
                else
                {
                    _velocityX = 0.0f;
                }
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
        private void recoilCounter()
        {
            if (!_recoil) { _recoilFrames = 0; }
            else if (_recoil)
            {
                _recoilFrames++;
            }

            if (_recoilFrames > _maxrecoilFrames)
            {
                _recoil = false;
                _recoilFrames = 0;
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
                        //addPoints();
                    }
                    else if (other is PickUpLife)
                    {
                        other.Destroy();
                        //addLife();
                    }
                    else if (other is PickUpReload)
                    {
                        if (!(_bulletCounter >= _maxBullets))
                        {
                            other.Destroy();
                            _bulletCounter = _maxBullets;
                        }
                    }
                    //Enemys
                    else if (other is Enemy)
                    {
                        respawn();
                    }
                }
            }
            return false;
        }
        ////------------PICKUPS-------------------
        //private void addLife()
        //{

        //}
        //private void addPoints()
        //{

        //}
        //------------ANIMATION-----------------
        private void animation()
        {
            animationState();

            _curFrame += _animSpeed;

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
                case AnimationStatePlayer.idle:
                    {
                        if (!_hasWeapon)
                        {
                            setAnimationRange(0, 2);
                        }
                        else if (_hasWeapon)
                        {
                            if (_aimDirection == PlayerDirection.left) { setAnimationRange(8, 10); }
                            else if (_aimDirection == PlayerDirection.right) { setAnimationRange(8, 10); }
                            else if (_aimDirection == PlayerDirection.up) { setAnimationRange(16, 18); }
                            else if (_aimDirection == PlayerDirection.down) { setAnimationRange(24, 26); }
                        }
                        break;
                    }
                case AnimationStatePlayer.walk:
                    {
                        if (!_hasWeapon)
                        {
                            setAnimationRange(3, 7);
                        }
                        else if (_hasWeapon)
                        {
                            if (_aimDirection == PlayerDirection.left) { setAnimationRange(11, 15); }
                            else if (_aimDirection == PlayerDirection.right) { setAnimationRange(11, 15); }
                            else if (_aimDirection == PlayerDirection.up) { setAnimationRange(19, 23); }
                            else if (_aimDirection == PlayerDirection.down) { setAnimationRange(27, 31); }
                        }
                        break;
                    }
                case AnimationStatePlayer.jump:
                    {
                        if (_aimDirection == PlayerDirection.left) { setAnimationRange(32, 32); }
                        else if (_aimDirection == PlayerDirection.right) { setAnimationRange(32, 32); }
                        else if (_aimDirection == PlayerDirection.up) { setAnimationRange(34, 34); }
                        else if (_aimDirection == PlayerDirection.down) { setAnimationRange(33, 33); }
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
        private void createBullet(PlayerDirection pDirection)
        {
            PlayerBullet bullet = new PlayerBullet(pDirection, _level);
            _level.AddChild(bullet);
            bullet.SetXY(x, y - (height / 2));
        }
    }
}
