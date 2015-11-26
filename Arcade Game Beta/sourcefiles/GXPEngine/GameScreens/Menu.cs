using GXPEngine.Core;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        private HeraGUN _game;
        private bool _coinInserted = false;
        private AnimationSprite _text;
        private float _frameCounter = 0;
        private float _bgFrameCounter = 0.0f;
        private float _bgWaitCounter = 0.0f;
        private bool _secondDrewn = false;

        AnimationSprite menuBG = new AnimationSprite("menuanimation.png", 10, 3);
        public Menu(HeraGUN pGame)
        {
            _game = pGame;
            drawNormal();
        }
        void Update()
        {
            checkButtons();
            animateText();
            animateBackground();
            drawSecond();
        }
        private void checkButtons()
        {
            if (Input.GetAnyKeyDown() && _coinInserted)
            {
                _game.setGameState(GameStates.part1);
            }
            if (Input.GetKeyDown((int)PlayerButtons.insert))
            {
                _coinInserted = true;
                drawSecond();
            }
        }
        private void drawNormal()
        {
            AddChild(menuBG);

            _text = new AnimationSprite("text.png", 1, 4);
            AddChild(_text);
            _text.SetOrigin(_text.width / 2, _text.height / 2);
            _text.SetXY(game.width / 2 + 230, game.height - 150);
            _text.SetFrame(2);
        }
        private void drawSecond()
        {
            if (_coinInserted && !_secondDrewn)
            {
                menuBG.Destroy();
                _text.Destroy();

                Sprite background = new Sprite("buttonmenu.png");
                AddChild(background);

                _text = new AnimationSprite("text.png", 1, 4);
                AddChild(_text);
                _text.SetFrame(1);
                _text.SetXY(game.width / 2 - 390 , game.height - 60);
                _secondDrewn = true;
            }
        }
        private void animateBackground()
        {
            if (_bgFrameCounter <= 21)
            {
                _bgFrameCounter += 0.1f;
                menuBG.SetFrame((int)_bgFrameCounter);
            }
            else if(_bgFrameCounter >= 21)
            {
                _bgWaitCounter++;
            }

            if (_bgWaitCounter >= 100)
            {
                _bgFrameCounter = 0;
                _bgWaitCounter = 0;
                menuBG.SetFrame((int)_bgFrameCounter);
            }
        }
        private void animateText()
        {
            _frameCounter += 0.02f;

            if (_frameCounter >= 1)
            {
                _frameCounter = 0;
                _text.visible = _text.visible ? false : true;
            }
        }
        protected override Collider createCollider()
        {
            return null;
        }
    }
}