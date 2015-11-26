using GXPEngine.Core;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        private HeraGUN _game;
        private bool _coinInserted = false;
        private AnimationSprite _text;
        private float _frameCounter = 0;
        public Menu(HeraGUN pGame)
        {
            _game = pGame;
            draw();
        }
        void Update()
        {
            checkButtons();
            animateText();
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
            }
        }
        private void draw()
        {
            Sprite menuBG = new Sprite("menubackground.png");
            AddChild(menuBG);

            _text = new AnimationSprite("text.png", 0, 4);
            AddChild(_text);
            _text.SetXY(game.width / 2, game.height - _text.width);
            _text.SetFrame(2);
        }
        private void animateText()
        {
            _frameCounter += 0.02f;

            if (_frameCounter >= 1)
            {
                _frameCounter = 0;
                visible = visible ? false : true;
            }
        }
        protected override Collider createCollider()
        {
            return null;
        }
    }
}