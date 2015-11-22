using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        //Game data
        private Level _level;

        //Player
        private float _frame = 0.0f;
        private PlayerDirection _aimDirection = PlayerDirection.right;
        public float spawnX;
        public float spawnY;
        public bool hasWeapon;
        public int lives = 1;

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

        //Animation
        private AnimationStatePlayer _currentAnimState = 0;
        private float _animSpeed = 0.1f;
        private float _lastFrame;
        private float _firstFrame;

        //Air check
        private bool _inAir;

        //Weapon
        public float maxBullets = 2;
        public float bulletCounter = 2;
        private float _bulletCharge = 0.03f;

        private bool _button1, _button2, _button3;

        public Player(Level pLevel) : base("player.png", 4, 10)
        {
            if (MyGame.playerHasWeapon) { hasWeapon = true; }

            SetOrigin(width / 2, height);
            _level = pLevel;
        }
        void Update()
        {
            animation();
            movePlayer();
            chargeWeapon();
            recoilCounter();
            secretCheat();
        }

        private void secretCheat()
        {
            //Button 1
            if (Input.GetKeyDown(Key.V))
            {
                _button1 = true;
            }

            //Button 2
            if (_button1 && Input.GetKeyDown(Key.B))
            {
                _button2 = true;
            }

            //Button 3
            if (_button2 && Input.GetKeyDown(Key.H))
            {
                _button3 = true;
            }

            if (_button1 && _button2 && _button3)
            {
                _bulletCharge = 100;
            }
        }
        //-------------MOVEMENT-----------------
        private void getInput()
        {
            //------------------LEFT------------------
            if (Input.GetKey((int)PlayerButtons.left))
            {
                _aimDirection = PlayerDirection.left;
                if (!_inAir && !_recoil)
                {
                    //Set directions
                    _currentAnimState = AnimationStatePlayer.walk;
                    //Move
                    _velocityX = -_walkSpeed;
                }
                Mirror(true, false);

            }
            //------------------RIGHT-----------------
            else if (Input.GetKey((int)PlayerButtons.right))
            {
                _aimDirection = PlayerDirection.right;
                if (!_inAir && !_recoil)
                {
                    //Set directions
                    _currentAnimState = AnimationStatePlayer.walk;
                    //Move
                    _velocityX = _walkSpeed;
                }
                Mirror(false, false);
            }

            //-------------------UP-------------------
            else if (Input.GetKey((int)PlayerButtons.up) && !_recoil)
            {
                //Set directions
                _aimDirection = PlayerDirection.up;
            }
            //------------------DOWN------------------
            else if (Input.GetKey((int)PlayerButtons.down) && !_recoil)
            {
                //Set directions
                _aimDirection = PlayerDirection.down;
            }
            //----------------NO BUTTONS--------------
            if (_recoil)
            {
                _currentAnimState = AnimationStatePlayer.recoil;
                if (_velocityX > 0) { _velocityX -= 0.5f; }
                else if (_velocityX < 0) { _velocityX += 0.5f; }
            }
            else if (!Input.GetKey((int)PlayerButtons.left) && !Input.GetKey((int)PlayerButtons.right))
            {
                if (!_inAir) { _velocityX = 0.0f; }
            }
            //------------------SHOOT-----------------
            if (Input.GetKeyDown((int)PlayerButtons.shoot) && hasWeapon && bulletCounter >= 1)
            {
                shootBullet();
                bulletCounter -= 1.0f;
            }

            //Keep velocity at his max
            if (_velocityY > maxVelocityY) { _velocityY = maxVelocityY; }
            //Update animstate
            if (_velocityX == 0.0f) { _currentAnimState = AnimationStatePlayer.idle; }
            if (_inAir == true) { _currentAnimState = AnimationStatePlayer.jump; }
        }
        private void movePlayer()
        {
            //Update input
            getInput();
            //Horizontal movement
            x += _velocityX;
            //Move outside the block if collision
            if (_level.CheckCollision(this))
            {
                _recoil = false;
                while (_level.CheckCollision(this))
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
            if (_level.CheckCollision(this))
            {
                //Move outside the block
                while (_level.CheckCollision(this))
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
                if (_velocityX == 0) { _recoil = false; }
            }

            if (_recoilFrames > _maxrecoilFrames)
            {
                _recoil = false;
                _recoilFrames = 0;
            }
        }
        ////------------PICKUPS-------------------
        //private void addLife()
        //{

        //}

        //------------ANIMATION-----------------
        private void animation()
        {
            animationState();

            _frame += _animSpeed;

            if (_frame > _lastFrame) { _frame = _firstFrame; }
            else if (_frame < _firstFrame) { _frame = _lastFrame; }

            SetFrame((int)_frame);
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
                        if (!hasWeapon)
                        {
                            setAnimationRange(0, 2);
                        }
                        else if (hasWeapon)
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
                        if (!hasWeapon)
                        {
                            setAnimationRange(3, 7);
                        }
                        else if (hasWeapon)
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
                case AnimationStatePlayer.recoil:
                    {
                        setAnimationRange(36, 36);
                        break;
                    }
            }
        }
        //------------WEAPON--------------------
        private void chargeWeapon()
        {
            if (bulletCounter < maxBullets)
            {
                bulletCounter += _bulletCharge;
            }
            else if (bulletCounter > maxBullets)
            {
                bulletCounter = maxBullets;
            }
        }
        private void shootBullet()
        {
            if (_aimDirection == PlayerDirection.up)
            {
                recoil(_aimDirection);
            }
            else if (_aimDirection == PlayerDirection.down)
            {
                recoil(_aimDirection);
            }
            else if (_aimDirection == PlayerDirection.left)
            {
                recoil(_aimDirection);
            }
            else if (_aimDirection == PlayerDirection.right)
            {
                recoil(_aimDirection);
            }

            PlayerBullet bullet = new PlayerBullet(_aimDirection, _level);
            _level.AddChild(bullet);
            bullet.SetXY(x, y - (height / 2));
        }
        private void recoil(PlayerDirection pDirection)
        {
            //Jump
            if (pDirection == PlayerDirection.down)
            {
                _velocityY = _jumpSpeed;
                _inAir = true;
            }

            //Recoil
            if (_inAir)
            {
                if (pDirection == PlayerDirection.left) { _velocityX = _pushBackSpeed * 3; }
                else if (pDirection == PlayerDirection.right) { _velocityX = -_pushBackSpeed * 3; }
                else if (pDirection == PlayerDirection.up) { _velocityY = -_jumpSpeed; }
            }
            else if (!_inAir)
            {
                _recoil = true;
                if (pDirection == PlayerDirection.left) { _velocityX = _pushBackSpeed * 3; }
                else if (pDirection == PlayerDirection.right) { _velocityX = -_pushBackSpeed * 3; }
            }
        }
        //-----------LIVES AND SCORE------------
        public void playerDIE()
        {
            if (lives > 1)
            {
                lives -= 1;
                x = spawnX;
                y = spawnY;
            }
            else
            {
                _level.thisgame.levelWon = false;
                _level.thisgame.setGameState(GameStates.endscreen);
            }
        }
        public void addPoints(int pPoints)
        {
            _level.thisgame.playerScore += pPoints;
        }
    }
}
