using System;
using GXPEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HeraGUN : Game
{
    //GameState
    private GameStates _state = GameStates.@default;

    //Gameobjects
    private Menu _menu;
    private Level _part1;
    private Level _part2;
    private Level _part3;
    private WonLostScreen _wlscreen;
    private Endscreen _endscreen;
    private NameInput _nameinput;

    public static bool playerHasWeapon = false;
    public int playerScore = 0;
    public bool levelWon = false;
    private bool shaked = false;

    SoundChannel soundChannel = new SoundChannel(1);
    Sound backgroundSound = new Sound("background.wav", false, true);
    private bool _loopStarted = false;
    public HeraGUN() : base(1024, 768, false)
    {
        backgroundSound.Play(false, 1);
        setGameState(GameStates.menu);
    }
    void Update()
    {
        resetShake();
        loopMusic();
    }
    private void loopMusic()
    {
        if (_loopStarted) return;

        if (soundChannel.IsPlaying == false)
        {
            Sound backgroundloop = new Sound("backgroundloop.mp3", true, true);
            backgroundloop.Play(false, 1);
            _loopStarted = true;
        }
    }

    public void setGameState(GameStates pState)
    {
        if (pState == _state) return;
        destroyGameState(_state);
        _state = pState;
        newGameState(pState);
    }
    private void newGameState(GameStates pState)
    {
        switch (pState)
        {
            case GameStates.menu:
                {
                    _menu = new Menu(this);
                    AddChild(_menu);
                    break;
                }
            case GameStates.part1:
                {
                    _part1 = new Level(this, "part1.txt", 1);
                    AddChild(_part1);
                    break;
                }
            case GameStates.part2:
                {
                    _part2 = new Level(this, "part2.txt", 2);
                    AddChild(_part2);
                    break;
                }
            case GameStates.part3:
                {
                    _part3 = new Level(this, "part3.txt", 3);
                    AddChild(_part3);
                    break;
                }
            case GameStates.wonlostscreen:
                {
                    _wlscreen = new WonLostScreen(this);
                    AddChild(_wlscreen);
                    break;
                }
            case GameStates.nameinput:
                {
                    _nameinput = new NameInput(_part1);
                    AddChild(_nameinput);
                    break;
                }
            case GameStates.endscreen:
                {
                    _endscreen = new Endscreen(this);
                    AddChild(_endscreen);
                    break;
                }

        }
    }
    private void destroyGameState(GameStates pState)
    {
        switch (pState)
        {
            case GameStates.menu:
                {
                    if (_menu != null) { _menu.Destroy(); }
                    break;
                }
            case GameStates.part1:
                {
                    if (_part1 != null) { _part1.Destroy(); }
                    break;
                }
            case GameStates.part2:
                {
                    if (_part2 != null) { _part2.Destroy(); }
                    break;
                }
            case GameStates.part3:
                {
                    if (_part3 != null) { _part3.Destroy(); }
                    break;
                }
            case GameStates.wonlostscreen:
                {
                    if (_wlscreen != null) { _wlscreen.Destroy(); }
                    break;
                }
            case GameStates.nameinput:
                {
                    if (_nameinput != null) { _nameinput.Destroy(); }
                    break;
                }
            case GameStates.endscreen:
                {
                    if (_endscreen != null) { _endscreen.Destroy(); }
                    break;
                }

        }
    }

    public void shakeScreen()
    {
        if (!shaked)
        {
            x += 5;
            shaked = true;
        }
    }
    private void resetShake()
    {
        if (shaked)
        {
            x -= 5;
            shaked = false;
        }
    }

    static void Main()
    {
        new HeraGUN().Start();
    }
}