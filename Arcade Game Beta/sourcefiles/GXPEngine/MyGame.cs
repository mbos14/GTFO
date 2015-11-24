using System;
using GXPEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyGame : Game
{
    //GameState
    private GameStates _state = GameStates.@default;

    //Gameobjects
    private Menu _menu;
    private Level _level;
    private Level _bossArena;
    private Endscreen _endscreen;
    private NameInput _nameinput;

    //public Drawer drawer;
    //Level variables
    public static bool playerHasWeapon = false;
    public int playerScore = 0;
    public bool levelWon = false;

    public MyGame() : base(1024, 768, false)
    {
        //drawer = new Drawer(1024, 768);
        //AddChild(drawer);

        setGameState(GameStates.endscreen);
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
            case GameStates.level:
                {
                    _level = new Level(this, "level1prototype.txt");
                    AddChild(_level);
                    break;
                }
            case GameStates.bossarena:
                {
                    _bossArena = new Level(this, "bossarena.txt");
                    AddChild(_bossArena);
                    break;
                }
            case GameStates.nameinput:
                {
                    _nameinput = new NameInput(_level);
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
            case GameStates.level:
                {
                    if (_level != null) { _level.Destroy(); }
                    break;
                }
            case GameStates.bossarena:
                {
                    if (_bossArena != null) { _bossArena.Destroy(); }
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
    static void Main()
    {
        new MyGame().Start();
    }
}